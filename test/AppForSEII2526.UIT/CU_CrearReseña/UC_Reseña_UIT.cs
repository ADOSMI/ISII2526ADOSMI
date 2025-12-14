using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using Microsoft.VisualStudio.TestPlatform.Utilities;
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

        
        private const string URI = "https://localhost:7081/";

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
        }

        
        private void InitialStepsForReview()
        {
            // Vamos a la pag principal
            _driver.Navigate().GoToUrl(URI);
            // A crear reseña
            _selectBocadillos_PO.WaitForBeingVisible(By.Id("nav-crear-resena"));
            _driver.FindElement(By.Id("nav-crear-resena")).Click();
        }

        // --- FA0 Y BASICO2 
        [Theory]
        [InlineData(bocadilloNombre1, bocadilloPVP1, bocadilloTamano1, bocadilloPan1, "Bacon", "")] // FA 0: Filtro Nombre
        [InlineData(bocadilloNombre3, bocadilloPVP3, bocadilloTamano3, bocadilloPan3, "", "3.00")] // FA 0: Filtro PVP
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF0_Filtering(string nombre, string pvp, string tamano, string pan,
                                      string filtroNombre, string filtroPrecio)
        {
            // Arrange
            InitialStepsForReview();

            // Definir orden:
            // El orden: Nombre, Precio, Tamaño, Pan .
            var bocadillosEsperados = new List<string[]>
            {
                new string[] { nombre, pvp, tamano, pan }
            };

            _selectBocadillos_PO.FiltrarBocadillos(filtroNombre, filtroPrecio);

            
            Assert.True(_selectBocadillos_PO.CompruebaListaBocadillos(bocadillosEsperados));
        }

        // --- FLUJO ALT1 Y ALT2
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF1_AF2_CartInteraction()
        {
            // Arrange
            InitialStepsForReview();

            // Aseguramos que vemos el bocadillo (Paso 2)
            _selectBocadillos_PO.FiltrarBocadillos("Bacon", "");

            
            // Si no se seleccionan bocadillos el botón -> oculto
            Assert.False(_selectBocadillos_PO.isCrearReseñaVisible(),
                "ERROR: El botón 'Crear Reseña' debería estar oculto al principio (Carrito vacío).");

            
            // Uusario selecciona un bocadillo y lo añade a carrito
            _selectBocadillos_PO.AnadirBocadillo(bocadilloNombre1);

            // Hacemos el botón visible porque ya hay bocadillos en el carrito
            Assert.True(_selectBocadillos_PO.isCrearReseñaVisible(),
                "ERROR: El botón 'Crear Reseña' debería verse tras añadir un bocadillo.");

            
            // Usuario quiere modificar su lista. 
            _selectBocadillos_PO.QuitarBocadillo(bocadilloNombre1);

            
            // Si el carrito vuelve a estar vacío, el botón se oculta
            Assert.False(_selectBocadillos_PO.isCrearReseñaVisible(),
                "ERROR: El botón debería volver a ocultarse tras vaciar el carrito (FA 1).");
        }
    }
}