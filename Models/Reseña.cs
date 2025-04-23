namespace MicroService_NaceTuIdea.Models
{
    public class Reseña
    {
        public Guid id { get; set; }
        public Guid userid { get; set; }
        public Guid localid { get; set; }
        public int calificacion { get; set; }
        public string comentario { get; set; }
        public DateTime creacion { get; set; }
        public Guid respuestaid { get; set; }

        // Propiedades de navegación
        public Users Usuario { get; set; }
        public Local Local { get; set; }
        public RespuestaR Respuesta { get; set; }
    }
}
