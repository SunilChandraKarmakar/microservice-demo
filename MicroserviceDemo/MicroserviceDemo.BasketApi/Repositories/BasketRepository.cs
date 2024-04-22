using MicroserviceDemo.BasketApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MicroserviceDemo.BasketApi.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;
        public BasketRepository(IDistributedCache distributedCache) => _distributedCache = distributedCache;

        public async Task<ShopingCart> GetShopingCardByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName)) 
                return new ShopingCart();

            var getShopingCard = await _distributedCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(getShopingCard))
                return new ShopingCart();

            var convertShopingCardObj = JsonConvert.DeserializeObject<ShopingCart>(getShopingCard);
            return convertShopingCardObj!;
        }

        public async Task<ShopingCart> UpsertAsync(ShopingCart shopingCart)
        {
            var jsonConvertShopingCart = JsonConvert.SerializeObject(shopingCart);
            await _distributedCache.SetStringAsync(shopingCart.UserName, jsonConvertShopingCart);
            var getUpsertShopingCard = await GetShopingCardByUserNameAsync(shopingCart.UserName);

            return getUpsertShopingCard;
        }

        public async Task<string> DeleteAsync(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
            return "Basket remove successfully,";
        }
    }
}