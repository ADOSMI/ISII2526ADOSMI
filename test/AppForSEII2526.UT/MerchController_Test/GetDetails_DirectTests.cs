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
    /// <summary>
    /// Pruebas unitarias del método GetDetails() del MerchController (Paso 7).
    /// </summary>
    public class GetDetails_DirectTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MerchController _controller;

        public GetDetails_DirectTests()
        {
            // BD en memoria nueva para cada prueba
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Crear datos de prueba
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

        // ✅ Caso 1: Devuelve correctamente el detalle del producto con ID válido
        [Fact(DisplayName = "GetDetails devuelve el producto correcto para un ID existente")]
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
            Assert.Empty(dto.Items); // En tu DTO los items se inicializan vacíos
        }

        // ⚠️ Caso 2: Producto no encontrado
        [Fact(DisplayName = "GetDetails devuelve NotFound si el producto no existe")]
        public async Task GetDetails_ReturnsNotFound()
        {
            var response = await _controller.GetDetails(99);
            var result = response.Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No se encontró ningún producto con ID 99.", result.Value);
        }

        // 🧩 Caso 3: Comprueba que el tipo y los datos del DTO son correctos
        [Fact(DisplayName = "GetDetails incluye correctamente tipo, precio y stock")]
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

        // 🧱 Caso 4: Producto con stock 0 también se muestra correctamente
        [Fact(DisplayName = "GetDetails devuelve el producto incluso si tiene stock 0")]
        public async Task GetDetails_StockZeroIsVisible()
        {
            var tipoCamiseta = _context.TipoProducto.First();
            var productoSinStock = new Producto(10, "Camiseta sin stock", 30.00, 0, 1, tipoCamiseta);

            _context.Producto.Add(productoSinStock);
            _context.SaveChanges();

            var response = await _controller.GetDetails(10);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            var dto = Assert.IsAssignableFrom<MerchDetailsDTO>(result.Value);

            Assert.Equal("Camiseta sin stock", dto.Nombre);
            Assert.Equal(0, dto.Stock);
        }

        // 🧠 Caso 5: El DTO devuelto tiene todos los campos esperados inicializados
        [Fact(DisplayName = "GetDetails devuelve un MerchDetailsDTO completo y válido")]
        public async Task GetDetails_ReturnsFullDTO()
        {
            var response = await _controller.GetDetails(3);
            var result = response.Result as OkObjectResult;

            Assert.NotNull(result);
            var dto = Assert.IsAssignableFrom<MerchDetailsDTO>(result.Value);

            Assert.NotNull(dto.Nombre);
            Assert.NotNull(dto.Tipo);
            Assert.True(dto.PVP > 0);
            Assert.True(dto.Stock >= 0);
            Assert.NotNull(dto.Items); // Tus DTOs inicializan Items siempre
        }
    }
}


