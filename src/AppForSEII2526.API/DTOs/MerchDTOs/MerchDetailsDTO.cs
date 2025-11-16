namespace AppForSEII2526.API.DTOs.Merch

{

    /// <summary> 

    /// DTO que representa el detalle de un producto de merchandising (Paso 7 del CU). 

    /// </summary> 

    public class MerchDetailsDTO

    {

        public MerchDetailsDTO(int id, string nombre, string tipo, double pvp, int stock, IList<MerchItemDTO>? items = null)

        {

            Id = id;

            Nombre = nombre;

            Tipo = tipo;

            PVP = pvp;

            Stock = stock;

            Items = items ?? new List<MerchItemDTO>();

        }



        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public double PVP { get; set; }

        public int Stock { get; set; }



        public IList<MerchItemDTO> Items { get; set; }



        public override bool Equals(object? obj)

        {

            return obj is MerchDetailsDTO dto &&

                   Id == dto.Id &&

                   Nombre == dto.Nombre &&

                   Tipo == dto.Tipo &&

                   PVP.Equals(dto.PVP) &&

                   Stock == dto.Stock &&

                   Items.SequenceEqual(dto.Items);

        }



        public override int GetHashCode()

        {

            return HashCode.Combine(Id, Nombre, Tipo, PVP, Stock);

        }

    }

}
