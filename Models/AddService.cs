namespace MicroService_NaceTuIdea.Models
{
    public class AddService
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        
        public Guid LocalId { get; set; }
        public Local Local { get; set; }
    }
}
