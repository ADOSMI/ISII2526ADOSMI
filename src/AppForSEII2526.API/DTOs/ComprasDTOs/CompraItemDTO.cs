namespace AppForSEII2526.API.DTOs.CompraBocadilloDTOs
{
    public class CompraItemDTO
    {
        public CompraItemDTO(int bocadilloID, string nombre, double precioUnitario, string tipoPan, int cantidad)
        {
            BocadilloID = bocadilloID;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            PrecioUnitario = precioUnitario;
            TipoPan = tipoPan;
            Cantidad = cantidad;
        }

        public int BocadilloID { get; set; }


        [StringLength(50, ErrorMessage = "El nombre no puede ser superior a 50 caracteres")]
        public string Nombre { get; set; }


        [Display(Name = "Price de venta al público")]
        [Precision(10, 2)]
        public double PrecioUnitario { get; set; }

        public string TipoPan { get; set; }

        [Required]
        //this is defined to check that at least one movie is bought
        [Range(1, Double.MaxValue, ErrorMessage = "Debes de insertar una cantidad")]
        public int Cantidad { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CompraItemDTO model &&
                BocadilloID == model.BocadilloID &&
                PrecioUnitario == model.PrecioUnitario &&
                Cantidad == model.Cantidad &&
                TipoPan == model.TipoPan &&
                Nombre == model.Nombre;
        }
    }

}
