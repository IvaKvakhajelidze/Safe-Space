using Microsoft.EntityFrameworkCore;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.Infrastructure.Data;

namespace SafeSpace.Infrastructure.Services
{
    public class StoryService : Service<Story>, IStoryService
    {
        public StoryService(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Story>> GetStoriesWithCommentsAsync()
        {
            return await _context.Stories
                .Where(s => !s.IsDeleted)
                .Include(s => s.Comments)
                .ThenInclude(c => c.User)
                .Include(s => s.User)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetStoriesByUserAsync(Guid userId)
        {
            return await _context.Stories
                .Where(s => s.UserId == userId && !s.IsDeleted)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }
    }
}
