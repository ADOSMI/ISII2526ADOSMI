using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using SeleniumExtras.WaitHelpers; 

namespace AppForSEII2526.UIT.UC_Reseña
{
    public class SelectBocadillosForReseña_PO : PageObject
    {
        
        private By _inputNombreBy = By.Id("inputNombre");
        private By _inputPvpBy = By.Id("inputPvp");
        private By _botonBuscarBy = By.Id("searchBocadillos");
        private By _tablaBocadillosBy = By.Id("TableOfBocadillos");

        
        private By _botonCrearReseñaBy = By.Id("createReviewButton");

        private IWebElement _inputNombre() => _driver.FindElement(_inputNombreBy);
        private IWebElement _inputPvp() => _driver.FindElement(_inputPvpBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarBy);
        private IWebElement _botonCrearReseña() => _driver.FindElement(_botonCrearReseñaBy);

        public SelectBocadillosForReseña_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // FILTRAR
        public void FiltrarBocadillos(string nombre, string pvp)
        {
            
            WaitForBeingClickable(_inputNombreBy);

            // 2. Rellenar Nombre
            if (nombre != "")
            {
                _driver.FindElement(_inputNombreBy).SendKeys(nombre);
            }

            // 3. Rellenar PVP
            if (pvp != "")
            {
                _driver.FindElement(_inputPvpBy).SendKeys(pvp);
            }

            // 4. Click en Buscar
            _driver.FindElement(_botonBuscarBy).Click();

            
            System.Threading.Thread.Sleep(1000);
        }

        // Comprobar lista
        public bool CompruebaListaBocadillos(List<string[]> bocadillosEsperados)
        {
            return CheckBodyTable(bocadillosEsperados, _tablaBocadillosBy);
        }

        // Comprobamos si el boton de crear reseña es visible
        public bool isCrearReseñaVisible()
        {
            try
            {
                var boton = _botonCrearReseña();
                return boton.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        // Añadir bocadillo
        public void AnadirBocadillo(string nombreBocadillo)
        {
            
            var idBoton = "bocadilloToAdd_" + nombreBocadillo;
            var selector = By.Id(idBoton);

            WaitForBeingVisible(selector);
            WaitForBeingClickable(selector);

            _driver.FindElement(selector).Click();

            // Esperar actualizacion de carrito
            System.Threading.Thread.Sleep(200);
        }

        // Quitar bocadillo
        public void QuitarBocadillo(string nombreBocadillo)
        {
            
            var idBoton = "removeBocadillo_" + nombreBocadillo;
            var selector = By.Id(idBoton);

            WaitForBeingVisible(selector);
            WaitForBeingClickable(selector);

            _driver.FindElement(selector).Click();
            System.Threading.Thread.Sleep(200);
        }

        // Ir a crear reseña
        public void IrACrearReseña()
        {
            WaitForBeingClickable(_botonCrearReseñaBy);
            _botonCrearReseña().Click();
            
            System.Threading.Thread.Sleep(500);
        }

        
   
    }
}
