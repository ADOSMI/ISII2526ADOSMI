using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.Merch;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AppForSEII2526.UT.MerchSelectController_test
{
    public class GetSelect_DirectTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MerchSelectController _controller;

        public GetSelect_DirectTests()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            
            var tipos = new List<TipoProducto>()
            {
                new TipoProducto(1, "Camiseta"),
                new TipoProducto(2, "Sudadera"),
                new TipoProducto(3, "Gorra")
            };

            var productos = new List<Producto>()
            {
                new Producto(1, "Camiseta Negra", 25.99, 15, 1, tipos[0]),
                new Producto(2, "Sudadera Oversize", 49.99, 10, 2, tipos[1]),
                new Producto(3, "Gorra BATWRLD", 19.99, 8, 3, tipos[2]),
                new Producto(4, "Camiseta Blanca", 19.50, 12, 1, tipos[0])
            };

            _context.AddRange(tipos);
            _context.AddRange(productos);
            _context.SaveChanges();

            var mockLogger = new Mock<ILogger<MerchSelectController>>();
            _controller = new MerchSelectController(_context, mockLogger.Object);
        }

        
        [Fact(DisplayName = "GetSelect devuelve todos los productos disponibles (sin filtros)")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetSelect_ReturnsAllProducts()
        {
            var response = await _controller.GetSelect(null, null);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var productos = Assert.IsAssignableFrom<List<MerchSelectDTO>>(result.Value);

            Assert.Equal(4, productos.Count);
            Assert.All(productos, p => Assert.True(p.Stock > 0));
        }


        
        [Fact(DisplayName = "GetSelect aplica correctamente ambos filtros combinados")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetSelect_FilterByTipoAndMaxPrice()
        {
            var response = await _controller.GetSelect("Camiseta", 20.00);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            var productos = Assert.IsAssignableFrom<List<MerchSelectDTO>>(result.Value);

            Assert.Single(productos);
            var producto = productos.First();

            Assert.Equal("Camiseta Blanca", producto.Nombre);
            Assert.Equal("Camiseta", producto.Tipo);
            Assert.True(producto.PVP <= 20.00);
        }

        
        [Fact(DisplayName = "GetSelect devuelve NotFound si no hay productos coincidentes")]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetSelect_ReturnsNotFound()
        {
            var response = await _controller.GetSelect("Zapatos", null);
            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No hay productos disponibles actualmente.", result.Value);
        }
    }
}




