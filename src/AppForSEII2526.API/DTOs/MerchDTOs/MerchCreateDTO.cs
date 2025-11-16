using AppForSEII2526.API.DTOs.Merch;
using System.ComponentModel.DataAnnotations;



namespace AppForSEII2526.API.DTOs.Merch

{

    /// <summary> 

    /// DTO que contiene los datos del formulario de compra (Paso 5 del CU). 

    /// </summary> 

    public class MerchCreateDTO

    {

        public MerchCreateDTO(string nombre, string apellido1, string? apellido2, string direccionEnvio, string metodoPago, IList<MerchItemDTO> items)

        {

            Nombre = nombre;

            Apellido1 = apellido1;

            Apellido2 = apellido2;

            DireccionEnvio = direccionEnvio;

            MetodoPago = metodoPago;

            Items = items ?? new List<MerchItemDTO>();

        }



        [Required]

        [StringLength(50)]

        public string Nombre { get; set; }



        [Required]

        [StringLength(50)]

        public string Apellido1 { get; set; }



        [StringLength(50)]

        public string? Apellido2 { get; set; }



        [Required]

        [StringLength(100)]

        public string DireccionEnvio { get; set; }



        [Required]

        public string MetodoPago { get; set; }



        [Required]

        public IList<MerchItemDTO> Items { get; set; }

    }

}




