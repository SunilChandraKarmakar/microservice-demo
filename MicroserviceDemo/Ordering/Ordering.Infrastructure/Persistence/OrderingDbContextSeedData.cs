using Microsoft.EntityFrameworkCore;
using Ordering.Model.Models;

namespace Ordering.Infrastructure.Persistence
{
    public static class OrderingDbContextSeedData
    {
        public static async Task SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    FirstName = "Sunil",
                    LastName = "Karmakar",
                    Email = "sunil_karmakar@ymail.com",
                    AddressLineOne = "Test Sunil Address 1001",
                    CreatedBy = "Sunil",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "Sunil",
                    UpdatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    CardName = "CT MAX",
                    CardNumber = "JHYT65KL",
                    CVV = "KSEQ232L",
                    Expiration = DateTime.Now.AddYears(1),
                    PaymentMethod = "Online",
                    CreatedBy = "Sunil",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "Sunil",
                    UpdatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserName =  "sunil_karmakar@ymail.com",
                    TotalPrice = 600,
                    AddressId = 1,
                    PaymentId = 1,
                    CreatedBy = "Sunil",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "Sunil",
                    UpdatedDate = DateTime.Now
                }
            );
        }
    }
}