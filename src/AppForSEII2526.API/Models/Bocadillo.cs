using System;

public class Bocadillo
{
	public Bocadillo()
	{
	}

	public Bocadillo(int comprasDelBocadillo, int id, string nombre, double pvp, int stock, int panId)
	{
		ComprasDelBocadillo = comprasDelBocadillo;
		Id = id;
		Nombre = nombre;
		PVP = pvp;
		Stock = stock;
        PanId = panId;
        ResenyasBocadillo = new List<ResenyaBocadillo>();
	}

	[Key] //Clave Primaria
    public int Id { get; set; }

    [Display(Name = "Compras del bocadillo")]
    [Range(1, int.MaxValue, ErrorMessage = "El mínimo de compras de bocadillo es 1")]
    public int ComprasDelBocadillo {  get; set; }

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

    // Clave foránea a TipoPan
    [ForeignKey("TipoPan")]
    public int PanId { get; set; }
    public TipoPan TipoPan { get; set; }

    public IList<ResenyaBocadillo> ResenyasBocadillo { get; set; }

    

}

public enum Tamano
{
    Pequeño,
    Normal
}
