using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IUsers
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetById(Guid id);
        Task Add(Users user);
        Task Update(Users user);
        Task Delete(Guid id);
    }
}
