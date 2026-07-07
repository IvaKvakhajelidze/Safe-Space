using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpace.Domain.Entities;

namespace SafeSpace.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.DateOfBirth)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasMany(u => u.Stories)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}
