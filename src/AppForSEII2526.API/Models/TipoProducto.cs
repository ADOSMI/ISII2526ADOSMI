namespace AppForSEII2526.API.Models
{
    public class TipoProducto
    {
        public TipoProducto() { }

        public TipoProducto(int id, string nombre)
        {
            ProductoId = id;
            Nombre = nombre;
           
        }

        [Key]
        public int ProductoId { get; set; }

        // NOMBRE DEL PRODUCTO
        [Display(Name = "Nombre del producto")]
        [StringLength(40, ErrorMessage = "El nombre del producto no puede ser mayor de 40 caracteres")]
        public string Nombre { get; set; }

       public IList<Producto> Productos { get; set; } = new List<Producto>();
    }
}
