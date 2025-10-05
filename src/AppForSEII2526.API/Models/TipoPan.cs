using System;

public class TipoPan
{
	public TipoPan()
	{
	}

	public TipoPan(string nombre, int panId){
		 Nombre = nombre;
		 PanId = panId;
		 Bocadillos = new List<Bocadillo>();
	}

    
    [Key] //Clave Primaria
    public int PanId { get; set; }

    [Required(ErrorMessage = "El nombre del tipo de pan es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre del tipo de pan no puede contener más de 50 caracteres.")]
    public string Nombre { get; set; }

    public IList<Bocadillo> Bocadillos { get; set; }

}
