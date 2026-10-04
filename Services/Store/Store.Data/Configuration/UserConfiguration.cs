using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Data.Configuration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.PhoneNumber)
                   .HasMaxLength(20);

            builder.HasIndex(p => p.UserId).IsUnique();

            // ✅ One-to-One Relationship
            builder.HasOne(p => p.User)
                   .WithOne(u => u.UserProfile)
                   .HasForeignKey<UserProfile>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}