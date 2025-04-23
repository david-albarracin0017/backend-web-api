using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class NotificacionRepository : INotificacion
    {
        private readonly AppDbcontext _context;

        public NotificacionRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notificacion>> GetAll()
        {
            return await _context.Notificacions 
                .Include(n => n.Usuario)
                .ToListAsync();
        }

        public async Task<Notificacion> GetById(Guid id)
        {
            return await _context.Notificacions 
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(n => n.id == id);
        }

        public async Task Add(Notificacion notificacion)
        {
            _context.Notificacions.Add(notificacion);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Notificacion notificacion)
        {
            _context.Entry(notificacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var notificacionToDelete = await _context.Notificacions.FindAsync(id);
            if (notificacionToDelete != null)
            {
                _context.Notificacions.Remove(notificacionToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
