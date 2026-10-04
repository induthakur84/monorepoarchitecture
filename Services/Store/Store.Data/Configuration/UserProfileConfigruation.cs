using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Data.Configuration
{
    public class UserProfileConfigruation : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(p => p.Address)
                    .IsRequired();

            builder.HasOne(p => p.User)
         .WithOne(u => u.UserProfile)
         .HasForeignKey<UserProfile>(p => p.UserId)
         // ✅ Cascade Delete:
         // If a User is deleted, the related UserProfile will be automatically deleted
         // by the database. This prevents orphan records (UserProfile without User).
         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
