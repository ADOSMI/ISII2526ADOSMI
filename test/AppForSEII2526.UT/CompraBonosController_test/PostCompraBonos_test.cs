using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.BonosDTOs;
using AppForSEII2526.API.DTOs.CompraBonosDTOs;
using AppForSEII2526.API.Models;
using AppForSEII2526.UT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.ContentModel;
using System;
using System.Linq.Expressions;
using System.Net.WebSockets;

namespace AppForSEII2526.UT.CompraBonosController_test
{
    public class PostCompraBonos_test : AppForSEII25264SqliteUT
    {
        public PostCompraBonos_test()
        {
            var applicationUser = new List<ApplicationUser>()
            {
                new ApplicationUser("1","Adolfo","Escribano","Martinez","AEM")
            };

            var tipoBocadillo = new List<TipoBocadillo>()
            {
                new TipoBocadillo(1,"Completo"),
                new TipoBocadillo(2,"Serrano"),
                new TipoBocadillo(3,"Politecnico")
            };

            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(tipoBocadillo[0],1,5,2,"Bono1",2,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[1],2,5,3,"Bono2",3,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[1],3,5,5,"Bono3",3,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[2],4,5,4,"Bono3",4,new List<BonosComprados>()),
            };

            var compraBono = new CompraBono(null, "Adolfo", "Escribano", "Martinez", 3, DateTime.Now.Date, EnumMetodosPago.PayPal, 5, 15, new List<BonosComprados>());
            var bonoComprado = new BonosComprados(3, 1, 3, 3, bonos[1], compraBono);

            var compraBono2 = new CompraBono(null, "Adolfo", "Escribano", "Martinez", 4, DateTime.Now.Date, EnumMetodosPago.PayPal, 5, 15, new List<BonosComprados>());
            var bonoComprado2 = new BonosComprados(4, 1, 4, 4, bonos[3], compraBono2);

            compraBono.BonosComprados.Add(bonoComprado);
            compraBono.BonosComprados.Add(bonoComprado2);

            _context.AddRange(applicationUser);
            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.AddRange(compraBono);
            _context.AddRange(compraBono2);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> TestCasesPara_CrearCompraBono()
        {
            //SIN BONOS ELEGIDOS
            BonosForCreateDTO comprarNoItemCompra = new BonosForCreateDTO(null, "Adolfo", "Escribano", "Martinez", EnumMetodosPago.PayPal, new List<BonosItemDTO>());

            //BONO QUE NO EXISTE
            BonosForCreateDTO comprarBonoNoExiste = new BonosForCreateDTO(null, "Adolfo", "Escribano", "Martinez", EnumMetodosPago.PayPal, new List<BonosItemDTO>());
            comprarBonoNoExiste.ItemsCompraBono.Add(new BonosItemDTO(0, 5, 5, "Bono15", "Submarino", 10));

            //CANTIDAD DEMASIADO ALTA (NO HAY SUFICIENTE STOCK DISPONIBLE DE ESE BONO)
            BonosForCreateDTO comprarCantidadMuyAlta = new BonosForCreateDTO(null, "Adolfo", "Escribano", "Martinez", EnumMetodosPago.PayPal, new List<BonosItemDTO>());
            comprarCantidadMuyAlta.ItemsCompraBono.Add(new BonosItemDTO(1, 2, 2, "Bono1", "Completo", 10000));

            //CANTIDAD DEMASIADO ALTA (NO HAY SUFICIENTE STOCK DISPONIBLE DE ESE BONO)
            BonosForCreateDTO comprarBonoPrecioMenor3 = new BonosForCreateDTO(null, "Adolfo", "Escribano", "Martinez", EnumMetodosPago.PayPal, new List<BonosItemDTO>());
            comprarBonoPrecioMenor3.ItemsCompraBono.Add(new BonosItemDTO(3, 2, 3, "Bono3", "Completo", 1));





            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { comprarNoItemCompra, "Error. Necesitas seleccionar al menos un bono para ser comprado.",  },
                new object[] { comprarBonoNoExiste, $"Error. Bono titulado {comprarBonoNoExiste.ItemsCompraBono[0].Nombre} con Id {comprarBonoNoExiste.ItemsCompraBono[0].Id} no existe en la base de datos", },
                new object[] { comprarCantidadMuyAlta, $"Error. Bono titulado {comprarCantidadMuyAlta.ItemsCompraBono[0].Nombre} tiene solo 5 unidades disponibles pero {comprarCantidadMuyAlta.ItemsCompraBono[0].Cantidad} fueron seleccionadas", },
                new object[] { comprarBonoPrecioMenor3, $"Error!. El precio debe ser mayor que 3",  },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesPara_CrearCompraBono))]
        public async Task CrearCompra_Error_test(BonosForCreateDTO? compraBonoForCreate, string errorEsperado)
        {
            // Arrange
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;

            var controller = new CompraBonosController(_context, logger);

            // Act
            var result = await controller.CrearCompra(compraBonoForCreate);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            //we check that the expected error message and actual are the same
            Assert.Equal(errorEsperado, problemDetails.Errors.First().Value[0]);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearCompra_Exito_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;

            BonosForCreateDTO compraBonosForCreate = new BonosForCreateDTO(null, "Adolfo", "Escribano", "Martinez", EnumMetodosPago.PayPal, new List<BonosItemDTO>());
            compraBonosForCreate.ItemsCompraBono.Add(new BonosItemDTO(1, 4, 2, "Bono1", "Completo", 1));

            //we expected to have a new purchase in the database
            var esperadoCompraBonoDetailDTO = new BonosDetailDTO(5, "Adolfo", "Escribano", "Martinez", EnumMetodosPago.PayPal, DateTime.Now.Date, 4, new List<BonosItemDTO>());
            esperadoCompraBonoDetailDTO.ItemsBono.Add(new BonosItemDTO(1, 4, 2, "Bono1", "Completo", 1));

            var controller = new CompraBonosController(_context, logger);


            // Act
            var result = await controller.CrearCompra(compraBonosForCreate);

            //Assert
            //we check that the response type CreatedAtAction and obtain the purchase created

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var purchaseDetailsActual = Assert.IsType<BonosDetailDTO>(createdResult.Value);

            purchaseDetailsActual.FechaCompra = purchaseDetailsActual.FechaCompra.Date;
            purchaseDetailsActual.PrecioTotal = esperadoCompraBonoDetailDTO.PrecioTotal;


            //we check that the expected and actual are the same

            Assert.Equal(esperadoCompraBonoDetailDTO, purchaseDetailsActual);

        }


    }
}
