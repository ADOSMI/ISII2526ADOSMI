using AppForSEII2526.API.DTOs.BocadilloDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BocadillosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BocadillosController> _logger;

        public BocadillosController(ApplicationDbContext context, ILogger<BocadillosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<BocadilloDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBocadillosForCompra( string? nombre, Tamano? tamano, TipoPan? tipoPan, double? precio)
        {
            var bocadillos = await _context.Bocadillo
                .Include(b=>b.TipoPan)
                .Include(b=>b.CompraBocadillo)
                    .ThenInclude(cb=>cb.Compra)
                .Where(b=>((b.Nombre.Contains(nombre)) || (nombre==null))
                    && ((b.TipoPan.Nombre.Equals(tipoPan)) || (tipoPan==null))
                    && ((b.PVP <= precio) || (precio==null))
                    && ((b.Tamano == tamano) || (tamano==null)))
                .OrderBy(b=>b.Nombre)
                    .ThenBy(b => b.PVP)
                .Select(b=>new BocadilloDTO(b.Id, b.Nombre, b.Tamano, b.TipoPan, b.PVP)
                    ).ToListAsync();
            return Ok(bocadillos);
        }

    }
}
