using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService_NaceTuIdea.Repository
{
    public class UsersRepository : IUsers
    {
        private readonly AppDbcontext _context;

        public UsersRepository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users
                .Include(u => u.Reservas)
                .Include(u => u.Reseñas)
                .Include(u => u.Respuestas)
                .Include(u => u.Notificaciones)
                .Include(u => u.Locales)
                .ToListAsync();
        }

        public async Task<Users> GetById(Guid id)
        {
            return await _context.Users
                .Include(u => u.Reservas)
                .Include(u => u.Reseñas)
                .Include(u => u.Respuestas)
                .Include(u => u.Notificaciones)
                .Include(u => u.Locales)
                .FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task Add(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Users user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
