using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IComodidadLocal
    {
        Task<IEnumerable<ComodidadLocal>> GetAll();
        Task<ComodidadLocal> GetById(Guid id);
        Task Add(ComodidadLocal comodidadLocal);
        Task Update(ComodidadLocal comodidadLocal);
        Task Delete(Guid id);
    }
}
