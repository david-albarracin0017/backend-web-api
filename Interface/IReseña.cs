using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IReseña
    {
        Task<IEnumerable<Reseña>> GetAll();
        Task<Reseña> GetById(Guid id);
        Task Add(Reseña reseña);
        Task Update(Reseña reseña);
        Task Delete(Guid id);
    }
}
