using MicroService_NaceTuIdea.Models;

namespace MicroService_NaceTuIdea.Interface
{
    public interface IRespuestaR
    {
        Task<IEnumerable<RespuestaR>> GetAll();
        Task<RespuestaR> GetById(Guid id);
        Task Add(RespuestaR respuesta);
        Task Update(RespuestaR respuesta);
        Task Delete(Guid id);
    }
}
