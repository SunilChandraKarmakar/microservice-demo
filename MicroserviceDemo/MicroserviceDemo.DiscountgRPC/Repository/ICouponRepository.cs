using MicroserviceDemo.DiscountgRPC.Models;

namespace MicroserviceDemo.DiscountgRPC.Repository
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetDiscounts();
        Task<Coupon> GetDiscountByProductId(string productId);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId);
    }
}