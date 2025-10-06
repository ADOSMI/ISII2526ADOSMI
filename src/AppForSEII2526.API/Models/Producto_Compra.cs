namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(ProductoId), nameof(CompraId))]
    public class Producto_Compra
    {
        public Producto_Compra() { }
        public Producto_Compra(int cantidad, int compraid, int productoid, int pvp, Producto producto)
        {
            Cantidad = cantidad;
            CompraId = compraid;
            ProductoId = productoid;
            PVP = pvp;
            Producto = producto;

        }
        [Key]
        public int CompraId { get; set; }
        //[ForeignKey("Producto")] ??
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima de compra es 1")]
        public int Cantidad { get; set; }

        // Precio unitario en el momento de la compra
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
        [Display(Name = "Precio unitario")]
        public double PVP { get; set; }

        [ForeignKey("CompraId")]
        public int CompraId1 { get; set; }

        [ForeignKey ("ResenyaId")]
        public Resenya Resenya { get; set; }

        public Compra_Producto compra { get; set; }
        public Producto Producto { get; set; }
    }
}
