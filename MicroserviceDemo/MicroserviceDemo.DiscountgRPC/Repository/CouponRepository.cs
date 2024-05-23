﻿using Dapper;
using MicroserviceDemo.DiscountgRPC.Models;
using Npgsql;

namespace MicroserviceDemo.DiscountgRPC.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscountByProductId(string productId)
        {
            var npgSqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var existCoupon = await npgSqlConnection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupons WHERE ProductId = @ProductId", new { ProductId = productId });

            if (existCoupon == null)
                return new Coupon() { ProductName = "No Product", Amount = 0 };

            return existCoupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var npgSqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var newCoupon = await npgSqlConnection.ExecuteAsync("INSERT INTO Coupons(ProductId, ProductName, Discription, Amount) " +
                "VALUES(@ProductId, @ProductName, @Discription, @Amount)", new
                {
                    ProductId = coupon.ProductId,
                    ProductName = coupon.ProductName,
                    Discription = coupon.Discription,
                    Amount = coupon.Amount
                });

            if (newCoupon > 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var npgSqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var updateCoupon = await npgSqlConnection.ExecuteAsync("UPDATE Coupons SET ProductId = @ProductId, " +
                "ProductName = ProductName, Discription = @Discription, Amount = @Amount", new { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Discription = coupon.Discription, Amount = coupon.Amount });

            if (updateCoupon > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            var npgSqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var deleteCoupon = await npgSqlConnection.ExecuteAsync("DELETE FROM Coupons WHERE ProductId = @ProductId",
                new { ProductId = productId });

            if (deleteCoupon > 0)
                return true;

            return false;
        }
    }
}