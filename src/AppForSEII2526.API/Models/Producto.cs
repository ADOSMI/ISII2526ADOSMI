
using System.ComponentModel.DataAnnotations;
namespace AppForSEII2526.API.Models
{
    public class Producto
    {
        public Producto() { }

        public Producto(int productoId, string nombre, double pvp, int stock, int tipoProductoId, TipoProducto tipoProducto)
        {
            ProductoId = productoId;
            Nombre = nombre;
            PVP = pvp;
            Stock = stock;
            TipoProducto = tipoProducto;


        }

        [Key]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre del producto no puede contener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio")]
        public double PVP { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        public TipoProducto TipoProducto { get; set; }


        // Relación con Producto_Compra
        public IList<Producto_Compra> producto_Compras { get; set; } = new List<Producto_Compra>();
    }
}