using AppForSEII2526.API.DTOs.Merch;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MerchSelectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MerchSelectController> _logger;

        public MerchSelectController(ApplicationDbContext context, ILogger<MerchSelectController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MerchSelectDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<MerchSelectDTO>>> GetSelect([FromQuery] string? tipo, [FromQuery] double? maxPrecio)
        {
            try
            {
                var query = _context.Producto
                    .Include(p => p.TipoProducto)
                    .Where(p => p.Stock > 0);
                

                if (!string.IsNullOrWhiteSpace(tipo))
                    query = query.Where(p => p.TipoProducto.Nombre.Contains(tipo));

                
                if (maxPrecio.HasValue)
                    query = query.Where(p => p.PVP <= maxPrecio.Value);
                

                var productos = await query
                    .OrderBy(p => p.Nombre)
                    .Select(p => new MerchSelectDTO(
                        p.ProductoId,
                        p.Nombre,
                        p.TipoProducto != null ? p.TipoProducto.Nombre : "Sin tipo",
                        p.PVP,
                        p.Stock
                    ))
                    .ToListAsync();

                
                if (!productos.Any())
                {
                    _logger.LogWarning("No hay productos disponibles o coincidentes con los filtros.");
                    return NotFound("No hay productos disponibles actualmente.");
                }

                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos de merchandising.");
                return StatusCode(500, "Error interno del servidor al obtener los productos.");
            }
        }
    }
}

