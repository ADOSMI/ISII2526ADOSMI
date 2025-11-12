

namespace AppForSEII2526.API.Models
{

    public class CompraBono
    {
	    public CompraBono()
	    {
	    }

        public CompraBono(ApplicationUser aplicationUser ,string apellidoBono1, string apellidoBono2, int compraBonoId, DateTime fechaCompraBono, EnumMetodosPago metodoPago, int nBonos, string nombreCliente, double precioTotalBono, IList<BonosComprados> bonosComprados)
        {
            Id = compraBonoId;
            FechaCompra = fechaCompraBono;
            MetodoPago = metodoPago;
            NBonos = nBonos;
            ApplicationUser = aplicationUser;
            PrecioTotalBono = precioTotalBono;
            BonosComprados = bonosComprados;
            
        }

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