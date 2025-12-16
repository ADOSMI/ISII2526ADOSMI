using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_PedirBocadillos
{
    public class DetailCompraBocadillo_PO : PageObject
    {
        By bodySelector = By.TagName("body");

        public DetailCompraBocadillo_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void WaitForDetailsPage()
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                // Esperamos simplemente a que la URL cambie
                wait.Until(d => d.Url.ToLower().Contains("detailcompra"));
                _output.WriteLine("Estamos en la página de detalles.");
            }
            catch (Exception)
            {
                _output.WriteLine($"Timeout esperando detailcompra. URL: {_driver.Url}");
                throw;
            }
        }

        // Método SIMPLIFICADO: Busca texto bruto en toda la página
        public bool VerificarDatos(string nombreCliente, string apellidoCliente, string nombreBocadillo, string cantidad)
        {
            try
            {
                // Esperamos a que el cuerpo sea visible
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementIsVisible(bodySelector));

                // Cogemos TODO el texto de la web
                string textoPagina = _driver.FindElement(bodySelector).Text;

                _output.WriteLine("Verificando existencia de datos en pantalla...");

                bool tieneNombre = textoPagina.Contains(nombreCliente);
                bool tieneApellido = textoPagina.Contains(apellidoCliente);
                bool tieneBocadillo = textoPagina.Contains(nombreBocadillo);
                bool tieneCantidad = textoPagina.Contains(cantidad);

                _output.WriteLine($"Nombre ({nombreCliente}): {tieneNombre}");
                _output.WriteLine($"Apellido ({apellidoCliente}): {tieneApellido}");
                _output.WriteLine($"Bocadillo ({nombreBocadillo}): {tieneBocadillo}");
                _output.WriteLine($"Cantidad ({cantidad}): {tieneCantidad}");

                return tieneNombre && tieneApellido && tieneBocadillo && tieneCantidad;
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error leyendo la página: {ex.Message}");
                return false;
            }

        }
    }
}
