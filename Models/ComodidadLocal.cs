namespace MicroService_NaceTuIdea.Models
{
    public class ComodidadLocal
    {
        public Guid id { get; set; }
        public string name { get; set; }

        // Propiedad de navegación para la relación muchos a muchos con Local
        public List<Local> Locales { get; set; } = new List<Local>();
    }
}
