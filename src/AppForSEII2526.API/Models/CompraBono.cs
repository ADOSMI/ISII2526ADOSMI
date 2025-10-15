

namespace AppForSEII2526.API.Models
{

    public class CompraBono
    {
	    public CompraBono()
	    {
	    }

        public CompraBono(ApplicationUser aplicationUser ,string apellidoBono1, string apellidoBono2, int compraBonoId, DateTime fechaCompraBono, EnumMetodosPago metodoPago, int nBonos, string nombreCliente, double precioTotalBono, IList<BonosComprados> bonosComprados)
        {
            ApellidoBono1 = apellidoBono1;
            ApellidoBono2 = apellidoBono2;
            Id = compraBonoId;
            FechaCompra = fechaCompraBono;
            MetodoPago = metodoPago;
            NBonos = nBonos;
            ApplicationUser = aplicationUser;
            PrecioTotalBono = precioTotalBono;
            BonosComprados = bonosComprados;
            
        }


        //APELLIDO BONO 1
        [Display(Name = "Primer Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su primer apellido")]
        [StringLength(40, ErrorMessage = "Primer Apellido no puede ser mayor de 40 caracteres")]
        public string ApellidoBono1 { get; set; }

        //APELLIDO BONO 2
        [Display(Name = "Segundo Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su segundo apellido")]
        [StringLength(40, ErrorMessage = "Segundo Apellido no puede ser mayor de 40 caracteres")]
        public string ApellidoBono2 { get; set; }
    
        //COMPRA BONO ID
        [Key] //Clave Primaria
        public int Id { get; set; }

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

        //NOMBRE CLIENTE
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre")]
        [StringLength(40, ErrorMessage = "Nombre no puede ser mayor de 40 caracteres")]
        public string NombreCliente { get; set; }

        //PRECIO TOTAL BONO
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio Total Bono")]
        public double PrecioTotalBono { get; set; }

        public IList<BonosComprados> BonosComprados { get; set; }

        public ApplicationUser ApplicationUser { get; set; }



        public enum EnumMetodosPago
        {
            TarjetaCredito,
            PayPal,
            GooglePay
        }
    }


    
}