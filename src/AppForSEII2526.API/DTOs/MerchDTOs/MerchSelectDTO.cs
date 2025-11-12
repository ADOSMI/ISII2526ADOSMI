namespace AppForSEII2526.API.DTOs.Merch
{
    /// <summary>
    /// DTO que representa un producto disponible para comprar (Paso 2 del CU).
    /// </summary>
    public class MerchSelectDTO
    {
        public MerchSelectDTO(int id, string nombre, string tipo, double pvp, int stock)
        {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
            PVP = pvp;
            Stock = stock;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public double PVP { get; set; }
        public int Stock { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MerchSelectDTO dto &&
                   Id == dto.Id &&
                   Nombre == dto.Nombre &&
                   Tipo == dto.Tipo &&
                   PVP.Equals(dto.PVP) &&
                   Stock == dto.Stock;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Tipo, PVP, Stock);
        }
    }
}


