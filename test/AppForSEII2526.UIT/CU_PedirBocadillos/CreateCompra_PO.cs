using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; 
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.UC_PedirBocadillo
{

    public class CreateCompra_PO : PageObject
    {
        By inputNombre = By.Id("Name");
        By inputApellido1 = By.Id("Surname1");
        By inputApellido2 = By.Id("Surname2");
        By selectMetodoPago = By.Id("MetodoPago");
        By buttonEnviar = By.Id("Enviar");
        By buttonModificar = By.Id("ModifyMovies");
        By tableCompraItems = By.Id("TableOfRentalItems");
        By errorShownBy = By.Id("ErrorsShown");
        By validationSummaryBy = By.CssSelector("ul.validation-errors");
        By buttonConfirmarDialogo = By.CssSelector(".modal-footer .btn-primary");

        public CreateCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }


        public void RellenarDatosCliente(string nombre, string apellido1, string apellido2, string metodoPago)
        {
            
            // Nombre
            WaitForBeingVisible(inputNombre);
            var elNombre = _driver.FindElement(inputNombre);
            elNombre.Click();
            elNombre.SendKeys(Keys.Control + "a");
            elNombre.SendKeys(Keys.Delete);
            elNombre.SendKeys(nombre);
            elNombre.SendKeys(Keys.Tab);


            // Apellido 1
            var elAp1 = _driver.FindElement(inputApellido1);
            elAp1.Click();
            elAp1.Clear();
            elAp1.SendKeys(apellido1);

            // Apellido 2
            WaitForBeingVisible(inputApellido2);
            var elAp2 = _driver.FindElement(inputApellido2);
            elAp2.Clear();
            elAp2.SendKeys(apellido2 ?? "");

            // Método de pago
            WaitForBeingVisible(selectMetodoPago);
            _driver.FindElement(selectMetodoPago).SendKeys(metodoPago);
        }

        public void ClickGuardar()
        {
            WaitForBeingClickable(buttonEnviar);
            _driver.FindElement(buttonEnviar).Click();


            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementIsVisible(buttonConfirmarDialogo));

                // Si aparece, lo pulsamos
                _driver.FindElement(buttonConfirmarDialogo).Click();
                _output.WriteLine("Modal de confirmación detectado y aceptado.");
            }
            catch (WebDriverTimeoutException)
            {
                _output.WriteLine("No apareció modal de confirmación (posible error de validación previo).");
            }
        }


        public void ClickModificarBocadillos()
        {
            WaitForBeingClickable(buttonModificar);
            _driver.FindElement(buttonModificar).Click();
        }

        public void SeleccionarBocadilloCantidad(string bocadilloNombre, int cantidad)
        {
            By cssSelector = By.CssSelector($"#BocadilloData_{bocadilloNombre} input");

            WaitForBeingClickable(cssSelector);
            var inputElement = _driver.FindElement(cssSelector);

            inputElement.Click();
            inputElement.SendKeys(Keys.Control + "a");
            inputElement.SendKeys(Keys.Delete);

            inputElement.SendKeys(cantidad.ToString());
            
          
            inputElement.SendKeys(Keys.Tab);
        }


        public bool IsBotonEnviarActivo()
        {
            try
            {
                var boton = _driver.FindElement(buttonEnviar);
                return boton.Displayed && boton.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public bool CheckCompraItems(List<string[]> expectedItems)
        {
            return CheckBodyTable(expectedItems, tableCompraItems);
        }




        public bool CheckMessageError(string errorMessage)
        {
            // 1. Primero miramos si está en el ValidationSummary (errores de formulario "campos requeridos")
            try
            {
                // Esperamos un poco a ver si aparece la lista de errores
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementIsVisible(validationSummaryBy));

                var errorList = _driver.FindElement(validationSummaryBy);
                string textoValidacion = errorList.Text;

                if (textoValidacion.Contains(errorMessage))
                {
                    _output.WriteLine($"Error encontrado en ValidationSummary: '{errorMessage}'");
                    return true;
                }
            }
            catch (Exception)
            {
                // Si no está ahí, no pasa nada, seguimos buscando en el otro sitio
            }

            // 2. Si no estaba arriba, miramos si es un error de Servidor (ErrorsShown)
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementIsVisible(errorShownBy));

                IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
                wait.Until(d => actualErrorShown.Text.Length > 0);

                string textoActual = actualErrorShown.Text;
                _output.WriteLine($"Texto buscado: '{errorMessage}'");
                _output.WriteLine($"Texto encontrado en ErrorsShown: '{textoActual}'");

                return textoActual.Contains(errorMessage);
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error no encontrado en ningún sitio. Excepción: {ex.Message}");
                return false;
            }
        }



    }
}
