namespace MicroService_NaceTuIdea.Models
{
    public class CategoriaLocal
    {
        public Guid id {  get; set; }
        public string nombre {  get; set; }
        public string descripcion { get; set; }
        public List<Local> Locales { get; set; } = new List<Local>();
    }
}
