using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class ComodidadLocalRepository : IComodidadLocal
    {
        private readonly AppDbcontext _context;

        public ComodidadLocalRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComodidadLocal>> GetAll()
        {
            return await _context.ComodidadLocals
                .ToListAsync(); 
        }

        public async Task<ComodidadLocal> GetById(Guid id)
        {
            return await _context.ComodidadLocals
                .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task Add(ComodidadLocal comodidadLocal)
        {
            _context.ComodidadLocals.Add(comodidadLocal);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ComodidadLocal comodidadLocal)
        {
            _context.Entry(comodidadLocal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var comodidadLocalToDelete = await _context.ComodidadLocals.FindAsync(id);
            if (comodidadLocalToDelete != null)
            {
                _context.ComodidadLocals.Remove(comodidadLocalToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
