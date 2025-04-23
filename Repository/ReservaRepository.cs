using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class ReservaRepository : IReserva
    {
        private readonly AppDbcontext _context;

        public ReservaRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAll()
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Local)
                .ToListAsync();
        }

        public async Task<Reserva> GetById(Guid id)
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Local)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task Add(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Reserva reserva)
        {
            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var reservaToDelete = await _context.Reservas.FindAsync(id);
            if (reservaToDelete != null)
            {
                _context.Reservas.Remove(reservaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
