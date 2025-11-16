using static Resenya;

namespace AppForSEII2526.API.DTOs.ReseñaDTOs
{
    public class ReseñaCreateDTO
    {
        public ReseñaCreateDTO()
        {

        }

        public ReseñaCreateDTO(string? nombreUsuario, string titulo, string descripcion, Valoracion_General valoracion, IList<ReseñaItemDTO> reseñaItemsDTOs)
        {
            NombreUsuario = nombreUsuario;
            Titulo = titulo;
            Descripcion = descripcion;
            Valoracion = valoracion;
            ReseñaItemDTOs = reseñaItemsDTOs;
        }


        [Required]
        [StringLength(250, ErrorMessage = "La descripción no puede ocupar más de 250 caracteres")]
        public string Descripcion { get; set; }


        [StringLength(50, ErrorMessage = "El nombre de usuario no puede ocupar más de 50 caracteres")]
        public string? NombreUsuario { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El título no puede ocupar más de 50 caracteres")]
        public string Titulo { get; set; }


        [Required]
        public Valoracion_General Valoracion { get; set; }

        [Required]
        public IList<ReseñaItemDTO> ReseñaItemDTOs { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReseñaCreateDTO dTO &&
                   Descripcion == dTO.Descripcion &&
                   NombreUsuario == dTO.NombreUsuario &&
                   Titulo == dTO.Titulo &&
                   Valoracion == dTO.Valoracion;
        }
    }
}
