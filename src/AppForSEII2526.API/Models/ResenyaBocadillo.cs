namespace AppForSEII2526.API.Models
{
    
    public class ResenyaBocadillo
    {
        public ResenyaBocadillo()
        {
        }
        public ResenyaBocadillo(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }
        public int Id { get; set; }
        //DESCRIPCIÓN DE LA RESEÑA DEL BOCADILLO
        [Display(Name = "Descripción de la reseña del bocadillo")]
        [StringLength(200, ErrorMessage = "La descripción de la reseña del bocadillo no puede ser mayor de 200 caracteres")]
        public string Descripcion { get; set; }
        public IList<Bocadillo> Bocadillos { get; set; } = new List<Bocadillo>();
    }
}
