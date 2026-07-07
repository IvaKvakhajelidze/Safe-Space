using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SafeSpace.Domain.Entities;

namespace SafeSpace.Infrastructure.Configurations
{
    public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.HasKey(cl => new { cl.CommentId, cl.UserId });

            builder.HasOne(cl => cl.Comment)
                .WithMany()
                .HasForeignKey(cl => cl.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cl => cl.User)
                .WithMany()
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
