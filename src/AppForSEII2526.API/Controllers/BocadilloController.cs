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

        //CONTROLADOR BOCADILLOS
        [HttpGet]
        [Route("actionBocadillos")]
        [ProducesResponseType(typeof(IList<BocadilloForReseñaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBocadillosForReseña(string? nombre, double? pvp)
        {
            IList<BocadilloForReseñaDTO> bocadillos = await _context
            .Bocadillo
            .Where(b => (b.Nombre.Equals(nombre) || nombre == null)
            && (b.PVP.Equals(pvp) || pvp == null)) //Filtros por numero de bocadillos y por tipo 
            .Select(b => new BocadilloForReseñaDTO(b.Nombre, b.PVP, b.Tamano, b.TipoPan)) //Select con lo que mostraremos
            .ToListAsync();
            return Ok(bocadillos);
        }
    }
}
