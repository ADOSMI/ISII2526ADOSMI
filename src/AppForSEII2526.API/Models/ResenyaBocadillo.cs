using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; // <-- importante

[PrimaryKey(nameof(BocadilloId), nameof(ResenyaId))]
public class ResenyaBocadillo
{
    public ResenyaBocadillo() { }

    public ResenyaBocadillo(int bocadilloId, int puntuacion, int resenyaId)
    {
        BocadilloId = bocadilloId;
        Puntuacion = puntuacion;
        ResenyaId = resenyaId;
    }

    public int BocadilloId { get; set; }

    public int ResenyaId { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
    [Display(Name = "Puntuación")]
    public int Puntuacion { get; set; }

    [ForeignKey("BocadilloId")]
    public Bocadillo Bocadillo { get; set; }

    [ForeignKey("ResenyaId")]
    public Resenya Resenya { get; set; }
}

