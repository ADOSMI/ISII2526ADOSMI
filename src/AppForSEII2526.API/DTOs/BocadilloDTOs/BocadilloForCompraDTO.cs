using Humanizer.Localisation;

namespace AppForSEII2526.API.DTOs.BocadilloDTOs
{
    public class BocadilloForCompraDTO
    {

        public BocadilloForCompraDTO()
        { 
        }

        public BocadilloForCompraDTO(int id, string nombre, Tamano tamano, string tipoPan, double precio)
        {
            Id = id;
            Nombre = nombre;
            TipoPan = tipoPan;
            Precio = precio;
            Tamano = tamano;    
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del bocadillo no puede contener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio")]
        public double Precio { get; set; }

        public Tamano Tamano { get; set; }

        public string TipoPan { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is BocadilloForCompraDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   TipoPan == dTO.TipoPan &&
                   Precio == dTO.Precio &&
                   Tamano == dTO.Tamano;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TipoPan, Nombre, Precio, Tamano);
        }


    }
}
