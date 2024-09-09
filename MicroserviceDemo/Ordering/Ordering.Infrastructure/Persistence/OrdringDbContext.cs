using Microsoft.EntityFrameworkCore;
using Ordering.Model.Models;

namespace Ordering.Infrastructure.Persistence
{
    public class OrdringDbContext : DbContext
    {
        public OrdringDbContext(DbContextOptions<OrdringDbContext> options) : base(options)
        {
            
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Payment> Payments { get; set; }    
        public DbSet<Order> Orders { get; set; }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            await OrderingDbContextSeedData.SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}