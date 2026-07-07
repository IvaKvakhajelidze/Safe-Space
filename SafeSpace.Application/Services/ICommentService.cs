using SafeSpace.Domain.Entities;

namespace SafeSpace.Application.Services
{
    public interface ICommentService : IService<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByStoryAsync(Guid storyId);
    }
}
