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
    
    public class GetDetails_DirectTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MerchController _controller;

        public GetDetails_DirectTests()
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
                new Producto(3, "Gorra BATWRLD", 19.99, 8, 3, tipoGorra)
            };

            _context.AddRange(tipoCamiseta, tipoSudadera, tipoGorra);
            _context.AddRange(productos);
            _context.SaveChanges();

            var mockLogger = new Mock<ILogger<MerchController>>();
            _controller = new MerchController(_context, mockLogger.Object);
        }

        [Fact(DisplayName = "GetDetails devuelve el producto correcto para un ID existente")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetails_ReturnsValidProduct()
        {
            var response = await _controller.GetDetails(1);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var dto = Assert.IsAssignableFrom<MerchDetailsDTO>(result.Value);

            Assert.Equal(1, dto.Id);
            Assert.Equal("Camiseta Negra", dto.Nombre);
            Assert.Equal("Camiseta", dto.Tipo);
            Assert.Equal(25.99, dto.PVP);
            Assert.Equal(15, dto.Stock);
            Assert.Empty(dto.Items);
        }

        [Fact(DisplayName = "GetDetails devuelve NotFound si el producto no existe")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetails_ReturnsNotFound()
        {
            var response = await _controller.GetDetails(99);
            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No se encontró ningún producto con ID 99.", result.Value);
        }

        [Fact(DisplayName = "GetDetails incluye correctamente tipo, precio y stock")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetails_ReturnsCorrectDTOData()
        {
            var response = await _controller.GetDetails(2);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            var dto = Assert.IsAssignableFrom<MerchDetailsDTO>(result.Value);

            Assert.Equal("Sudadera", dto.Tipo);
            Assert.Equal("Sudadera Oversize", dto.Nombre);
            Assert.Equal(49.99, dto.PVP);
            Assert.Equal(10, dto.Stock);
        }
    }
}


