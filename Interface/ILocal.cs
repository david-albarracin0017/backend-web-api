using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface ILocal
    {
        Task<IEnumerable<Local>> GetAll();
        Task<Local> GetById(Guid id);
        Task Add(Local local);
        Task Update(Local local);
        Task Delete(Guid id);
    }
}
