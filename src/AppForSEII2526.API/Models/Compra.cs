using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        // Constructor vacío
        public Compra() { 
        }

        public Compra(int id, string apellido_1Cliente, string? apellido_2Cliente, string nombreCliente, DateTime fechaCompra, int nBocadillos, IList<CompraBocadillo> compraBocadillo, EnumMetodosPago metodoPago)
        {
            PrecioTotal = compraBocadillo.Sum(cb => cb.Cantidad * cb.Precio);
            Id = id;
            Apellido_1Cliente = apellido_1Cliente;
            Apellido_2Cliente = apellido_2Cliente;
            NombreCliente = nombreCliente;
            FechaCompra = fechaCompra;
            NumBocadillos = nBocadillos;
            CompraBocadillo = compraBocadillo;
            MetodoPago = metodoPago;
        }


        //ID COMPRA
        [Key] //Clave Primaria
        public int Id { get; set; }

        //PRIMER APELLIDO
        [Display(Name = "Primer apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su primer apellido")]
        [StringLength(40, ErrorMessage = "El apellido no puede ser mayor de 40 caracteres")]
        public string Apellido_1Cliente { get; set; }

        //SEGUNDO APELLIDO
        [Display(Name = "Segundo apellido")]
        [StringLength(40, ErrorMessage = "El segundo apellido no puede ser mayor de 40 caracteres")]
        public string? Apellido_2Cliente { get; set; }

        //NOMBRE
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre")]
        [StringLength(40, ErrorMessage = "El nombre no puede ser mayor de 40 caracteres")]
        public string NombreCliente { get; set; }

        //FECHA DE LA COMPRA
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        //PRECIO TOTAL
        [Precision(10, 2)]
        public double PrecioTotal { get; set; }

        //NÚMERO DE BOCADILLOS
        [Range(1, int.MaxValue, ErrorMessage = "Tiene que haber al menos 1 bocadillo")]
        public int NumBocadillos { get; set; }

        //MÉTODO DE PAGO
        [Required]
        [Display(Name = "Metodo de pago")]
        public EnumMetodosPago MetodoPago { get; set; }

        public IList<CompraBocadillo> CompraBocadillo { get; set; }

    }

    public enum EnumMetodosPago
    {
        Tarjeta,
        PayPal,
        GPay
    }



}

