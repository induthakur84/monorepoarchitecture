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
           builder.Property(x=>x.CreatedAt).IsRequired();

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
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


