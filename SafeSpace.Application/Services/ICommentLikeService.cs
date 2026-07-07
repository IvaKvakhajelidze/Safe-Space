using SafeSpace.Domain.Entities;

namespace SafeSpace.Application.Services
{
    public interface ICommentLikeService : IService<CommentLike>
    {
        Task AddLikeAsync(Guid commentId, Guid userId);
        Task<bool> HasUserLikedAsync(Guid commentId, Guid userId);
    }
}
