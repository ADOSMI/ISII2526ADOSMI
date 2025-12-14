using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_ComprarBonos
{
    public class SeleccionarBonos_PO : PageObject
    {
        private By _nombreBonoBy = By.Id("inputNombre");
        private By _tipoBocadilloBy = By.Id("inputTipoBocadillo");
        private By _botonBuscarBy = By.Id("searchBonos");
        private By _botonComprarBy = By.Id("botonComprar");
        private By _tablaBonosForCompraBy = By.Id("TablaBonosForCompra");

        private IWebElement _nombreBono() => _driver.FindElement(_nombreBonoBy);
        private IWebElement _tipoBocadillo() => _driver.FindElement(_tipoBocadilloBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarBy);
        private IWebElement _botonComprar() => _driver.FindElement(_botonComprarBy);

        public SeleccionarBonos_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Simular el paso de filtrar los bonos. El método tendrá un parámetro por cada uno de los filtros que tengas.
        public void FiltrarBonosForCompra(string nombre, string tipoBocadillo)
        {
            // Para poder interaccionar con el elemento debe ser visible.
            // El método se le pasa el Id, no la referencia.
            WaitForBeingVisible(_nombreBonoBy);
            WaitForBeingVisible(_tipoBocadilloBy);
            

            // Se simula que se escribe en la cuadro de texto el filtro del nombre del videojuego.
            _driver.FindElement(_nombreBonoBy).SendKeys(nombre);
            _driver.FindElement(_tipoBocadilloBy).SendKeys(tipoBocadillo);
            


            _botonBuscar().Click();
            // Se espera 2000 milisegundos para esperar a que la tabla se recargue.
            System.Threading.Thread.Sleep(2000);
        }

        // Este método permite comprobar si la lista de videojuegos mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaBonos(List<string[]> bonosEsperados)
        {

            return CheckBodyTable(bonosEsperados, _tablaBonosForCompraBy);
        }

        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledComprar()
        {
            WaitForBeingVisible(By.Id("botonComprar"));
            IWebElement botonComprar = _botonComprar();

            return botonComprar.Enabled;
        }

        // Selecciona los videojuegos cuyos Ids aparecen en la lista.

        public void SeleccionarBonos(List<string> nombresBonos)
        {
            //we wait for till the movies are available to be selected 
            foreach (var nombre in nombresBonos)
            {
                // Buscamos por el ID correcto definido en el Razor
                var idBoton = $"bonoToBuy_{nombre}";
                var selector = By.Id(idBoton);

                WaitForBeingVisible(selector);
                _driver.FindElement(selector).Click();

                // Pequeña espera para que Blazor procese el clic y actualice el carrito
                System.Threading.Thread.Sleep(500);
            }
        }

        // Comprueba si los videojuegos cuyos Ids aparecen en la lista están todos seleccionados.
        // En caso contrario devuelve false.
        public bool ComprobarSeleccionBonos(List<string> bonosIds)
        {

            foreach (var bonoId in bonosIds)
            {
                WaitForBeingVisible(By.Id($"bonoCompra_{bonoId}"));
                string value = _driver.FindElement(By.Id($"bonoCompra_{bonoId}")).GetAttribute("checked");
                if (value.Equals("false")) return false;
            }

            return true;
        }

        // Pulsar el botón comprar.
        public void Comprar()
        {
            _botonComprar().Click();
            System.Threading.Thread.Sleep(200); // Si no añado este retardo no funcionan las pruebas.
        }

        //Metodo para deseleccionar bono
        public void DeseleccionarBono(string nombreBono)
        {
            var idBotonEliminar = $"removeBono_{nombreBono}";
            var selector = By.Id(idBotonEliminar);

            WaitForBeingVisible(selector);
            _driver.FindElement(selector).Click();

            System.Threading.Thread.Sleep(500);
        }

        public bool EstaEnElCarrito(string nombreBono)
        {
            // El ID del botón de borrar en el carrito es "removeBono_NOMBRE"
            var idBoton = $"removeBono_{nombreBono}";

            try
            {
                // Intentamos encontrar el botón X del carrito
                _driver.FindElement(By.Id(idBoton));
                return true; // Si lo encuentra, es que el bono está en el carrito
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return false; // Si no lo encuentra, es que no está
            }
        }
    }
}
