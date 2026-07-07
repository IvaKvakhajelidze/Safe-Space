using Microsoft.EntityFrameworkCore;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.Domain.Exceptions;
using SafeSpace.Infrastructure.Data;

namespace SafeSpace.Infrastructure.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(ApplicationDbContext context) : base(context)
        { }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task RegisterAsync(User user)
        {
            if (await EmailExistsAsync(user.Email))
            {
                throw new CanNotUseTheSameEmailTwiceException(user.Email);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            await CreateAsync(user);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid)
            {
                return null;
            }

            return user;
        }

        public async Task DeleteAccountAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return;
            }

            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
