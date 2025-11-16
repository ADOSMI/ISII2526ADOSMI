

using AppForSEII2526.API.DTOs.Merch;



using AppForSEII2526.API.Models;



using Microsoft.AspNetCore.Mvc;



using Microsoft.EntityFrameworkCore;



using System;







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







        /// <summary>  



        /// Paso 7 del CU “Comprar Merch”: muestra los detalles completos del producto seleccionado.  



        /// Flujo alternativo 0 → si no existe el producto, devuelve 404.  



        /// </summary>  



        /// <param name="id">Identificador del producto.</param>  



        [HttpGet("details/{id}")]



        [ProducesResponseType(typeof(MerchDetailsDTO), 200)]



        [ProducesResponseType(404)]



        public async Task<ActionResult<MerchDetailsDTO>> GetDetails(int id)



        {



            try



            {



                // Buscar el producto e incluir su tipo  



                var producto = await _context.Producto



                    .Include(p => p.TipoProducto)



                    .FirstOrDefaultAsync(p => p.ProductoId == id);







                // Flujo alternativo 0 – producto no encontrado  



                if (producto == null)



                {



                    _logger.LogWarning($"Producto con ID {id} no encontrado.");



                    return NotFound($"No se encontró ningún producto con ID {id}.");



                }







                // Crear el DTO de respuesta  



                var dto = new MerchDetailsDTO(



                    producto.ProductoId,



                    producto.Nombre,



                    producto.TipoProducto?.Nombre ?? "Sin tipo",



                    producto.PVP,



                    producto.Stock



                );







                return Ok(dto);



            }



            catch (Exception ex)



            {



                _logger.LogError(ex, "Error al obtener los detalles del producto.");



                return StatusCode(500, "Error interno del servidor al obtener los detalles del producto.");



            }



        }



    }



}
