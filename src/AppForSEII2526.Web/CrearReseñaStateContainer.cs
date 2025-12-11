using AppForSEII2526.Web.API; // Ajusta según donde esté tu DTO

namespace AppForSEII2526.Web
{
    public class CrearReseñaStateContainer
    {
        // Instancia principal (equivalente a Rental)
        public ReseñaCreateDTO Reseña { get; private set; } = new ReseñaCreateDTO()
        {
            ReseñaItemDTOs = new List<ReseñaItemDTO>()
        };

        // Evento para notificar cambios a la UI (Igual que el modelo)
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        // Añadir bocadillo (Equivalente a AddMovieToRental)
        public void AddBocadilloAReseña(BocadilloForReseñaDTO bocadillo)
        {
            // Verificamos si ya existe antes de añadir
            if (!Reseña.ReseñaItemDTOs.Any(item => item.Id == bocadillo.Id))
            {
                // Mapeamos y añadimos
                Reseña.ReseñaItemDTOs.Add(new ReseñaItemDTO()
                {
                    Id = bocadillo.Id,
                    Nombre = bocadillo.Nombre,
                    Pvp = bocadillo.Pvp,     // Asegúrate que la propiedad en el DTO es Pvp o PVP
                    Tamano = bocadillo.Tamano,
                    Puntuacion = 0 // Inicializamos a 0
                });

                NotifyStateChanged(); // Notificamos a la UI
            }
        }

        // Eliminar bocadillo (Equivalente a RemoveRentalItemToRent)
        // CAMBIO IMPORTANTE: Ahora recibe el objeto DTO, no el int ID, para ser igual al modelo.
        public void RemoveBocadilloDeReseña(ReseñaItemDTO item)
        {
            Reseña.ReseñaItemDTOs.Remove(item);
            NotifyStateChanged();
        }

        // Vaciar lista (Equivalente a ClearRentingCart)
        public void ClearReseñaCart()
        {
            Reseña.ReseñaItemDTOs.Clear();
            NotifyStateChanged();
        }

        // Finalizar proceso (Equivalente a RentalProcessed)
        public void ReseñaProcessed()
        {
            // Reiniciamos el contenedor creando una nueva instancia
            Reseña = new ReseñaCreateDTO()
            {
                ReseñaItemDTOs = new List<ReseñaItemDTO>()
            };
            NotifyStateChanged();
        }

        // Métodos auxiliares que tenías (Includes/IsEmpty) 
        // Puedes mantenerlos o usar directamente las propiedades en la vista como hace el modelo
        public bool IsEmpty() => Reseña.ReseñaItemDTOs.Count == 0;

        public bool Includes(int id) => Reseña.ReseñaItemDTOs.Any(i => i.Id == id);
    }
}