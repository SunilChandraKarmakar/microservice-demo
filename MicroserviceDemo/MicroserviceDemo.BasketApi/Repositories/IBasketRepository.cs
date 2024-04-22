using MicroserviceDemo.BasketApi.Models;

namespace MicroserviceDemo.BasketApi.Repositories
{
    public interface IBasketRepository
    {
        Task<ShopingCart> GetShopingCardByUserNameAsync(string userName);
        Task<ShopingCart> UpsertAsync(ShopingCart shopingCart);
        Task<string> DeleteAsync(string userName);
    }
}