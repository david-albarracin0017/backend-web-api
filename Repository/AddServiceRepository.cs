using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class AddServiceRepository : IAddService
    {
        private readonly AppDbcontext _context;

        public AddServiceRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddService>> GetAll()
        {
            return await _context.AddServices
                .Include(s => s.Local)
                .ToListAsync();
        }

        public async Task<AddService> GetById(Guid id)
        {
            return await _context.AddServices
                .Include(s => s.Local)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Add(AddService addService)
        {
            _context.AddServices.Add(addService);
            await _context.SaveChangesAsync();
        }

        public async Task Update(AddService addService)
        {
            _context.Entry(addService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var addServiceToDelete = await _context.AddServices.FindAsync(id);
            if (addServiceToDelete != null)
            {
                _context.AddServices.Remove(addServiceToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
