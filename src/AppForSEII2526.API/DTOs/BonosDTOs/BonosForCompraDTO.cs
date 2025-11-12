
namespace AppForSEII2526.API.DTOs.BonosDTOs
{
    public class BonosForCompraDTO
    {
        public BonosForCompraDTO(string nombre, double precio, int numeroBocadillos,TipoBocadillo tipoBocadillo)
        {
            Nombre = nombre;
            Precio = precio;
            NumeroBocadillos = numeroBocadillos;
            TipoBocadillo = tipoBocadillo;
        }

        public BonosForCompraDTO(int id,string nombre, double precio, int numeroBocadillos, TipoBocadillo tipoBocadillo)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            NumeroBocadillos = numeroBocadillos;
            TipoBocadillo = tipoBocadillo;
        }

        public BonosForCompraDTO()
        {

        }

        //ID BONO
        [Key]
        public int Id { get; set; }

        //NOMBRE BONO
        [StringLength(50, ErrorMessage = "El Titulo no puede ser mayor de 50 caracteres")]
        [Display(Name = "Nombre Bono")]
        public string Nombre { get; set; }

        //PRECIO TOTAL BONO
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio Total Bono")]
        public double Precio { get; set; }

        //NUMERO DE BOCADILLOS POR BONO
        [Display(Name = "Numero de bocadillos por bono")]
        [Range(1, int.MaxValue, ErrorMessage = "El número mínimo de bocadillos por bono es 1")]
        public int NumeroBocadillos { get; set; }

        //TIPO BOCADILLO 
        public TipoBocadillo TipoBocadillo { get; set; }



    }
}
