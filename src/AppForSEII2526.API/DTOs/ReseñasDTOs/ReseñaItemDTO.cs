namespace AppForSEII2526.API.DTOs.ReseñasDTOs
{
    public class ReseñaItemDTO
    {

        public ReseñaItemDTO() {

        }

        //POST
        public ReseñaItemDTO(int id, int puntuacion) { 
            Id= id;
            Puntuacion= puntuacion;
        
        }

        //DETAIL
        public ReseñaItemDTO(int id, string nombre, double pvp, Tamano tamano, int puntuacion) { 
            Id = id;
            Puntuacion= puntuacion;
            Nombre= nombre;
            Tamano= tamano;
            PVP= pvp;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
        [Display(Name = "Puntuación")]
        public int Puntuacion { get; set; }

        public double PVP { get; set; }
        public Tamano Tamano { get; set; }
        public string Nombre { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReseñaItemDTO dTO &&
                   Id == dTO.Id &&
                   Puntuacion == dTO.Puntuacion &&
                   PVP == dTO.PVP &&
                   Tamano == dTO.Tamano &&
                   Nombre == dTO.Nombre;
        }
    }
}