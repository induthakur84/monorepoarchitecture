using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Data.Configuration
{
    

    // if all the order table  that user related records deleted then you are able to delete that user because here we using restrict
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



    // if 

    //you try to delted user with id 1,
    //but user with id 1 has orders in the order table,

    //order table have fk userid =1
}

//order  --- userId 1 -- delete order where userId 1

//user  1-- ram --- you want to delete ram but ram has orders,
//so you can't delete ram because of the foreign key constraint.
//you have to delete the orders first then you can delete ram.



//ef core provides different delete behaviors for handling related data
//when a parent entity is deleted.
//The two most common delete behaviors are Restrict and Cascade:
//Restrict and cascade


