using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpace.Domain.Entities;

namespace SafeSpace.Infrastructure.Configurations
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired();

            builder.Property(s => s.Text)
                .IsRequired();

            builder.Property(s => s.Title)
                .HasMaxLength(100);

            builder.HasMany(s => s.Comments)
                .WithOne(c => c.Story)
                .HasForeignKey(c => c.StoryId);
        }
    }
}
