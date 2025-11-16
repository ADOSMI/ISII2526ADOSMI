using System;

public class TipoBocadillo
{
	public TipoBocadillo()
	{
	}
    
	public TipoBocadillo(int id, string nombreTipo)
    {
        Id = id;
        Nombre = nombreTipo;
        
    }

    //ID TIPO BOCADILLO
    [Key] //Clave Primaria
    public int Id { get; set; }
        
    //NOMBRE TIPO BOCADILLO
    [Display(Name = "Nombre tipo bocadillo")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca el nombre de tipo de bocadillo")]
    [StringLength(40, ErrorMessage = "Nombre de tipo de bocadiilo no puede ser mayor de 40 caracteres")]
    public string Nombre { get; set; }

    public IList<BonoBocadillo> BonoBocadillo { get; set; } = new List<BonoBocadillo>();
}
