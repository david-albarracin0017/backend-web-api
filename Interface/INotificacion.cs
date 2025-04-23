using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface INotificacion
    {
        Task<IEnumerable<Notificacion>> GetAll();
        Task<Notificacion> GetById(Guid id);
        Task Add(Notificacion notificacion);
        Task Update(Notificacion notificacion);
        Task Delete(Guid id);
    }
}
