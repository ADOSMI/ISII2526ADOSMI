using AppForSEII2526.API.DTOs.Merch;

using AppForSEII2526.API.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using System.Net;



namespace AppForSEII2526.API.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class MerchController : ControllerBase

    {

        private readonly ApplicationDbContext _context;

        private readonly ILogger<MerchController> _logger;



        public MerchController(ApplicationDbContext context, ILogger<MerchController> logger)

        {

            _context = context;

            _logger = logger;

        }



        //  Mostrar detalle de producto por ID 

        [HttpGet("details/{id}")]

        [ProducesResponseType(typeof(MerchDetailsDTO), (int)HttpStatusCode.OK)]

        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<MerchDetailsDTO>> GetDetails(int id)

        {

            var producto = await _context.Producto

                .Include(p => p.TipoProducto)

                .FirstOrDefaultAsync(p => p.ProductoId == id);



            if (producto == null)

            {

                _logger.LogWarning($"Producto con ID {id} no encontrado.");

                return NotFound($"No se encontró ningún producto con ID {id}.");

            }



            var dto = new MerchDetailsDTO(

                producto.ProductoId,

                producto.Nombre,

                producto.TipoProducto?.Nombre ?? "Sin tipo",

                producto.PVP,

                producto.Stock

            );



            return Ok(dto);

        }



        //  Crear una compra 

        [HttpPost("comprar")]

        [ProducesResponseType(typeof(MerchDetailsDTO), (int)HttpStatusCode.Created)]

        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult> PostCompra([FromBody] MerchCreateDTO compraDto)

        {

            if (!ModelState.IsValid)

                return BadRequest(new ValidationProblemDetails(ModelState));



            if (compraDto.Items == null || !compraDto.Items.Any())

            {

                ModelState.AddModelError("Items", "Debes seleccionar al menos un producto para comprar.");

                return BadRequest(new ValidationProblemDetails(ModelState));

            }



            double precioTotal = 0;

            var productosAActualizar = new List<Producto>();



            foreach (var item in compraDto.Items)

            {

                var producto = await _context.Producto

                    .Include(p => p.TipoProducto)

                    .FirstOrDefaultAsync(p => p.ProductoId == item.Id);



                if (producto == null)

                {

                    ModelState.AddModelError("Items", $"El producto con ID {item.Id} no existe.");

                    return BadRequest(new ValidationProblemDetails(ModelState));

                }



                if (producto.Stock < item.Cantidad)

                {

                    ModelState.AddModelError("Items", $"No hay stock suficiente para {producto.Nombre} (Stock disponible: {producto.Stock}).");

                    return BadRequest(new ValidationProblemDetails(ModelState));

                }



                // Actualizar stock y calcular precio total 

                producto.Stock -= item.Cantidad;

                productosAActualizar.Add(producto);

                precioTotal += producto.PVP * item.Cantidad;

            }



            await _context.SaveChangesAsync();



            _logger.LogInformation($"Compra realizada por {compraDto.Nombre} {compraDto.Apellido1}. Total: {precioTotal}€");



            // Devuelve un MerchDetailsDTO del primer producto comprado como ejemplo 

            var primerProducto = productosAActualizar.First();

            var detalle = new MerchDetailsDTO(

                primerProducto.ProductoId,

                primerProducto.Nombre,

                primerProducto.TipoProducto?.Nombre ?? "Sin tipo",

                primerProducto.PVP,

                primerProducto.Stock

            );



            return CreatedAtAction(nameof(GetDetails), new { id = primerProducto.ProductoId }, detalle);

        }

    }

}
