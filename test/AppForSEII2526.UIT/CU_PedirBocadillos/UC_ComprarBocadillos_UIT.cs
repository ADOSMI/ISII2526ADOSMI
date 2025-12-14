using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_PedirBocadillo
{
    public class UC_ComprarBocadillos_UIT : UC_UIT
    {
        private SelectBocadillosForCompra_PO selectBocadillosForCompra_PO;
        private const int bocadilloId1 = 1;
        private const string bocadilloNombre1 = "Bacon";
        private const string bocadilloTipoPan1 = "Sin gluten";
        private const string bocadilloTamano1 = "Normal";
        private const int bocadilloId2 = 1;
        private const string bocadilloNombre2 = "Pollo";
        private const string bocadilloTipoPan2 = "Semillas";
        private const string bocadilloTamano2 = "Grande";



        public UC_ComprarBocadillos_UIT(ITestOutputHelper output) : base(output)
        {
            selectBocadillosForCompra_PO = new SelectBocadillosForCompra_PO(_driver, _output);
        }

        private void InitialStepsForCompraBocadillos()
        {
            Initial_step_opening_the_web_page();
            selectBocadillosForCompra_PO.WaitForBeingVisible(By.Id("CreateCompra"));
            //we click on the menu
            _driver.FindElement(By.Id("CreateCompra")).Click();
        }



        [Theory]
        [InlineData(bocadilloNombre1, bocadilloTipoPan1, bocadilloTamano1, "Normal", "")]
        [InlineData(bocadilloNombre2, bocadilloTipoPan2, bocadilloTamano2, "", "Semillas")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF1_4_5_filtering(string bocadilloNombre, string bocadilloTipoPan, string bocadilloTamano, string filtroTamano, string filtroTipoPan)
        {
            //Arrange
            InitialStepsForCompraBocadillos();
            var expectedBocadillos = new List<string[]> { new string[] { bocadilloNombre, bocadilloTipoPan, bocadilloTamano }, };

            //Act
            selectBocadillosForCompra_PO.SearchBocadillos(filtroTamano, filtroTipoPan);

            //Assert

            Assert.True(selectBocadillosForCompra_PO.CheckListOfBocadillos(expectedBocadillos));

        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_AF2_UC3_2_CompraNoDisponible()
        {
            //Arrange
            InitialStepsForCompraBocadillos();
            //Act
            selectBocadillosForCompra_PO.AddBocadilloToComprarCart(bocadilloNombre1);
            selectBocadillosForCompra_PO.RemoveMovieFromRentingCart(bocadilloNombre1);

            //Assert

            Assert.True(selectBocadillosForCompra_PO.CompraNoDisponible());

        }

    }
}

