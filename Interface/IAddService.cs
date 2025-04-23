using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IAddService
    {
        Task<IEnumerable<AddService>> GetAll();
        Task<AddService> GetById(Guid id);
        Task Add(AddService addService);
        Task Update(AddService addService);
        Task Delete(Guid id);
    }
}
