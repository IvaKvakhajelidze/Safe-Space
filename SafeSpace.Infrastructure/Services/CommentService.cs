using Microsoft.EntityFrameworkCore;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.Infrastructure.Data;

namespace SafeSpace.Infrastructure.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        public CommentService(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Comment>> GetCommentsByStoryAsync(Guid storyId)
        {
            return await _context.Comments.Where(c => c.StoryId == storyId).ToListAsync();
        }
    }
}
