using System;

public class BonosComprados
{
	public BonosComprados()
	{
	}
	public BonosComprados(int bonoCompradosId, int cantidad, int idCompra, double precioBono, BonoBocadillo bonoBocadillo, CompraBono compraBono)
    {
        Id = bonoCompradosId;
        IdCompra = idCompra;
        Cantidad = cantidad;
        PrecioBono = precioBono;
        BonoBocadillo = bonoBocadillo;
        CompraBono = compraBono;
    }

    //BONO COMPRADOS ID
    [Key] //Clave Primaria
    public int Id { get; set; }

    //CANTIDAD COMPRA
    [Required]
    [Display(Name = "Cantidad de Compra")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima de compra es 1")]
    public int Cantidad { get; set; }

    //ID COMPRA
    public int IdCompra { get; set; }

    //PRECIO BONO
    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
    [Display(Name = "Precio")]
    public double PrecioBono { get; set; }

    //BONO BOCADILLO 
    [Required]  //Requerido porque es 1 a N
    public BonoBocadillo BonoBocadillo { get; set; }

    //COMPRA BONO
    [Required] //Requerido porque es 1 a N
    public CompraBono CompraBono { get; set; }

}
