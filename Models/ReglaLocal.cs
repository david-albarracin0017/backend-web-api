namespace MicroService_NaceTuIdea.Models
{
    public class ReglaLocal
    {
        public Guid id { get; set; }
        public string descripcion { get; set; }

        
        public Guid LocalId { get; set; }
        public Local Local { get; set; }
    }
}
