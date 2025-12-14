using AppForSEII2526.Web.API; 

namespace AppForSEII2526.Web
{
    public class CrearReseñaStateContainer
    {
        // Instancia principal
        public ReseñaCreateDTO Reseña { get; private set; } = new ReseñaCreateDTO()
        {
            ReseñaItemDTOs = new List<ReseñaItemDTO>()
        };

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddBocadilloAReseña(BocadilloForReseñaDTO bocadillo)
        {
            if (!Reseña.ReseñaItemDTOs.Any(item => item.Id == bocadillo.Id))
            {
                Reseña.ReseñaItemDTOs.Add(new ReseñaItemDTO()
                {
                    Id = bocadillo.Id,
                    Nombre = bocadillo.Nombre,
                    Pvp = bocadillo.Pvp,     
                    Tamano = bocadillo.Tamano,
                    Puntuacion = 0 
                });

                NotifyStateChanged(); 
            }
        }

        .
        public void RemoveBocadilloDeReseña(ReseñaItemDTO item)
        {
            Reseña.ReseñaItemDTOs.Remove(item);
            NotifyStateChanged();
        }

        public void ClearReseñaCart()
        {
            Reseña.ReseñaItemDTOs.Clear();
            NotifyStateChanged();
        }

        public void ReseñaProcessed()
        {
            Reseña = new ReseñaCreateDTO()
            {
                ReseñaItemDTOs = new List<ReseñaItemDTO>()
            };
            NotifyStateChanged();
        }

        public bool IsEmpty() => Reseña.ReseñaItemDTOs.Count == 0;

        public bool Includes(int id) => Reseña.ReseñaItemDTOs.Any(i => i.Id == id);
    }
}