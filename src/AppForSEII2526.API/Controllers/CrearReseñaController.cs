using AppForSEII2526.API.DTOs.ReseñaDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrearReseñaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CrearReseñaController> _logger;

        public CrearReseñaController(ApplicationDbContext context, ILogger<CrearReseñaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //GET PARA MOSTRAR LO DEL PASO 7
        // Nombre Usuario, titulo reseña, descripcion, fecha de creacion, valoracion general
        // y los bocadillos reseñados (nombre, precio, tamano y puntuacion).
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReseñaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCrearReseña(int id)
        {
            if (_context.Resenya == null)
            {
                _logger.LogError("La tabla de reseñas no existe.");
                return NotFound();
            }

            var reseña = await _context.Resenya
                .Where(r => r.Id == id)
                .Include(r => r.ResenyaBocadillos)
                .ThenInclude(rb => rb.Bocadillo)
                .ThenInclude(b => b.TipoPan)
                .Select(r => new ReseñaDetailDTO
                (r.Id, r.NombreUsuario, r.Titulo, r.Descripcion, r.FechaPublicacion, r.Valoracion,
                r.ResenyaBocadillos.Select(rb => new ReseñaItemDTO(rb.BocadilloId, rb.Bocadillo.Nombre, rb.Bocadillo.PVP, rb.Bocadillo.Tamano, rb.Puntuacion)).ToList()
                                ))
                .FirstOrDefaultAsync();

            if (reseña == null)
            {
                _logger.LogWarning("Reseña con id {Id} no encontrada.", id);
                return NotFound();
            }

            return Ok(reseña);
        }

        //POST PARA MOSTRAR LO DEL PASO 5
        // Nombre Usuario, titulo reseña, descripcion, fecha de creacion, valoracion general
        // y los bocadillos reseñados (nombre, precio, tamano y puntuacion).
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReseñaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReseña([FromBody] ReseñaCreateDTO dto)
        {
            // 🔍 Validaciones automáticas de DataAnnotations
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            // 🔍 Flujo alternativo 1 – Paso 3: no hay bocadillos seleccionados
            if (dto.ReseñaItemDTOs == null || dto.ReseñaItemDTOs.Count == 0)
            {
                ModelState.AddModelError("Bocadillos", "Debe seleccionar al menos un bocadillo para hacer una reseña.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            // 🔍 Validar puntuaciones 1–10 (aunque ya tienes [Range], doble capa por seguridad)
            foreach (var item in dto.ReseñaItemDTOs)
            {
                if (item.Puntuacion < 1 || item.Puntuacion > 10)
                {
                    ModelState.AddModelError("Puntuacion", "La puntuación por bocadillo debe estar entre 1 y 10.");
                }
            }
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            // 🔍 Validar existencia real de los bocadillos en base de datos
            var idsBocadillos = dto.ReseñaItemDTOs.Select(x => x.Id).ToList();

            var bocadillos = await _context.Bocadillo
                .Include(b => b.TipoPan)
                .Where(b => idsBocadillos.Contains(b.Id))
                .ToListAsync();

            if (bocadillos.Count != idsBocadillos.Count)
            {
                ModelState.AddModelError("Bocadillos", "Alguno de los bocadillos seleccionados no existe en la base de datos.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == dto.NombreUsuario);

            if (usuario == null)
            {
                ModelState.AddModelError("Usuario", "El usuario especificado no existe.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            // 🆕 Crear la entidad Resenya
            var nuevaResenya = new Resenya
            {
                NombreUsuario = dto.NombreUsuario,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Valoracion = dto.Valoracion,
                FechaPublicacion = DateTime.Now,
                ApplicationUser = usuario,
                ResenyaBocadillos = new List<ResenyaBocadillo>()
            };

            _context.Resenya.Add(nuevaResenya);

            try
            {
                // Guardar para generar el Id
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Conflict("Error al crear la reseña. Inténtelo más tarde.");
            }

            // 🧩 Crear relaciones en ResenyaBocadillo
            foreach (var item in dto.ReseñaItemDTOs)
            {
                nuevaResenya.ResenyaBocadillos.Add(new ResenyaBocadillo
                {
                    ResenyaId = nuevaResenya.Id,
                    BocadilloId = item.Id,
                    Puntuacion = item.Puntuacion
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Reseña", "Error al guardar la reseña. Inténtelo más tarde.");
                return Conflict("Error: " + ex.Message);
            }

            // 🧾 Construir DETAIL DTO para devolverlo al usuario
            var detalle = new ReseñaDetailDTO(
                nuevaResenya.Id,
                nuevaResenya.NombreUsuario,
                nuevaResenya.Titulo,
                nuevaResenya.Descripcion,
                nuevaResenya.FechaPublicacion,
                nuevaResenya.Valoracion,
                nuevaResenya.ResenyaBocadillos.Select(rb =>
                {
                    var b = bocadillos.First(x => x.Id == rb.BocadilloId);
                    return new ReseñaItemDTO(
                        rb.BocadilloId,
                        b.Nombre,
                        b.PVP,
                        b.Tamano,
                        rb.Puntuacion
                    );
                }).ToList()
            );

            return CreatedAtAction(nameof(GetCrearReseña), new { id = nuevaResenya.Id }, detalle);
        }

    }


}
