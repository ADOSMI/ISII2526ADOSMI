namespace AppForSEII2526.API.DTOs.Merch

{

    /// <summary> 

    /// DTO que representa un producto dentro de un detalle o compra (Paso 5 y 7 del CU). 

    /// </summary> 

    public class MerchItemDTO

    {

        public MerchItemDTO(int id, string nombre, string tipo, double pvp, int cantidad)

        {

            Id = id;

            Nombre = nombre;

            Tipo = tipo;

            PVP = pvp;

            Cantidad = cantidad;

        }



        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public double PVP { get; set; }

        public int Cantidad { get; set; }



        public override bool Equals(object? obj)

        {

            return obj is MerchItemDTO dto &&

                   Id == dto.Id &&

                   Nombre == dto.Nombre &&

                   Tipo == dto.Tipo &&

                   PVP.Equals(dto.PVP) &&

                   Cantidad == dto.Cantidad;

        }



        public override int GetHashCode()

        {

            return HashCode.Combine(Id, Nombre, Tipo, PVP, Cantidad);

        }

    }

}

