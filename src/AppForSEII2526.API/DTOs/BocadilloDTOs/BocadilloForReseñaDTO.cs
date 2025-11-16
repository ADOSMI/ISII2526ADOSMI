
namespace AppForSEII2526.API.DTOs.BocadilloDTOs
{
    public class BocadilloForReseñaDTO
    {
        public BocadilloForReseñaDTO()
        {
        }

        public BocadilloForReseñaDTO(int id, string nombre, double pvp, Tamano tamano, string tipoPan)
        {
            Id = id;
            Nombre = nombre;
            PVP = pvp;
            Tamano = tamano;
            TipoPan = tipoPan;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del bocadillo no puede contener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio")]
        public double PVP { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        public Tamano Tamano { get; set; }

        public string TipoPan { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BocadilloForReseñaDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   PVP == dTO.PVP &&
                   Stock == dTO.Stock &&
                   Tamano == dTO.Tamano &&
                   TipoPan == dTO.TipoPan;
        }
    }

}
