using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using OpenQA.Selenium; 
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.UC_Reseña
{
    public class UC_Reseña_UIT : UC_UIT
    {
        private SelectBocadillosForReseña_PO _selectBocadillos_PO;
        private PostCrearReseña_PO _postReseña_PO; 

       

        // DATOS (EXISTENTES EN BBDD)
        private const string bocadilloNombre1 = "Bacon";
        private const string bocadilloPVP1 = "5,00 €";
        private const string bocadilloTamano1 = "Normal";
        private const string bocadilloPan1 = "Sin gluten";

        private const string bocadilloNombre3 = "Serranito";
        private const string bocadilloPVP3 = "2,00 €";
        private const string bocadilloTamano3 = "Normal";
        private const string bocadilloPan3 = "Rustico";

        public UC_Reseña_UIT(ITestOutputHelper output) : base(output)
        {
            _selectBocadillos_PO = new SelectBocadillosForReseña_PO(_driver, _output);
            _postReseña_PO = new PostCrearReseña_PO(_driver, _output); // <--- NUEVO: Lo inicializamos
        }

        // SELECCIONAMOS BOCADILLO (PRUEBAS POST)
        private void Precondition_SelectBaconAndGoToForm()
        {
            Initial_step_opening_the_web_page();
            _selectBocadillos_PO.WaitForBeingVisible(By.Id("nav-crear-resena"));
            _driver.FindElement(By.Id("nav-crear-resena")).Click();
            System.Threading.Thread.Sleep(2000);

            
            _selectBocadillos_PO.FiltrarBocadillos("Bacon", ""); System.Threading.Thread.Sleep(2000);
            _selectBocadillos_PO.AnadirBocadillo(bocadilloNombre1); System.Threading.Thread.Sleep(2000);
            _selectBocadillos_PO.IrACrearReseña(); System.Threading.Thread.Sleep(3000);
        }

        //PRUEBAS SELECT

        [Theory]
        [InlineData(bocadilloNombre1, bocadilloPVP1, bocadilloTamano1, bocadilloPan1, "Bacon", "")] // FA 0: Filtro Nombre
        [InlineData(bocadilloNombre3, bocadilloPVP3, bocadilloTamano3, bocadilloPan3, "", "3.00")] // FA 0: Filtro PVP
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF0_Filtering(string nombre, string pvp, string tamano, string pan,
                                      string filtroNombre, string filtroPrecio)
        {
            // Arrange
            Initial_step_opening_the_web_page();
            _selectBocadillos_PO.WaitForBeingVisible(By.Id("nav-crear-resena"));
            _driver.FindElement(By.Id("nav-crear-resena")).Click();
            System.Threading.Thread.Sleep(2000);

            var bocadillosEsperados = new List<string[]>
            {
                new string[] { nombre, pvp, tamano, pan }
            };

            _selectBocadillos_PO.FiltrarBocadillos(filtroNombre, filtroPrecio);
            System.Threading.Thread.Sleep(2000);
            Assert.True(_selectBocadillos_PO.CompruebaListaBocadillos(bocadillosEsperados));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_AF2_CartInteraction()
        {
            // Arrange
            Initial_step_opening_the_web_page();
            _selectBocadillos_PO.WaitForBeingVisible(By.Id("nav-crear-resena"));
            _driver.FindElement(By.Id("nav-crear-resena")).Click();
            System.Threading.Thread.Sleep(2000);

            _selectBocadillos_PO.FiltrarBocadillos("Bacon", ""); System.Threading.Thread.Sleep(2000);

            Assert.False(_selectBocadillos_PO.isCrearReseñaVisible(),
                "ERROR: El botón 'Crear Reseña' debería estar oculto al principio.");

            _selectBocadillos_PO.AnadirBocadillo(bocadilloNombre1); System.Threading.Thread.Sleep(2000);

            Assert.True(_selectBocadillos_PO.isCrearReseñaVisible(),
                "ERROR: El botón 'Crear Reseña' debería verse tras añadir un bocadillo.");

            _selectBocadillos_PO.QuitarBocadillo(bocadilloNombre1); System.Threading.Thread.Sleep(2000);

            Assert.False(_selectBocadillos_PO.isCrearReseñaVisible(),
                "ERROR: El botón debería volver a ocultarse tras vaciar el carrito.");
        }

        //PRUEBAS DEL POST

        // FA3 -> INTENTAR ENVIARLO VACIO
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF3_FormValidation_RequiredFields()
        {
            // 1. Arrange
            Precondition_SelectBaconAndGoToForm();

            
            _postReseña_PO.RellenarPuntuacionBocadillo("Bacon", "0");

            _postReseña_PO.PublicarReseña();

            
            System.Threading.Thread.Sleep(2000);

            
            Assert.True(_postReseña_PO.EstoyEnFormulario(), "No debería avanzar si hay errores.");

            
            Assert.True(_postReseña_PO.HayErroresDeValidacion(),
                "Debe aparecer el mensaje de error de la puntuación.");
        }

        // FA4 -> MODIFICAR Y VER QUE TODO SE HA GUARDADO
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF4_BackButton_PreserveData()
        {
    
            string usuarioEsp = "Oscar123";
            string tituloEsp = "Sugerencia para persistencia";
            string descEsp = "Probando que no se borra nada al volver";
            string valGeneralEsp = "Cinco"; 
            string puntuacionBocadilloEsp = "9";

            Precondition_SelectBaconAndGoToForm();

            _postReseña_PO.RellenarDatosGenerales(usuarioEsp, tituloEsp, descEsp, valGeneralEsp);
            _postReseña_PO.RellenarPuntuacionBocadillo(bocadilloNombre1, puntuacionBocadilloEsp);

            _postReseña_PO.VolverASeleccion();
            System.Threading.Thread.Sleep(2000);

            Assert.True(_selectBocadillos_PO.isCrearReseñaVisible());

            
            _selectBocadillos_PO.IrACrearReseña();
            System.Threading.Thread.Sleep(2000);

            //Comprobar campo por campo que coinciden con lo expected

            Assert.Equal(usuarioEsp, _postReseña_PO.GetValorUsuario());

            Assert.Equal(tituloEsp, _postReseña_PO.GetValorTitulo()); 

            Assert.Equal(descEsp, _postReseña_PO.GetValorDescripcion());

            
            Assert.Contains(valGeneralEsp, _postReseña_PO.GetValorValoracion());

            // Comprobar Puntuación del Bacon
            Assert.Equal(puntuacionBocadilloEsp, _postReseña_PO.GetPuntuacionBocadillo(bocadilloNombre1));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_BasicFlow_SubmitSuccess()
        {
            Precondition_SelectBaconAndGoToForm();


            _postReseña_PO.RellenarDatosGenerales("Oscar123", "Sugerencia para el chef", "Muy rico todo", "Cinco");
            _postReseña_PO.RellenarPuntuacionBocadillo(bocadilloNombre1, "10");

            _postReseña_PO.PublicarReseña();

            System.Threading.Thread.Sleep(4000);

            
            Assert.False(_postReseña_PO.EstoyEnFormulario(),
                "ERROR: El formulario sigue visible. Probablemente falló la validación del servidor (Usuario o Título incorrecto).");
        }
    }
}