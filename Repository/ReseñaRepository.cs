using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class ReseñaRepository : IReseña
    {
        private readonly AppDbcontext _context;

        public ReseñaRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reseña>> GetAll()
        {
            return await _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Local)
                .Include(r => r.Respuesta)
                .ToListAsync();
        }

        public async Task<Reseña> GetById(Guid id)
        {
            return await _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Local)
                .Include(r => r.Respuesta)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task Add(Reseña reseña)
        {
            _context.Reseñas.Add(reseña);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Reseña reseña)
        {
            _context.Entry(reseña).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var reseñaToDelete = await _context.Reseñas.FindAsync(id);
            if (reseñaToDelete != null)
            {
                _context.Reseñas.Remove(reseñaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
