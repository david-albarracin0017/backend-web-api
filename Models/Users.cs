namespace MicroService_NaceTuIdea.Models
{
    public class Users
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public DateTime registro { get; set; }
        public bool propietario { get; set; }
        

        // Propiedades de navegación (para las relaciones definidas en DbContext)
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
        public List<Reseña> Reseñas { get; set; } = new List<Reseña>();
        public List<RespuestaR> Respuestas { get; set; } = new List<RespuestaR>(); 
        public List<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public List<Local> Locales { get; set; } = new List<Local>(); 
        
    }
}
