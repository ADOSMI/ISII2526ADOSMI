namespace AppForSEII2526.API.Models
{
    public class Compra_Producto
    {
        public Compra_Producto() { }
        public Compra_Producto(string apellido_1, string apellido_2, int compraid, string direccionEnvio, DateTime fechaCompra, string metodo_Pago, string nombre, int precioFinal)
        {
            Apellido_1 = apellido_1;
            Apellido_2 = apellido_2;
            Compraid = compraid;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            Metodo_Pago = metodo_Pago;
            Nombre = nombre;
            PrecioFinal = precioFinal;
        }
        [Key] 
        public int Compraid { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección de envío es obligatoria.")]
        [StringLength(100, ErrorMessage = "La dirección no puede tener más de 100 caracteres.")]
        public string DireccionEnvio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Required, DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de publicación")]
        public DateTime FechaCompra { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [StringLength(30, ErrorMessage = "El método de pago no puede tener más de 30 caracteres.")]
        public string Metodo_Pago { get; set; }


        [Required(ErrorMessage = "El precio final es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio final debe ser mayor que 0.")]
        public int PrecioFinal { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El primer apellido no puede tener más de 50 caracteres.")]
        public string Apellido_1 { get; set; }

        [StringLength(50, ErrorMessage = "El segundo apellido no puede tener más de 50 caracteres.")]
        public string Apellido_2 { get; set; } 
        

        public IList<Producto_Compra> ListaCompra = new List<Producto_Compra>();
        
    }
}
