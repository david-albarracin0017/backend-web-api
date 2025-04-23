namespace MicroService_NaceTuIdea.Models
{
    public class DisponibilidadLocal
    {
        public Guid id { get; set; }
        public Guid LocalId { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public TimeSpan? horainicio { get; set; }
        public TimeSpan? horafin { get; set; }
        public bool disponible { get; set; }

        // Propiedad de navegación
        public Local Local { get; set; }
    }
}
