using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface ICategoriaLocal
    {
        Task<IEnumerable<CategoriaLocal>> GetAll();
        Task<CategoriaLocal> GetById(Guid id);
        Task Add(CategoriaLocal categoriaLocal);
        Task Update(CategoriaLocal categoriaLocal);
        Task Delete(Guid id);
    }
}
