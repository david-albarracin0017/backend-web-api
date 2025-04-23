using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IDisponibilidadLocal
    {
        Task<IEnumerable<DisponibilidadLocal>> GetAll();
        Task<DisponibilidadLocal> GetById(Guid id);
        Task Add(DisponibilidadLocal disponibilidadLocal);
        Task Update(DisponibilidadLocal disponibilidadLocal);
        Task Delete(Guid id);
    }
}
