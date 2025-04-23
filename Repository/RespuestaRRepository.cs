using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class RespuestaRRepository : IRespuestaR
    {
        private readonly AppDbcontext _context;

        public RespuestaRRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RespuestaR>> GetAll()
        {
            return await _context.RespuestaRs
                .Include(r => r.Reseña)
                .Include(r => r.Propietario)
                .ToListAsync();
        }

        public async Task<RespuestaR> GetById(Guid id)
        {
            return await _context.RespuestaRs
                .Include(r => r.Reseña)
                .Include(r => r.Propietario)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task Add(RespuestaR respuesta)
        {
            _context.RespuestaRs.Add(respuesta);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RespuestaR respuesta)
        {
            _context.Entry(respuesta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var respuestaToDelete = await _context.RespuestaRs.FindAsync(id);
            if (respuestaToDelete != null)
            {
                _context.RespuestaRs.Remove(respuestaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
