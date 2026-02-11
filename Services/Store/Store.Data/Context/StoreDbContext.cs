using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using System.Reflection;

namespace Store.Data.Context
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //10000
        public DbSet<User> Users { get; set; }
    }
}
