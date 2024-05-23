using MicroserviceDemo.DiscountgRPC.Models;

namespace MicroserviceDemo.DiscountgRPC.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetDiscountByProductId(string productId);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId);
    }
}