using Microsoft.EntityFrameworkCore;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Common;
using SafeSpace.Infrastructure.Data;

namespace SafeSpace.Infrastructure.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Service(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            T entityFromDb = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (entityFromDb != null)
            {
                _dbSet.Remove(entityFromDb);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
