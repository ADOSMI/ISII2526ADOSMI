using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.Merch;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AppForSEII2526.UT.MerchController_test
{
    public class PostCompraTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MerchController _controller;

        public PostCompraTests()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            
            var tipoCamiseta = new TipoProducto(1, "Camiseta");
            var tipoSudadera = new TipoProducto(2, "Sudadera");
            var tipoGorra = new TipoProducto(3, "Gorra");

            var productos = new List<Producto>()
            {
                new Producto(1, "Camiseta Negra", 25.99, 15, 1, tipoCamiseta),
                new Producto(2, "Sudadera Oversize", 49.99, 10, 2, tipoSudadera),
                new Producto(3, "Gorra BATWRLD", 19.99, 0, 3, tipoGorra)
            };

            _context.AddRange(tipoCamiseta, tipoSudadera, tipoGorra);
            _context.AddRange(productos);
            _context.SaveChanges();

            var mockLogger = new Mock<ILogger<MerchController>>();
            _controller = new MerchController(_context, mockLogger.Object);
        }

        [Fact(DisplayName = "PostCompra crea una compra válida y devuelve Created con MerchDetailsDTO")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task PostCompra_ReturnsCreatedAndValidDTO()
        {
            var dto = new MerchCreateDTO(
                "Lucía",
                "Fernández",
                "Pérez",
                "Calle Luna 12",
                "Tarjeta de crédito",
                new List<MerchItemDTO>
                {
                    new MerchItemDTO(1, "Camiseta Negra", "Camiseta", 25.99, 2)
                });

            var response = await _controller.PostCompra(dto);
            var result = response as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            var dtoResult = Assert.IsAssignableFrom<MerchDetailsDTO>(result.Value);
            Assert.Equal("Camiseta Negra", dtoResult.Nombre);
            Assert.Equal("Camiseta", dtoResult.Tipo);
            Assert.True(dtoResult.Stock >= 0);
            Assert.True(dtoResult.PVP > 0);
        }

        [Fact(DisplayName = "PostCompra devuelve BadRequest si no hay stock suficiente")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task PostCompra_InsufficientStock_ReturnsBadRequest()
        {
            var dto = new MerchCreateDTO(
                "Lucía",
                "Fernández",
                "Pérez",
                "Calle Luna 12",
                "Tarjeta de crédito",
                new List<MerchItemDTO>
                {
                    new MerchItemDTO(3, "Gorra BATWRLD", "Gorra", 19.99, 2)
                });

            var response = await _controller.PostCompra(dto);
            var result = Assert.IsType<BadRequestObjectResult>(response);
            var problem = Assert.IsType<ValidationProblemDetails>(result.Value);

            var error = problem.Errors.First().Value[0];
            Assert.Contains("No hay stock suficiente", error, StringComparison.OrdinalIgnoreCase);
        }

  
        [Fact(DisplayName = "PostCompra devuelve BadRequest si el modelo no es válido")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [Trait("Database", "WithoutFixture")]
        public async Task PostCompra_InvalidModel_ReturnsBadRequest()
        {
            var dto = new MerchCreateDTO(
                "", 
                "Fernández",
                "Pérez",
                "Calle Luna 12",
                "Tarjeta de crédito",
                new List<MerchItemDTO>
                {
                    new MerchItemDTO(1, "Camiseta Negra", "Camiseta", 25.99, 1)
                });

            _controller.ModelState.AddModelError("Nombre", "El nombre es obligatorio.");

            var response = await _controller.PostCompra(dto);
            var result = Assert.IsType<BadRequestObjectResult>(response);
            var details = Assert.IsType<ValidationProblemDetails>(result.Value);

            var error = details.Errors.First().Value[0];
            Assert.Contains("obligatorio", error, StringComparison.OrdinalIgnoreCase);
        }
    }
}

