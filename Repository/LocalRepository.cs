using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class LocalRepository : ILocal
    {
        private readonly AppDbcontext _context;

        public LocalRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Local>> GetAll()
        {
            return await _context.locals 
                .Include(l => l.Propietario)
                .Include(l => l.Reseñas)
                .Include(l => l.Reservas)
                .Include(l => l.Comodidades)
                .Include(l => l.Reglas)
                .Include(l => l.ServiciosAdicionales)
                .Include(l => l.Disponibilidades)
                .Include(l => l.Categorias)
                .ToListAsync();
        }

        public async Task<Local> GetById(Guid id)
        {
            return await _context.locals 
                .Include(l => l.Propietario)
                .Include(l => l.Reseñas)
                .Include(l => l.Reservas)
                .Include(l => l.Comodidades)
                .Include(l => l.Reglas)
                .Include(l => l.ServiciosAdicionales)
                .Include(l => l.Disponibilidades)
                .Include(l => l.Categorias)
                .FirstOrDefaultAsync(l => l.id == id);
        }

        public async Task Add(Local local)
        {
            _context.locals.Add(local);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Local local)
        {
            _context.Entry(local).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var localToDelete = await _context.locals.FindAsync(id);
            if (localToDelete != null)
            {
                _context.locals.Remove(localToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
