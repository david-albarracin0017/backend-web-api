using Microsoft.AspNetCore.Components.Web;

namespace MicroService_NaceTuIdea.Models
{
    public class Notificacion
    {
        public Guid id { get; set; }
        public Guid usuarioid { get; set; }
        public string tipo { get; set; }
        public string mensaje { get; set; }
        public DateTime creacion { get; set; }
        public bool vista { get; set; }

        // Propiedad de navegación
        public Users Usuario { get; set; }
    }
}
