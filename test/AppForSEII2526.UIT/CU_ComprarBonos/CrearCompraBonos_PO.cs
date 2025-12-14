using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_ComprarBonos
{
    public class CrearCompraBonos_PO : PageObject
    {
        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _botonComprarBy = By.Id("Submit");
        private By _botonModificarBonosBy = By.Id("botonVolver");
        private By _seleccionarMetodoPagoBy = By.Id("MetodoPago");
        private By _precioTotalBy = By.Id("precioTotal");
        private By _tablaBonosForCompraBy = By.Id("TablaBonosForCompra");

        private By _nombreBy = By.Id("Nombre");
        private By _primerApellidoBy = By.Id("Primer Apellido");
        private By _segundoApellidoBy = By.Id("Segundo Apellido");
        private By _erroresBy = By.CssSelector(".validation-message, .validation-errors, ul.validation-errors li");



        private IWebElement _botonComprar() => _driver.FindElement(_botonComprarBy);
        private IWebElement _botonModificarBonos() => _driver.FindElement(_botonModificarBonosBy);
        private IWebElement _metodoPago() => _driver.FindElement(_seleccionarMetodoPagoBy);
        private IWebElement _nombre() => _driver.FindElement(_nombreBy);
        private IWebElement _primerApellido() => _driver.FindElement(_primerApellidoBy);
        private IWebElement _segundoApellido() => _driver.FindElement(_segundoApellidoBy);

        public CrearCompraBonos_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Pulsar el botón volver.
        public void Volver()
        {
            _botonModificarBonos().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Pulsar el botón comprar.
        public void Comprar()
        {
            _botonComprar().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Método para obtener el precio total (Necesario para el test UC3_5)
        public string ObtenerPrecioTotal()
        {
            WaitForBeingVisible(_precioTotalBy);
            return _driver.FindElement(_precioTotalBy).Text;
        }


        // Hay un problema, no salta el evento onChange por lo que no se actualiza correctamente el contenedor del estado.
        public void setMetodoPago(string metodoPago)
        {
            WaitForBeingClickable(_seleccionarMetodoPagoBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_metodoPago());
            // Selecciona la opción que se ha indicado en el parámetro.
            selectElement.SelectByText(metodoPago);
        }
        public void setDatos(string nombre, string primerApellido, string segundoApellido, string metodoPago)
        {
            WaitForBeingVisible(_nombreBy);
            WaitForBeingVisible(_primerApellidoBy);
            WaitForBeingVisible(_segundoApellidoBy);
            WaitForBeingClickable(_seleccionarMetodoPagoBy);


            _nombre().SendKeys(nombre);
            _primerApellido().SendKeys(primerApellido);
            _segundoApellido().SendKeys(segundoApellido);

            SelectElement selectElement = new SelectElement(_metodoPago());
            selectElement.SelectByText(metodoPago);

        }

        // Método para verificar errores de validación (Necesario para UC3_6)
        public bool ExisteMensajeError()
        {
            try
            {
                // Creamos una espera de hasta 3 segundos
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

                // Le decimos: "Espera hasta que ALGÚN elemento de error sea visible"
                return wait.Until(driver =>
                {
                    var elementos = driver.FindElements(_erroresBy);
                    return elementos.Any(e => e.Displayed && !string.IsNullOrEmpty(e.Text));
                });
            }
            catch (WebDriverTimeoutException)
            {
                // Si pasan 3 segundos y no aparece nada, devolvemos false
                return false;
            }
        }

        public void ModificarBonos()
        {
            _driver.FindElement(By.Id("Volver")).Click();
        }


        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledComprar()
        {
            WaitForBeingVisible(By.Id("botonComprar"));
            IWebElement botonComprar = _botonComprar();

            return botonComprar.Enabled;
        }
    }
}
