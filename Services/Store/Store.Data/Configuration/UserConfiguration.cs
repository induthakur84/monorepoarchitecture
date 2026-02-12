using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(builder => builder.Name).IsRequired();
            builder.HasIndex(builder=>builder.Name).IsUnique();
            

        }
    }
}

// user or userprofile
//name
//email
