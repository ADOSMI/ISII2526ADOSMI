using AppForSEII2526.API.DTOs.BonosDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BonosController> _logger;

        public BonosController(ApplicationDbContext context, ILogger<BonosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //CONTROLADOR CASO DE USO: COMPRAR BONOS
        [HttpGet]
        [Route("actionCompraBono")]
        [ProducesResponseType(typeof(IList<BonosForCompraDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBonosForCompra(string? nombre,string? tipobocadillo)
        {
            IList<BonosForCompraDTO> bonos = await _context
            .BonoBocadillo
            .Include(b => b.TipoBocadillo)
            .Where(b => b.Nombre.Equals(nombre) || nombre == null
            && (b.TipoBocadillo.Nombre.Equals(tipobocadillo) || tipobocadillo == null)) //Filtros por numero de bocadillos y por tipo 
            .Select(b => new BonosForCompraDTO(b.Nombre, b.PrecioPorBono, b.NumeroBocadillos, b.TipoBocadillo)) //Select con lo que mostraremos
            .ToListAsync();
            return Ok(bonos);
        }
    }
}
