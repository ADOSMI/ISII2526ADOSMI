using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_PedirBocadillo
{
    public class SelectBocadillosForCompra_PO : PageObject
    {
        By inputTamano = By.Id("inputTamano");
        By inputTipoPan = By.Id("inputTipopan");
        By buttonBuscarBocadillos = By.Id("buscarBocadillos");
        By tableOfBocadillosBy = By.Id("TableOfBocadillos");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonComprarBocadillos = By.Id("compraBocadilloButton");

        public SelectBocadillosForCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchBocadillos(string tamano, string tipoPan)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputTamano);
            _driver.FindElement(inputTamano).SendKeys(tamano);
            //if (tipoPan == "") tipoPan = "All";
            WaitForBeingClickable(inputTipoPan);
            _driver.FindElement(inputTipoPan).SendKeys(tipoPan);
            _driver.FindElement(buttonBuscarBocadillos).Click();


        }

        public bool CheckListOfBocadillos(List<string[]> expectedBocadillos)
        {
            return CheckBodyTable(expectedBocadillos, tableOfBocadillosBy);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddBocadilloToComprarCart(string bocadilloNombre)
        {
            WaitForBeingClickable(By.Id("bocadilloToCompra_" + bocadilloNombre));
            _driver.FindElement(By.Id("bocadilloToCompra_" + bocadilloNombre)).Click();
        }

        public void RemoveMovieFromRentingCart(string bocadilloNombre)
        {
            WaitForBeingClickable(By.Id("removeBocadillo_" + bocadilloNombre));
            _driver.FindElement(By.Id("removeBocadillo_" + bocadilloNombre)).Click();
        }

        public void Comprar() {
            _driver.FindElement(buttonComprarBocadillos).Click();
        }

        public bool CompraNoDisponible()
        {

            return _driver.FindElement(buttonComprarBocadillos).Displayed == false;
        }

    }

}