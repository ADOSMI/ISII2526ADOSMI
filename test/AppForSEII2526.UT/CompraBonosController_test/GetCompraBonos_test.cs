using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.BonosDTOs;
using AppForSEII2526.API.DTOs.CompraBonosDTOs;
using AppForSEII2526.API.Models;
using AppForSEII2526.UT;
using System;
using System.Linq.Expressions;
using System.Net.WebSockets;

namespace AppForSEII2526.UT.CompraBonosController_test
{
    public class GetCompraBonos_test : AppForSEII25264SqliteUT
    {
        public GetCompraBonos_test()
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
                new BonoBocadillo(tipoBocadillo[1],3,5,3,"Bono3",3,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[2],4,5,4,"Bono3",4,new List<BonosComprados>()),
            };

            var compraBono = new CompraBono(null, "Adolfo", "Escribano", "Martinez", 1, new DateTime(2025, 11, 19), EnumMetodosPago.PayPal, 5, 10, new List<BonosComprados>());
            var bonoComprado = new BonosComprados(1, 1, 1, 2, bonos[0], compraBono);

            compraBono.BonosComprados.Add(bonoComprado);

            _context.AddRange(applicationUser);
            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.AddRange(compraBono);
            _context.SaveChanges();


        }
       

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetCompraBonos_Encontrada_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;
            var controller = new CompraBonosController(_context, logger);

            var itemsCompraBonos = new List<BonosItemDTO>
            {
                new BonosItemDTO(1,2,2,"Bono1","Completo",5)
            };
            var precioTotal = itemsCompraBonos.Sum(icb => icb.PrecioPorBono * icb.Cantidad); //necesitamos calcular el precio de la lista de Items para pasarselo al constructor y que se pueda mostrar en el Select
            var compraEsperada = new BonosDetailDTO(1,"Adolfo","Escribano","Martinez",EnumMetodosPago.PayPal, new DateTime(2025, 11, 19),precioTotal,itemsCompraBonos);

            // Act
            var result = await controller.GetCompraBono(1);

            //Assert
            //Comprobamos que el tipo de respuesta es OK y obtenemos la lista de videojuegos
            var okResult = Assert.IsType<OkObjectResult>(result);
            var compraBonosDTOActual = Assert.IsType<BonosDetailDTO>(okResult.Value);

            //Comprobamos que lo esperado y lo actual coinciden
            Assert.Equal(compraEsperada, compraBonosDTOActual);
            



        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetCompra_NoEncontrada_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;

            var controller = new CompraBonosController(_context, logger);

            // Act
            var result = await controller.GetCompraBono(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

    }
}
