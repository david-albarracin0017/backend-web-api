using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class ReglaLocalRepository : IReglaLocal
    {
        private readonly AppDbcontext _context;

        public ReglaLocalRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReglaLocal>> GetAll()
        {
            return await _context.ReglaLocals
                .Include(r => r.Local)
                .ToListAsync();
        }

        public async Task<ReglaLocal> GetById(Guid id)
        {
            return await _context.ReglaLocals
                .Include(r => r.Local)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task Add(ReglaLocal reglaLocal)
        {
            _context.ReglaLocals.Add(reglaLocal);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ReglaLocal reglaLocal)
        {
            _context.Entry(reglaLocal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var reglaLocalToDelete = await _context.ReglaLocals.FindAsync(id);
            if (reglaLocalToDelete != null)
            {
                _context.ReglaLocals.Remove(reglaLocalToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
