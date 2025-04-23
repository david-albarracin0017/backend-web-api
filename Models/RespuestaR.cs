namespace MicroService_NaceTuIdea.Models
{
    public class RespuestaR
    {
        public Guid id { get; set; }
        public Guid reseñaid { get; set; }
        public Guid propietarioid { get; set; }
        public string comentario { get; set; }
        public DateTime creacion { get; set; }

        // Propiedades de navegación
        public Reseña Reseña { get; set; }
        public Users Propietario { get; set; }
    }
}
