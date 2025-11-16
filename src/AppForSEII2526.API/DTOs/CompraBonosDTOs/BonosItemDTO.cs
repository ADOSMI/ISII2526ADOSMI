using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.DTOs.CompraBonosDTOs
{
    public class BonosItemDTO
    {   
        public BonosItemDTO(int idBono, double PVP, int nBocadillos, string nombre, string tipoBocadillo, int cantidad )
        {
            Id = idBono;
            PrecioPorBono = PVP;
            NumeroBocadillos = nBocadillos;
            Nombre = nombre;
            TipoBocadillo = tipoBocadillo;
            Cantidad = cantidad;
        }

        public BonosItemDTO()
        {
           
        }

        //BONO BOCADILLO ID
        [Key] //Clave Primaria
        public int Id { get; set; }

        //PRECIO POR BONO
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio Por Bono")]
        public double PrecioPorBono { get; set; }

        //NUMERO DE BOCADILLOS POR BONO
        [Display(Name = "Numero de bocadillos por bono")]
        [Range(1, int.MaxValue, ErrorMessage = "El número mínimo de bocadillos por bono es 1")]
        public int NumeroBocadillos { get; set; }

        
        public string Nombre { get; set; }

        //TIPO BOCADILLO 
        
        public string TipoBocadillo { get; set; }

        //CANTIDAD
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Cantidad mínima es 1")]
        [Display(Name = "Cantidad Por Bono")]
        public int Cantidad { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is BonosItemDTO dTO &&
                   Id == dTO.Id &&
                   PrecioPorBono == dTO.PrecioPorBono &&
                   NumeroBocadillos == dTO.NumeroBocadillos &&
                   Nombre == dTO.Nombre &&
                   TipoBocadillo == dTO.TipoBocadillo &&
                   Cantidad == dTO.Cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrecioPorBono, NumeroBocadillos, Nombre, TipoBocadillo);
        }

        
    }
}
