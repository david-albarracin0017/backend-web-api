using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IReserva
    {
        Task<IEnumerable<Reserva>> GetAll();
        Task<Reserva> GetById(Guid id);
        Task Add(Reserva reserva);
        Task Update(Reserva reserva);
        Task Delete(Guid id);
    }
}
