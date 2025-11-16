using AppForSEII2526.API.DTOs.BonosDTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.DTOs.CompraBonosDTOs
{
    public class BonosDetailDTO 
    {
        public BonosDetailDTO(int id, string nombre, string apellido1, string apellido2, EnumMetodosPago metodoPago, DateTime fechaCompraBono, double precioTotal, IList<BonosItemDTO> itemsBono) 
        { 
            Id = id;
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            MetodoPago = metodoPago;
            FechaCompra = fechaCompraBono;
            PrecioTotal = precioTotal;
            ItemsBono = itemsBono ?? new List<BonosItemDTO>();

        }
        
   
        public BonosDetailDTO()
        {

        }

        //ID BONO
        public int Id { get; set; }

        
        //NOMBRE
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre")]
        [StringLength(40, ErrorMessage = "Nombre no puede ser mayor de 40 caracteres")]
        public string Nombre { get; set; }

        //PRIMER APELLIDO    
        [Display(Name = "Primer apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su primer apellido")]
        [StringLength(40, ErrorMessage = "Primer apellido no puede ser mayor de 40 caracteres")]
        public string Apellido1 { get; set; }

        //EGUNDO APELLIDO    
        [Display(Name = "Segundo apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su segundo apellido")]
        [StringLength(40, ErrorMessage = "Segundo apellido no puede ser mayor de 40 caracteres")]
        public string Apellido2 { get; set; }
        
        
        //METODO DE PAGO
        [Display(Name = "Método de pago")]
        [Required(ErrorMessage = "Elija un método de pago")]
        public EnumMetodosPago MetodoPago { get; set; }

        //FECHA DE COMPRA
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de compra")]
        public DateTime FechaCompra { get; set; }

        //PRECIO TOTAL BONO
        public double PrecioTotal { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        //LISTA VIDEOJUEGOS
        public IList<BonosItemDTO> ItemsBono { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BonosDetailDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   Apellido1 == dTO.Apellido1 &&
                   Apellido2 == dTO.Apellido2 &&
                   MetodoPago == dTO.MetodoPago &&
                   FechaCompra.Date == dTO.FechaCompra.Date &&
                   PrecioTotal == dTO.PrecioTotal &&
                   ItemsBono.SequenceEqual(dTO.ItemsBono); //Compara listas de items
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Nombre);
            hash.Add(Apellido1);
            hash.Add(Apellido2);
            hash.Add(MetodoPago);
            hash.Add(FechaCompra);
            hash.Add(PrecioTotal);
            hash.Add(ItemsBono);
            return hash.ToHashCode();
        }
    }
}
