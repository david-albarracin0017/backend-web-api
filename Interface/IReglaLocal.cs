using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IReglaLocal
    {
        Task<IEnumerable<ReglaLocal>> GetAll();
        Task<ReglaLocal> GetById(Guid id);
        Task Add(ReglaLocal reglaLocal);
        Task Update(ReglaLocal reglaLocal);
        Task Delete(Guid id);
    }
}
