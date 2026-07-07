using SafeSpace.Domain.Common;

namespace SafeSpace.Application.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(Guid id);
    }
}
