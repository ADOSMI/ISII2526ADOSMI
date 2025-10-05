namespace AppForSEII2526.API.Models
{
    public class Bocadillo
    {
        public Bocadillo()
        {
        }
        public Bocadillo(int id, string nombre, decimal pvp, ResenyaBocadillo resenyaBocadillo, int stock, string tamanyo, TipoPan tipoPan)
        {
            Id = id;
            Nombre = nombre;
            PVP = pvp;
            ResenyaBocadillo = resenyaBocadillo;
            Stock = stock;
            Tamanyo = tamanyo;
            TipoPan = tipoPan;
        }

        public int Id { get; set; }

        //NOMBRE DEL BOCADILLO
        [Display(Name = "Nombre del bocadillo")]
        [StringLength(50, ErrorMessage = "El nombre del bocadillo no puede ser mayor de 50 caracteres")]
        public string Nombre { get; set; }

        //PRECIO DE VENTA AL PÚBLICO
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, float.MaxValue, ErrorMessage = "Minimum price is 0.5 ")]
        [Display(Name = "Precio de venta")]
        [Precision(10, 2)]
        public decimal PVP { get; set; }

        //RESEÑA DEL BOCADILLO
        public ResenyaBocadillo ResenyaBocadillo { get; set; }

        //STOCK
        [Display(Name = "Cantidad de bocadillos")]
        public int Stock { get; set; }

        //TAMAÑO
        [Display(Name = "tamaño bocadillo")]
        [StringLength(40, ErrorMessage = "El nombre del bocadillo no puede ser mayor de 40 caracteres")]
        public string Tamanyo { get; set; }

        //TIPO DE BOCADILLO
        public TipoPan TipoPan { get; set; }

        public IList<CompraBocadillo> CompraBocadillo { get; set; }

    }
}