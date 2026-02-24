using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder.Property(x=>x.CreatedAt).IsRequired();
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

//order  --- userId 1 -- delete order where userId 1

//user  1-- ram --- you want to delete ram but ram has orders,
//so you can't delete ram because of the foreign key constraint.
//you have to delete the orders first then you can delete ram.





