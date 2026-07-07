using Microsoft.EntityFrameworkCore;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.Infrastructure.Data;

namespace SafeSpace.Infrastructure.Services
{
    public class CommentLikeService : Service<CommentLike>, ICommentLikeService
    {
        public CommentLikeService(ApplicationDbContext context) : base(context)
        { }

        public async Task AddLikeAsync(Guid commentId, Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingLike = await _context.CommentLike.FirstOrDefaultAsync(l => l.CommentId == commentId && l.UserId == userId);

                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);


                if (existingLike == null)
                {
                    _context.CommentLike.Add(new CommentLike
                    {
                        CommentId = commentId,
                        UserId = userId
                    });

                    comment.likeAmount++;
                }
                else
                {
                    _context.CommentLike.Remove(existingLike);
                    comment.likeAmount = Math.Max(0, comment.likeAmount - 1);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> HasUserLikedAsync(Guid commentId, Guid userId)
        {
            return await _context.CommentLike.AnyAsync(l => l.CommentId == commentId && l.UserId == userId);
        }
    }
}
