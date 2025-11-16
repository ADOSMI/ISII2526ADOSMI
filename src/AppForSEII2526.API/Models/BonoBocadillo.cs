using System;

public class BonoBocadillo
{
	public BonoBocadillo()
	{
	}

	public BonoBocadillo(TipoBocadillo tipobocadillo, int bonoBocadilloId, int cantidadDisponible, int nBocadillos, string nombre, double PVP, IList<BonosComprados> bonosComprados)
	{
        TipoBocadillo = tipobocadillo;
		Id = bonoBocadilloId;
        CantidadDisponible = cantidadDisponible;
        NumeroBocadillos = nBocadillos;
        Nombre = nombre;
        PrecioPorBono = PVP;
        BonosComprados = bonosComprados;

    }

    //TIPO BOCADILLO 
     
    public TipoBocadillo TipoBocadillo  { get; set; }

    //BONO BOCADILLO ID
    [Key] //Clave Primaria
    public int Id { get; set; }

    //CANTIDAD DISPONIBLE
    [Required]
    [Display(Name = "Cantidad disponible")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad disponible mínima es 1")]
    public int CantidadDisponible { get; set; }

    //NUMERO DE BOCADILLOS POR BONO
    [Required]
    [Display(Name = "Numero de bocadillos por bono")]
    [Range(1, int.MaxValue, ErrorMessage = "El número mínimo de bocadillos por bono es 1")]
    public int NumeroBocadillos { get; set; }

    public string Nombre { get; set; }

    //PRECIO POR BONO
    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, float.MaxValue, ErrorMessage = "Precio mínimo es 1")]
    [Display(Name = "Precio Por Bono")]
    public double PrecioPorBono { get; set; }

    public IList<BonosComprados> BonosComprados { get; set; }


}
