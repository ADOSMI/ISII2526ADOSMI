using AppForSEII2526.API.DTOs.BocadilloDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BocadilloController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BocadilloController> _logger;

        public BocadilloController(ApplicationDbContext context, ILogger<BocadilloController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [Route("actionBocadillos")]
        [ProducesResponseType(typeof(IList<BocadilloForReseñaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBocadillosForReseña(string? nombre, double? pvp)
        {
            IList<BocadilloForReseñaDTO> bocadillos = await _context
            .Bocadillo
            .Where(b => (b.Nombre.Equals(nombre) || nombre == null)
            && (b.PVP < pvp || pvp == null)) //Filtros por numero de bocadillos y por tipo 
            .Select(b => new BocadilloForReseñaDTO(b.Id, b.Nombre, b.PVP, b.Tamano, b.TipoPan.Nombre)) //Select con lo que mostraremos
            .ToListAsync();
            return Ok(bocadillos);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<BocadilloForCompraDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBocadillosForCompra(Tamano? tamano, string? tipoPan)
        {
            var bocadillos = await _context.Bocadillo
                .Include(b => b.TipoPan)
                .Where(b => ((b.TipoPan.Nombre.Equals(tipoPan) || tipoPan == null)
                    && (b.Tamano.Equals(tamano) || tamano == null) && b.Stock > 0))
                .Select(b => new BocadilloForCompraDTO(b.Id, b.Nombre, b.Tamano, b.TipoPan.Nombre, b.PVP)
                    ).ToListAsync();
            return Ok(bocadillos);
        }

    }
}
