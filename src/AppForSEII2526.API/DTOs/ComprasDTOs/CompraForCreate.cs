namespace AppForSEII2526.API.DTOs.CompraBocadilloDTOs
{
    public class CompraForCreateDTO
    {
        public CompraForCreateDTO(string nombreCliente, string apellido1Cliente, string apellido2Cliente, EnumMetodosPago enumMetodosPago, IList<CompraItemDTO> compraItems)
        {
            NombreCliente = nombreCliente;
            Apellido1Cliente = apellido1Cliente;
            Apellido2Cliente = apellido2Cliente;
            EnumeracionMetodosPago = enumMetodosPago;
            CompraItems = compraItems ?? throw new ArgumentNullException(nameof(compraItems));
        }

        public CompraForCreateDTO()
        {
            CompraItems = new List<CompraItemDTO>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserte su nombre")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe contener al menos 2 caracteres")]
        public string NombreCliente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserte su primer apellido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe contener al menos 2 caracteres")]
        public string Apellido1Cliente { get; set; }


        [StringLength(50, ErrorMessage = "El segundo apellido no puede ser mayor de 50 caracteres")]
        public string? Apellido2Cliente { get; set; }

        public IList<CompraItemDTO> CompraItems { get; set; }
        [Required]
        public EnumMetodosPago EnumeracionMetodosPago { get; set; }

        [Display(Name = "Precio Total")]
        [JsonPropertyName("PrecioTotal")]
        public double PrecioTotal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraForCreateDTO dTO &&
                   Apellido1Cliente == dTO.Apellido1Cliente &&
                   Apellido2Cliente == dTO.Apellido2Cliente &&
                   NombreCliente == dTO.NombreCliente &&
                   CompraItems.SequenceEqual(dTO.CompraItems) &&
                   EnumeracionMetodosPago == dTO.EnumeracionMetodosPago &&
                   PrecioTotal == dTO.PrecioTotal;
        }
    }
}

