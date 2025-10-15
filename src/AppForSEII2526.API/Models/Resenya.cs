using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Resenya
{
    public Resenya()
    {
        ResenyaBocadillos = new List<ResenyaBocadillo>();
    }

    public Resenya(string descripcion, DateTime fechaPublicacion, int id, string nombreUsuario, string titulo)
    {
        Descripcion = descripcion;
        FechaPublicacion = fechaPublicacion;
        Id = id;
        NombreUsuario = nombreUsuario;
        Titulo = titulo;
        ResenyaBocadillos = new List<ResenyaBocadillo>();
    }

    [Key] // Clave primaria
    public int Id { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "La descripción no puede ocupar más de 250 caracteres")]
    public string Descripcion { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Fecha de publicación")]
    public DateTime FechaPublicacion { get; set; }

    [StringLength(50, ErrorMessage = "El nombre de usuario no puede ocupar más de 50 caracteres")]
    public string? NombreUsuario { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "El título no puede ocupar más de 50 caracteres")]
    public string Titulo { get; set; }

    [Required]
    public Valoracion_General Valoracion { get; set; }

    // Relación 1:N con ResenyaBocadillo
    public IList<ResenyaBocadillo> ResenyaBocadillos { get; set; }

    public enum Valoracion_General
    {
        Una,
        Dos,
        Tres,
        Cuatro,
        Cinco
    }
}
