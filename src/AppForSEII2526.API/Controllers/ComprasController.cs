using AppForSEII2526.API.Data;
using AppForSEII2526.API.DTOs.CompraBocadilloDTOs;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ApplicationDbContext context, ILogger<ComprasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCompra(int id)
        {
            if (_context.Compra == null)
            {
                _logger.LogError("Error: Compra tabla no existe");
                return NotFound();
            }

            var compra = await _context.Compra
             .Where(c => c.Id == id)
                 .Include(c => c.CompraBocadillo) //join table CompraBocadillo
                    .ThenInclude(ci => ci.Bocadillo) //then join table Bocadillo
                        .ThenInclude(bcdll => bcdll.TipoPan) //then join table TipoPan
             .Select(c => new CompraDetailDTO(c.Id, c.NombreCliente,
                    c.Apellido_1Cliente, c.Apellido_2Cliente,
                    (EnumMetodosPago)c.MetodoPago, c.FechaCompra,
                    c.CompraBocadillo
                        .Select(cb => new CompraItemDTO(cb.Bocadillo.Id,
                                cb.Bocadillo.Nombre, cb.Bocadillo.PVP,
                                cb.Bocadillo.TipoPan.Nombre, cb.Cantidad)).ToList<CompraItemDTO>()))
             .FirstOrDefaultAsync();


            if (compra == null)
            {
                _logger.LogError($"Error: La compra con id {id} no existe");
                return NotFound();
            }


            return Ok(compra);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateCompra(CompraForCreateDTO compraForCreate)
        {
            if (compraForCreate.CompraItems.Count == 0)
                ModelState.AddModelError("CompraItems", "Error. Tienes que añadir al menos un bocadillo para realizar la compra");

            // if (!_context.ApplicationUsers.Any(au=>au.UserName==rentalForCreate.CustomerUserName))
            var user = _context.Users.FirstOrDefault(au => au.UserName == compraForCreate.NombreCliente);
            if (user == null)
                ModelState.AddModelError("CompraApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var bocadilloNombres = compraForCreate.CompraItems.Select(ci => ci.Nombre).ToList<string>();

            var bocadillos = _context.Bocadillo.Include(b => b.CompraBocadillo)
                .ThenInclude(cb => cb.Compra)
                .Where(b => bocadilloNombres.Contains(b.Nombre))

                //we use an anonymous type https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types
                .Select(b => new {
                    b.Id,
                    b.Nombre,
                    b.Tamano,
                    b.PVP
                })
                .ToList();


            Compra compra = new Compra(
                DateTime.Now,
                compraForCreate.CompraItems.Count,
                new List<CompraBocadillo>(),
                (AppForSEII2526.API.Models.EnumMetodosPago)compraForCreate.EnumeracionMetodosPago,
                user,
                compraForCreate.NombreCliente,
                compraForCreate.Apellido1Cliente,
                compraForCreate.Apellido2Cliente
                );


            compra.PrecioTotal = 0;


            Bocadillo bocadillo;
            foreach (var item in compraForCreate.CompraItems)
            {
                bocadillo = await _context.Bocadillo.FindAsync(item.BocadilloID);
                if (bocadillo == null)
                {
                    ModelState.AddModelError("CompraItems", $"Error, el bocadillo con ID {item.BocadilloID} no existe");

                }
                else
                {
                    if (bocadillo.Stock < item.Cantidad)
                    {
                        ModelState.AddModelError("CompraItems", $"Error, {bocadillo.Nombre} solo tiene {bocadillo.Stock} unidades disponibles pero {item.Cantidad} fueron seleccionadas");
                    }
                    else
                    {
                        //we decrease the number of movies available
                        bocadillo.Stock -= item.Cantidad;
                        compra.CompraBocadillo.Add(new CompraBocadillo(bocadillo, item.Cantidad, compra));
                    }
                }

            }

            //if there is any problem because of the available quantity of movies or because the movie does not exist
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            compra.PrecioTotal = compra.CompraBocadillo.Sum(cb => cb.Cantidad * cb.Precio);

            _context.Add(compra);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                /*this condition can only be checked by means of Integration Testing
                ModelState.AddModelError("SaveChangesError", "Errors while saving the purchase");
                _logger.LogCritical("Exception will saving the purchase" + ex.Message);
                return Conflict(new ValidationProblemDetails(ModelState));*/
                return Conflict(new { error = ex.Message, inner = ex.InnerException?.Message });
            }

            //it returns purchasedetail
            var compraDetail = new CompraDetailDTO(compra.Id, compra.NombreCliente,
                compra.Apellido_1Cliente, compra.Apellido_2Cliente,
                compraForCreate.EnumeracionMetodosPago, compra.FechaCompra,
                compraForCreate.CompraItems);

            return CreatedAtAction("GetCompra", new { id = compra.Id }, compraDetail);
        }

    }

}

