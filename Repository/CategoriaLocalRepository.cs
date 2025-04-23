using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class CategoriaLocalRepository : ICategoriaLocal
    {
        private readonly AppDbcontext _context;

        public CategoriaLocalRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaLocal>> GetAll()
        {
            return await _context.CategoriaLocals
                .ToListAsync(); 
        }

        public async Task<CategoriaLocal> GetById(Guid id)
        {
            return await _context.CategoriaLocals
                .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task Add(CategoriaLocal categoriaLocal)
        {
            _context.CategoriaLocals.Add(categoriaLocal);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CategoriaLocal categoriaLocal)
        {
            _context.Entry(categoriaLocal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var categoriaLocalToDelete = await _context.CategoriaLocals.FindAsync(id);
            if (categoriaLocalToDelete != null)
            {
                _context.CategoriaLocals.Remove(categoriaLocalToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
