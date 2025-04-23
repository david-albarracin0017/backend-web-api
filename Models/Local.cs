using System.ComponentModel.DataAnnotations.Schema;

namespace MicroService_NaceTuIdea.Models
{
    public class Local
    {
        public Guid id { get; set; }
        public Guid propietarioid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal precioxhora { get; set; }
        public string direccion { get; set; }
        public int capacidad { get; set; }
        public List<ComodidadLocal> Comodidades { get; set; } = new List<ComodidadLocal>();
        public List<string> FotosUrls { get; set; } = new List<string>();
        public List<string> VideosUrls { get; set; } = new List<string>();
        public List<ReglaLocal> Reglas { get; set; } = new List<ReglaLocal>();
        public List<AddService> ServiciosAdicionales { get; set; } = new List<AddService>();
        public List<DisponibilidadLocal> Disponibilidades { get; set; } = new List<DisponibilidadLocal>();
        public List<Guid> CategoriasIds { get; set; } = new List<Guid>(); // Referencias a las categorías del local

        // Propiedades de navegación
        public Users Propietario { get; set; }
        public List<Reseña> Reseñas { get; set; } = new List<Reseña>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
        public List<CategoriaLocal> Categorias { get; set; } = new List<CategoriaLocal>();
        public List<Users> UsuariosFavoritos { get; set; } = new List<Users>();
    }
}
