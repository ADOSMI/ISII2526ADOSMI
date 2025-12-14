using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.UC_Reseña
{
    public class PostCrearReseña_PO : PageObject
    {
        
        private By _nombreUsuarioBy = By.Id("NombreUsuario"); 
        private By _tituloBy = By.Id("Titulo");
        private By _descripcionBy = By.Id("Descripcion");
        private By _valGeneralBy = By.Id("Valoracion");
        private By _botonPublicarBy = By.Id("Submit");
        private By _botonVolverBy = By.Id("ModifyBocadillos");
        private By _botonConfirmarModalXPath = By.XPath("//div[contains(@class, 'modal')]//button[contains(@class, 'btn-primary')]");

        
        private IWebElement _nombreUsuario() => _driver.FindElement(_nombreUsuarioBy); 
        private IWebElement _titulo() => _driver.FindElement(_tituloBy);
        private IWebElement _descripcion() => _driver.FindElement(_descripcionBy);
        private IWebElement _valGeneral() => _driver.FindElement(_valGeneralBy);
        private IWebElement _botonPublicar() => _driver.FindElement(_botonPublicarBy);
        private IWebElement _botonVolver() => _driver.FindElement(_botonVolverBy);

        public PostCrearReseña_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        

        
        public void RellenarDatosGenerales(string usuario, string titulo, string descripcion, string valoracionGeneral)
        {
            WaitForBeingVisible(_tituloBy);

            // Rellenar Usuario
            if (usuario != "")
            {
                _nombreUsuario().Clear();
                _nombreUsuario().SendKeys(usuario);
            }

            if (titulo != "")
            {
                _titulo().Clear();
                _titulo().SendKeys(titulo);
            }

            if (descripcion != "")
            {
                _descripcion().Clear();
                _descripcion().SendKeys(descripcion);
            }

            if (valoracionGeneral != "")
            {
                var selectElement = new SelectElement(_valGeneral());
                
                selectElement.SelectByText(valoracionGeneral);
            }
        }

        public void RellenarPuntuacionBocadillo(string nombreBocadillo, string puntuacion)
        {
            string cssSelector = $"tr[id='BocadilloData_{nombreBocadillo}'] input";
            By inputDinamicoBy = By.CssSelector(cssSelector);

            WaitForBeingVisible(inputDinamicoBy);
            _driver.FindElement(inputDinamicoBy).SendKeys(puntuacion);
        }

        public void PublicarReseña()
        {
            // Publicar
            WaitForBeingClickable(_botonPublicarBy);
            _botonPublicar().Click();

            
            System.Threading.Thread.Sleep(1000);
            try
            {
                var botonConfirmar = _driver.FindElement(_botonConfirmarModalXPath);
                botonConfirmar.Click();
            }
            catch (NoSuchElementException)
            {
                try { _driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter); } catch { }
            }

            
            System.Threading.Thread.Sleep(3000);
        }

        public void VolverASeleccion()
        {
            WaitForBeingClickable(_botonVolverBy);
            _botonVolver().Click();
        }

        
        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        

        public bool HayErroresDeValidacion()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

                
                var bannerErrores = _driver.FindElements(By.Id("ErrorsShown"));

                if (bannerErrores.Count > 0)
                {
                    var banner = bannerErrores[0];
                    if (banner.Displayed && banner.Text.Contains("Errors:") && banner.Text.Length > 10)
                    {
                        return true; // ¡Encontrado el error global!
                    }
                }

                var mensajesIndividuales = _driver.FindElements(By.ClassName("validation-message"));
                if (mensajesIndividuales.Any(e => e.Displayed && !string.IsNullOrWhiteSpace(e.Text)))
                {
                    return true;
                }

                var inputsInvalidos = _driver.FindElements(By.CssSelector("input:invalid"));
                if (inputsInvalidos.Count > 0) return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EstoyEnFormulario()
        {
            try
            {
                return _botonPublicar().Displayed;
            }
            catch
            {
                return false;
            }
        }

        public string GetValorTitulo()
        {
            WaitForBeingVisible(_tituloBy);
            return _titulo().GetAttribute("value");
        }


        public string GetValorUsuario()
        {
            WaitForBeingVisible(_nombreUsuarioBy);
            return _nombreUsuario().GetAttribute("value");
        }

        public string GetValorDescripcion()
        {
            WaitForBeingVisible(_descripcionBy);
            return _descripcion().GetAttribute("value");
        }

        public string GetValorValoracion()
        {
            WaitForBeingVisible(_valGeneralBy);
            var select = new SelectElement(_valGeneral());
            return select.SelectedOption.Text; 
        }

        public string GetPuntuacionBocadillo(string nombreBocadillo)
        {
            string cssSelector = $"tr[id='BocadilloData_{nombreBocadillo}'] input";
            By inputDinamicoBy = By.CssSelector(cssSelector);

            WaitForBeingVisible(inputDinamicoBy);
            return _driver.FindElement(inputDinamicoBy).GetAttribute("value");
        }
    }
}
