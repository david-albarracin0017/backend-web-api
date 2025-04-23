using System.ComponentModel.DataAnnotations.Schema;

namespace MicroService_NaceTuIdea.Models
{
    public class Reserva
    {
        public Guid id { get; set; }
        public Guid usuarioid { get; set; }
        public Guid localid { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public int asistentes { get; set; }
        public decimal precio { get; set; }
        public DateTime reserva { get; set; }
        public string estado { get; set; }

        // Propiedades de navegación
        public Users Usuario { get; set; }
        public Local Local { get; set; }
    }
}
