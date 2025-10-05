using System;

public class Bocadillo
{
	public Bocadillo()
	{
	}

	public Bocadillo( int id, string nombre, double pvp, int stock, TipoPan tipoPan)
	{
		
		Id = id;
		Nombre = nombre;
		PVP = pvp;
        Stock = stock;
        TipoPan = tipoPan;
        
	}

    public Bocadillo(int id, string nombre, double pvp, int stock, string tamanyo, TipoPan tipoPan)
    {
        Id = id;
        Nombre = nombre;
        PVP = pvp;
        Stock = stock;
        Tamanyo = tamanyo;
        TipoPan = tipoPan;
    }

    [Key] //Clave Primaria
    public int Id { get; set; }

    
    [Required]
    [StringLength(50, ErrorMessage = "El nombre del bocadillo no puede contener más de 50 caracteres.")]
    public string Nombre { get; set; }

    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
    [Display(Name = "Precio")]
    public double PVP { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    public Tamano Tamano { get; set; }

    //TAMAÑO
    [Display(Name = "tamaño bocadillo")]
    [StringLength(40, ErrorMessage = "El nombre del bocadillo no puede ser mayor de 40 caracteres")]
    public string Tamanyo { get; set; }

   
    public TipoPan TipoPan { get; set; }

    public IList<ResenyaBocadillo> ResenyasBocadillo { get; set; }

    public IList<CompraBocadillo> CompraBocadillo { get; set; }

}

public enum Tamano
{
    Pequeño,
    Normal
}
