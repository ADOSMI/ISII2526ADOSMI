using System.Drawing;

namespace AppForSEII2526.API.DTOs.CompraBocadilloDTOs
{
    public class CompraDetailDTO : CompraForCreateDTO
    {
        public CompraDetailDTO(int id, string nombreCliente,
            string apellido1Cliente, string apellido2Cliente, EnumMetodosPago enumMetodosPago,
            DateTime fechaCompra, IList<CompraItemDTO> compraItems) :
            base(nombreCliente, apellido1Cliente, apellido2Cliente, enumMetodosPago, compraItems)
        {
            Id = id;
            FechaCompra = fechaCompra;
        }
        public int Id { get; set; }

        public DateTime FechaCompra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraDetailDTO dTO &&
                    base.Equals(obj) &&
                    PrecioTotal == dTO.PrecioTotal &&
                    Id == dTO.Id &&
                    (FechaCompra.Subtract(dTO.FechaCompra) < new TimeSpan(0, 1, 0));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id, FechaCompra);
        }
    }
}
