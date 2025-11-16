using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.CompraBocadilloDTOs;
using AppForSEII2526.API.DTOs.ComprasDTOs;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprasControllerTest
{
    public class GetCompras_test : AppForSEII25264SqliteUT
    {
        public GetCompras_test()
        {

            var tiposPan = new List<TipoPan>()
            {
                new TipoPan(1,"Vegetariano"),
                new TipoPan(2,"Mixto"),
                new TipoPan(3, "Vegano")
            };

            var tamanos = new List<Tamano>()
            {
                Tamano.Grande,
                Tamano.Normal
            };

            var bocadillos = new List<Bocadillo>(){
                new Bocadillo(1,"Bocadillo de jamón", 5.99, 34, tiposPan[0], tamanos[1]),
                new Bocadillo(2,"Bocadillo vegetal", 4.99, 20, tiposPan[2], tamanos[0]),
                new Bocadillo(3,"Bocadillo mixto", 6.49, 15, tiposPan[1], tamanos[0])
            };

            ApplicationUser user = new ApplicationUser("1", "Alberto", "Cuenca", "alberto@gmail.com", "Alberto");

            var compra = new Compra(DateTime.Now, 2, new List<CompraBocadillo>(),
                AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta, user, "Alberto", "Cuenca", "Aleman");

            compra.CompraBocadillo.Add(new CompraBocadillo(bocadillos[0], 2, compra));

            _context.Users.Add(user);
            _context.AddRange(tiposPan);
            _context.AddRange(bocadillos);
            _context.Add(compra);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCompra_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);

            // Act
            var result = await controller.GetCompra(0);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetRental_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;
            var controller = new ComprasController(_context, logger);


            var expectedCompra = new CompraDetailDTO(1, "Alberto", "Cuenca", "Aleman",
                            AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta, DateTime.Now,
                            new List<CompraItemDTO>());
            expectedCompra.CompraItems.Add(new CompraItemDTO(1, "Bocadillo de jamón", 5.99, "Vegetariano", 2));

            // Act 
            var result = await controller.GetCompra(1);

            //Assert
            //we check that the response type is OK and obtain the purcharse
            var okResult = Assert.IsType<OkObjectResult>(result);
            var compraDTOActual = Assert.IsType<CompraDetailDTO>(okResult.Value);
            var eq = expectedCompra.Equals(compraDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedCompra, compraDTOActual);

        }
    }
}
