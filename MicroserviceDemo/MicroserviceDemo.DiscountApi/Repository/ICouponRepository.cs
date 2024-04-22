using MicroserviceDemo.DiscountApi.Models;

namespace MicroserviceDemo.DiscountApi.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetDiscountByProductId(string productId);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId);
    }
}