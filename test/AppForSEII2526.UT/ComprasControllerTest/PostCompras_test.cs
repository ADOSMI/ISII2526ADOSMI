using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.CompraBocadilloDTOs;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprasControllerTest
{
    public class PostCompras_test : AppForSEII25264SqliteUT
    {
        public PostCompras_test()
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

        public static IEnumerable<object[]> TestCasesFor_CreateCompra()
        {
            var rentalItemVacio = new CompraForCreateDTO("Alberto", "Cuenca",
                "Aleman", AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta,
                new List<CompraItemDTO>());

            var compraItems = new List<CompraItemDTO>() { new CompraItemDTO(1, "Bocadillo vegetal", 4.99, "Vegano", 2) };

            var compraNoApplicationUser = new CompraForCreateDTO("ZZZZZ", "Cuenca",
                "Aleman", AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta,
                compraItems);


            var compraItemsIdMal = new List<CompraItemDTO>() { new CompraItemDTO(111, "Bocadillo vegetal", 4.99, "Vegano", 2) };

            var rentalItemIdNoexiste = new CompraForCreateDTO("Alberto", "Cuenca",
                "Aleman", AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta,
                compraItemsIdMal);

            var compraItemsStockSuperior = new List<CompraItemDTO>() { new CompraItemDTO(2, "Bocadillo vegetal", 4.99, "Vegano", 200) };

            var rentalItemIdStockSuperior = new CompraForCreateDTO("Alberto", "Cuenca",
                "Aleman", AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta,
                compraItemsStockSuperior);




            var allTests = new List<object[]>
            {             //input for createCompra - Error expected
                new object[] { rentalItemVacio, "Error. Tienes que añadir al menos un bocadillo para realizar la compra"},
                new object[] { compraNoApplicationUser, "Error! UserName is not registered"},
                new object[] { rentalItemIdNoexiste, "Error, el bocadillo con ID 111 no existe"},
                new object[] { rentalItemIdStockSuperior, "Error, Bocadillo vegetal solo tiene 20 unidades disponibles pero 200 fueron seleccionadas" }
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateCompra))]
        public async Task CreateRental_Error_test(CompraForCreateDTO compraDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);

            // Act
            var result = await controller.CreateCompra(compraDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateCompra_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);

            var compraDTO = new CompraForCreateDTO("Alberto", "Cuenca",
                "Aleman", AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta,
                new List<CompraItemDTO>() { new CompraItemDTO(2, "Bocadillo vegetal", 4.99, "Vegano", 2) });

            var expectedcompraDetailDTO = new CompraDetailDTO(2, "Alberto",
                "Cuenca", "Aleman", AppForSEII2526.API.Models.EnumMetodosPago.Tarjeta,
                DateTime.Now, new List<CompraItemDTO>()
                { new CompraItemDTO(2, "Bocadillo vegetal", 4.99, "Vegano", 2)});

            // Act
            var result = await controller.CreateCompra(compraDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualCompraDetailDTO = Assert.IsType<CompraDetailDTO>(createdResult.Value);

            Assert.Equal(expectedcompraDetailDTO, actualCompraDetailDTO);

        }

    }
}

