using MicroserviceDemo.BasketApi.GrpcServices;
using MicroserviceDemo.BasketApi.Models;
using MicroserviceDemo.BasketApi.Repositories;
using MicroserviceDemo.DiscountgRPC.Protos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroserviceDemo.BasketApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShopingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShopingCart>> GetShopingCardByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("User name is not currect! Please, try again.");

            var getShoingCard = await _basketRepository.GetShopingCardByUserNameAsync(userName);

            if (string.IsNullOrEmpty(getShoingCard.UserName))
                return BadRequest("Shoping cart cannot found! Please, try again.");

            return Ok(getShoingCard);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShopingCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShopingCart>> Upsert(ShopingCart shopingCart)
        {
            if (string.IsNullOrEmpty(shopingCart.UserName))
                return BadRequest("User name is not currect! Please, try again.");

            // Check product have any discount
            foreach (var product in shopingCart.ShopingCartItems)
            {
                var productId = new GetDiscountByProductIdRequest { ProductId =  product.ProductId };
                var getDiscount = await _discountGrpcService.GetDiscountByProductId(productId);

                product.Price = product.Price - getDiscount.Amount;
            }

            var upsertShopingCart = await _basketRepository.UpsertAsync(shopingCart);

            if (string.IsNullOrEmpty(upsertShopingCart.UserName))
                return BadRequest("Shoping cart cannot save in backet! Please, try again.");

            return Ok(upsertShopingCart);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Delete(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("User name is not currect! Please, try again.");

            var deleteShopingCart = await _basketRepository.DeleteAsync(userName);

            if(string.IsNullOrEmpty(deleteShopingCart))
                return BadRequest("Shoping cart is not deleted! Please, try again.");

            return Ok(deleteShopingCart);
        }
    }
}