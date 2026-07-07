using SafeSpace.Domain.Entities;

namespace SafeSpace.Application.Services
{
    public interface IUserService : IService<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<bool> EmailExistsAsync(string email);

        Task RegisterAsync(User user);

        Task<User?> LoginAsync(string email, string password);

        Task DeleteAccountAsync(Guid userId);
    }
}
