

namespace AppForSEII2526.API.Models
{

    public class CompraBono
    {
	    public CompraBono()
	    {
	    }

        public CompraBono(ApplicationUser aplicationUser , string nombre, string apellido1, string apellido2, int compraBonoId, DateTime fechaCompraBono, EnumMetodosPago metodoPago, int nBonos, double precioTotalBono, IList<BonosComprados> bonosComprados)
        {
            Id = compraBonoId;
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            FechaCompra = fechaCompraBono;
            MetodoPago = metodoPago;
            NBonos = nBonos;
            ApplicationUser = aplicationUser;
            PrecioTotalBono = precioTotalBono;
            BonosComprados = bonosComprados;
            
        }

        public CompraBono(string nombre, string apellido1, string apellido2, EnumMetodosPago metodoPago, IList<BonosComprados> bonosComprados)
        {
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            MetodoPago = metodoPago;
            BonosComprados = bonosComprados;

        }

        public CompraBono(string nombre, string apellido1, string apellido2, DateTime fechaCompraBono, EnumMetodosPago metodoPago, IList<BonosComprados> bonosComprados)
        {
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            FechaCompra = fechaCompraBono;
            MetodoPago = metodoPago;
            BonosComprados = bonosComprados;

        }

        //COMPRA BONO ID
        [Key] //Clave Primaria
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

        //FECHA DE COMPRA
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de compra")]
        public DateTime FechaCompra { get; set; }

        //METODO DE PAGO
        [Display(Name = "Método de pago")]
        [Required(ErrorMessage = "Elija un método de pago")]
        public EnumMetodosPago MetodoPago { get; set; }

        //NÚMERO DE BONOS
        [Display(Name = "Numero de bonos")]
        [Range(1, int.MaxValue, ErrorMessage = "Numero de bonos mínimo es 1")]
        public int NBonos { get; set; }

        //PRECIO TOTAL BONO
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio Total Bono")]
        public double PrecioTotalBono { get; set; }

        public IList<BonosComprados> BonosComprados { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        
    }

    
}