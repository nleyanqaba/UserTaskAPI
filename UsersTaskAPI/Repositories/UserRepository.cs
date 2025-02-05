using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserTasksAPI.Data;
using UserTasksAPI.Models;

namespace UserTasksAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserTaskDbContext _context;

        public UserRepository(UserTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task<User> GetById(int id) => await _context.Users.FindAsync(id);

        public async Task<User?> GetByEmail(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task Add(User entity)
        {
            entity.PasswordHash = HashPassword(entity.Password);
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await GetByEmail(email);
            return user != null && user.PasswordHash == HashPassword(password);
        }
    }
}
