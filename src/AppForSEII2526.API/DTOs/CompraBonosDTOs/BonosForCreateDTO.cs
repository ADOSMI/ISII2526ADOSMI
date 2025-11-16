using AppForSEII2526.API;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.DTOs.CompraBonosDTOs
{
    public class BonosForCreateDTO //que introduzca de forma obligatoria su nombre completo al que estara el bono, apellidos y metodo de pago.
    {
        [JsonConstructor]
        public BonosForCreateDTO(ApplicationUser applicationUser,string nombre, string apellido1, string apellido2, EnumMetodosPago metodoPago, IList<BonosItemDTO> itemsCompraBono)
        {
            ApplicationUser = applicationUser;
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            MetodoPago = metodoPago;
            ItemsCompraBono = itemsCompraBono ?? new List<BonosItemDTO>(); ;
        }

        [Obsolete("Constructor requerido por System.Text.Json", true)]
        public BonosForCreateDTO() { }


        //NOMBRE BONO 
        [Display(Name = "Nombre del bono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca el nombre del bono")]
        [StringLength(40, ErrorMessage = "Nombre del bono no puede ser mayor de 40 caracteres")]
        public string Nombre { get; set; }

        //PRIMER APELLIDO    
        [Display(Name = "Primer apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su primer apellido")]
        [StringLength(40, ErrorMessage = "Primer apellido no puede ser mayor de 40 caracteres")]
        public string Apellido1 { get; set; }

        //EGUNDO APELLIDO    
        [Display(Name = "Segundo apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su segundo apellido")]
        [StringLength(40, ErrorMessage = "Segundo apellido no puede ser mayor de 40 caracteres")]
        public string Apellido2 { get; set; }


        //MÉTODO DE PAGO
        [JsonConverter(typeof(JsonStringEnumConverter))] //lo usan ellos 
        [Display(Name = "Método de pago")]
        [Required(ErrorMessage = "Elija un método de pago")]
        public EnumMetodosPago MetodoPago { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public IList<BonosItemDTO> ItemsCompraBono { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BonosForCreateDTO dTO &&
                   Nombre == dTO.Nombre &&
                   Apellido1 == dTO.Apellido1 &&
                   Apellido2 == dTO.Apellido2 &&
                   MetodoPago == dTO.MetodoPago &&
                   ItemsCompraBono.SequenceEqual(dTO.ItemsCompraBono);
        }
    }
}
