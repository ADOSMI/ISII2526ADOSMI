using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;
using AppForSEII2526.API.Data;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using AppForSEII2526.API.DTOs.CompraBonosDTOs;


namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraBonosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompraBonosController> _logger;

        public CompraBonosController(ApplicationDbContext context, ILogger<CompraBonosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        //GET PARA MOSTRAR LO DEL PASO 7
        //nombre y apellidos del bono, metodo de pago, fecha de compra, precio total y los bonos adquiridos (nombre, tipo, precio y cantidad).
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BonosDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCompraBono(int id)
        {
            //if (_context.CompraBono == null)
            //{
            //    _logger.LogError("Error: Tabla Compra Bono no existe");
            //    return NotFound();
            //}

            var compraBonodto = await _context.CompraBono
             .Where(cb => cb.Id == id)
             .Include(cb => cb.BonosComprados) //join con la tabla BonosComprados
                    .ThenInclude(ib => ib.BonoBocadillo) // then join con la tabla BonoBocadillo
                        .ThenInclude(bb => bb.TipoBocadillo) //then join con la tabla TipoBocadillo
             .Select(b => new BonosDetailDTO(b.Id, b.Nombre, b.Apellido1, b.Apellido2, b.MetodoPago, b.FechaCompra, b.PrecioTotalBono,
                    b.BonosComprados
                        .Select(ib => new BonosItemDTO(ib.BonoBocadillo.Id, ib.BonoBocadillo.PrecioPorBono, ib.BonoBocadillo.NumeroBocadillos, ib.BonoBocadillo.Nombre, ib.BonoBocadillo.TipoBocadillo.Nombre, ib.BonoBocadillo.CantidadDisponible)).ToList<BonosItemDTO>()))
             .FirstOrDefaultAsync();


            if (compraBonodto == null)
            {
                _logger.LogError($"Error: Compra con id {id} no existe");
                return NotFound();
            }

            return Ok(compraBonodto);

        }

        //POST 
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BonosDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearCompra(BonosForCreateDTO compraBonosParaCrear)
        {


            //cualquier validación definida en BonosForCreate se comprueba antes de ejecutar el método, por lo que no es necesario comprobarlas

            //PARA COMPROBAR QUE SE HA SELECCIOANDO AL MENOS UN Bono- Flujo alternativo 2 - al Paso 4
            if (compraBonosParaCrear.ItemsCompraBono.Count == 0)
            {
                ModelState.AddModelError("ItemsCompraBono", "Error. Necesitas seleccionar al menos un bono para ser comprado.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            CompraBono compraBono = new CompraBono(compraBonosParaCrear.Nombre, compraBonosParaCrear.Apellido1, compraBonosParaCrear.Apellido2, DateTime.Now, compraBonosParaCrear.MetodoPago, new List<BonosComprados>());

            //debemos comprobar que hay suficiente cantidad para comprar en la base de datos
            BonoBocadillo bonoBocadillo;
            foreach (var item in compraBonosParaCrear.ItemsCompraBono)
            {
                bonoBocadillo = await _context.BonoBocadillo.FindAsync(item.Id);
                if (bonoBocadillo == null)
                {
                    ModelState.AddModelError("ItemsCompraBono", $"Error. Bono titulado {item.Nombre} con Id {item.Id} no existe en la base de datos");
                }
                else
                {
                    if (bonoBocadillo.CantidadDisponible < item.Cantidad) //COMPROBAR QUE HAY CANTIDAD SUFICIENTE DE UN BONO
                    {
                        ModelState.AddModelError("ItemsCompraBono", $"Error. Bono titulado {item.Nombre} tiene solo {bonoBocadillo.CantidadDisponible} unidades disponibles pero {item.Cantidad} fueron seleccionadas");
                    }
                    else

                    if (bonoBocadillo.PrecioPorBono == null || item.PrecioPorBono < 3)
                    {
                        ModelState.AddModelError("ItemsCompraBono", $"Error!. El precio debe ser mayor que 3");
                    }
                }

            }

            //si hay algún problema por la cantidad disponible de bonos o porque el bono no existe
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            compraBono.PrecioTotalBono = compraBono.BonosComprados.Sum(bc => bc.Cantidad * bc.PrecioBono);

            _context.CompraBono.Add(compraBono);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message, inner = ex.InnerException?.Message });
            }


            //devuelve CompraDetail

            var bonoDetail = new BonosDetailDTO(compraBono.Id, compraBono.Nombre, compraBono.Apellido1, compraBono.Apellido2, compraBonosParaCrear.MetodoPago, compraBono.FechaCompra, compraBono.PrecioTotalBono, compraBonosParaCrear.ItemsCompraBono);

            return CreatedAtAction("GetCompraBono", new { id = compraBono.Id }, bonoDetail);
        }

    }

}

