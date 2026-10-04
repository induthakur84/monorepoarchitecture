using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // CreatedAt is required → Order must always have creation date
            builder.Property(x => x.CreatedAt).IsRequired();

            // ✅ One-to-Many Relationship
            // One User can have many Orders
            // Each Order belongs to one User
            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)

                   // 🔥 Delete Behavior Explanation (Simple Language)

                   // Restrict:
                   // 👉 Prevent deleting User if Orders exist
                   // Example:
                   // If User has 5 Orders, DB will NOT allow deleting that User.
                   // This protects important data (Orders history stays safe).
                   //
                   // Cascade:
                   // 👉 If User is deleted, ALL related Orders will be deleted automatically.
                   // Useful when child data has no meaning without parent.
                   //
                   // ⭐ In real systems (like ecommerce), we usually use Restrict
                   // because Orders are important records and should not be lost.
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}