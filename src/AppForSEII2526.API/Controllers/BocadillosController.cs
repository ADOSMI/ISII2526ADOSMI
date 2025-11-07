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
        public async Task<ActionResult> GetBocadillosForCompra(Tamano? tamano, String? tipoPan)
        {
            var bocadillos = await _context.Bocadillo
                .Include(b=>b.TipoPan)
                .Where(b=>(((b.TipoPan.Nombre.Equals(tipoPan)) || (tipoPan==null))
                    && ((b.Tamano.Equals(tamano)) || (tamano==null))))
                .Select(b=>new BocadilloDTO(b.Id, b.Nombre, b.Tamano, b.TipoPan, b.PVP)
                    ).ToListAsync();
            return Ok(bocadillos);
        }

    }
}
