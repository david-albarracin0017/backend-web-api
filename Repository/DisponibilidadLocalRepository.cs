using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class DisponibilidadLocalRepository : IDisponibilidadLocal
    {
        private readonly AppDbcontext _context;

        public DisponibilidadLocalRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DisponibilidadLocal>> GetAll()
        {
            return await _context.DisponibilidadLocals
                .Include(d => d.Local)
                .ToListAsync();
        }

        public async Task<DisponibilidadLocal> GetById(Guid id)
        {
            return await _context.DisponibilidadLocals
                .Include(d => d.Local)
                .FirstOrDefaultAsync(d => d.id == id);
        }

        public async Task Add(DisponibilidadLocal disponibilidadLocal)
        {
            _context.DisponibilidadLocals.Add(disponibilidadLocal);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DisponibilidadLocal disponibilidadLocal)
        {
            _context.Entry(disponibilidadLocal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var disponibilidadLocalToDelete = await _context.DisponibilidadLocals.FindAsync(id);
            if (disponibilidadLocalToDelete != null)
            {
                _context.DisponibilidadLocals.Remove(disponibilidadLocalToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
