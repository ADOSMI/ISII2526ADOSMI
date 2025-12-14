using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class CompraStateContainer
    {
        public CompraForCreateDTO Compra { get; private set; } = new CompraForCreateDTO()
        {
            CompraItems = new List<CompraItemDTO>()
        };

        /*we compute the TotalPrice of the movies we have selected for compra them
        public decimal TotalPrice
        {
            get
            {
                
                return Convert.ToDecimal(Compra.CompraItems.Sum(ci => ci.Cantidad));
            }
        }
        */
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddBocadilloToCompra(BocadilloForCompraDTO bocadillo)
        {
            //before adding a movie we checked whether it has been already added
            if (!Compra.CompraItems.Any(ri => ri.BocadilloID == bocadillo.Id))
                //we add it if it is not in the list
                Compra.CompraItems.Add(new CompraItemDTO()
                {
                    BocadilloID = bocadillo.Id,
                    Nombre = bocadillo.Nombre,
                    TipoPan = bocadillo.TipoPan,
                    PrecioUnitario = bocadillo.Precio,
                }
            );

        }

        //to delete movies from the list of selected movies
        public void RemoveCompraItemToCompra(CompraItemDTO item)
        {
            Compra.CompraItems.Remove(item);

        }

        //we eliminate all the movies from the list
        public void ClearCompraCart()
        {
            Compra.CompraItems.Clear();

        }

        //we have already finished the process of renting, thus, we create a new Rental 
        public void CompraProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Compra = new CompraForCreateDTO()
            {
                CompraItems = new List<CompraItemDTO>()
            };
        }
    }
}
