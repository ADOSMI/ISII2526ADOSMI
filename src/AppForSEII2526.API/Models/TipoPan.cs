namespace AppForSEII2526.API.Models
{
    public class TipoPan
    {
        public TipoPan()
        {
        }

        public TipoPan(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int Id { get; set; }

        //NOMBRE DEL PAN
        [Display(Name = "Nombre del pan")]
        [StringLength(40, ErrorMessage = "El nombre del pan no puede ser mayor de 40 caracteres")]
        public string Nombre { get; set; }

        public IList<Bocadillo> Bocadillos { get; set; } = new List<Bocadillo>();
    }
}
