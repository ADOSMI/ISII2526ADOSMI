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
    /// <summary>
    /// Pruebas unitarias del método GetSelect() del MerchSelectController (Paso 2).
    /// </summary>
    public class GetSelect_DirectTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MerchSelectController _controller;

        public GetSelect_DirectTests()
        {
            // Base de datos en memoria única para cada ejecución
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Crear datos iniciales
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

        // Devuelve todos los productos disponibles
        [Fact(DisplayName = "GetSelect devuelve todos los productos disponibles (sin filtros)")]
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

        // Filtrado por tipo (Camiseta)
        [Fact(DisplayName = "GetSelect filtra correctamente por tipo de producto")]
        public async Task GetSelect_FilterByTipo()
        {
            var response = await _controller.GetSelect("Camiseta", null);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            var productos = Assert.IsAssignableFrom<List<MerchSelectDTO>>(result.Value);

            Assert.Equal(2, productos.Count);
            Assert.All(productos, p => Assert.Equal("Camiseta", p.Tipo));
        }

        //  Filtrado por precio máximo
        [Fact(DisplayName = "GetSelect filtra correctamente por precio máximo")]
        public async Task GetSelect_FilterByMaxPrice()
        {
            var response = await _controller.GetSelect(null, 25.00);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            var productos = Assert.IsAssignableFrom<List<MerchSelectDTO>>(result.Value);

            Assert.All(productos, p => Assert.True(p.PVP <= 25.00));
        }

        //  Filtrado combinado (tipo y precio)
        [Fact(DisplayName = "GetSelect aplica correctamente ambos filtros combinados")]
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

        //  Filtro sin coincidencias
        [Fact(DisplayName = "GetSelect devuelve NotFound si no hay productos coincidentes")]
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




