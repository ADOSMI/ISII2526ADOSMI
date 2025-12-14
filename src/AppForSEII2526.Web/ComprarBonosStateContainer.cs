using AppForSEII2526.Web.API;


namespace AppForSEII2526.Web
{
    public class ComprarBonosStateContainer
    {

        public BonosForCreateDTO CompraBono { get; private set; } = new BonosForCreateDTO()
        {
            ItemsCompraBono = new List<BonosItemDTO>()
        };

        public double PrecioTotal
        {
            get
            {
                return CompraBono.ItemsCompraBono.Sum(cb => cb.Cantidad * cb.PrecioPorBono);
            }
        }


        public void AddBonoCompra(BonosForCompraDTO bonos)
        {
            // Si pides directamente en la interfaz de usuario la cantidad, el último párametro en vez de ser 1 sería la propia cantidad.
            CompraBono.ItemsCompraBono.Add(new BonosItemDTO()
                {
                Id = bonos.Id,
                PrecioPorBono = bonos.Precio,
                NumeroBocadillos = bonos.NumeroBocadillos,
                Nombre = bonos.Nombre,
                TipoBocadillo = bonos.TipoBocadillo,
                Cantidad = 1,
            }
            );

        }

        // Se elimina un elemento del carrito de la compra.

        public void RemoveBonoCompra(int bonoId)
        {
            BonosItemDTO itemCompraBono = CompraBono.ItemsCompraBono.FirstOrDefault(cb => cb.Id.Equals(bonoId));
            CompraBono.ItemsCompraBono.Remove(itemCompraBono);
        }

        // Devuelve si el carrito está vacío o no.
        public bool isEmpty()
        {
            if (CompraBono.ItemsCompraBono.Count == 0) return true;
            else return false;
        }

        // Devuelve si el carrito incluye o no un artículo.
        public bool includes(int id)
        {
            if (CompraBono.ItemsCompraBono.Any(cb => cb.Id.Equals(id))) { return true; }

            return false;
        }

        // Actualiza la cantidad que se quiere comprar del artículo cuyo id es 'id'.
        public void UpdateCarrito(int id, int cantidad)
        {
            BonosItemDTO? bono = CompraBono.ItemsCompraBono.FirstOrDefault(cb => cb.Id == id);

            if (bono != null) bono.Cantidad = cantidad;
        }

        // Se ha terminado la compra, así que hay que vaciar el carrito..
        public void FinalizarCompraBono()
        {
            CompraBono = new BonosForCreateDTO()
            {
                ItemsCompraBono = new List<BonosItemDTO>()
            };
        }
    }
}
