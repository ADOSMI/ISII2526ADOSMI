
using AppForSEII2526.API.DTOs.ReseñasDTOs;
using static Resenya;

namespace AppForSEII2526.API.DTOs.ReseñaDTOs
{
    public class ReseñaDetailDTO:ReseñaCreateDTO
    {

        public ReseñaDetailDTO() { 
        
        }

        public ReseñaDetailDTO(int id, string nombreUsuario, string titulo, string descripcion, DateTime fechaPublicacion, Valoracion_General valoracion, IList<ReseñaItemDTO> reseñaItemDTOs): base(nombreUsuario, titulo, descripcion, valoracion, reseñaItemDTOs) {

            Id = id;
            FechaPublicacion = fechaPublicacion;
        
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de publicación")]
        public DateTime FechaPublicacion { get; set; }
        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReseñaDetailDTO dTO &&
                   base.Equals(obj) &&
                   Descripcion == dTO.Descripcion &&
                   NombreUsuario == dTO.NombreUsuario &&
                   Titulo == dTO.Titulo &&
                   Valoracion == dTO.Valoracion &&
                   EqualityComparer<IList<ReseñaItemDTO>>.Default.Equals(ReseñaItemDTOs, dTO.ReseñaItemDTOs) &&
                   (FechaPublicacion.Subtract(dTO.FechaPublicacion) < new TimeSpan(0, 1, 0)) &&
                   Id == dTO.Id;
        }
    }
}