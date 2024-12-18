using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService,
            IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
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

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> OrderCheckout(OrderCheckout orderCheckout)
        {
            // Get exist item in the basket
            var existItem = await _basketRepository.GetShopingCardByUserNameAsync(orderCheckout.OrderModel.UserName);

            if (existItem is null)
                return BadRequest("Basket is empty! Try again.");

            // Publish event using rabitMQ
            var mapOrderCheckoutModel = _mapper.Map<OrderCheckoutEvent>(orderCheckout);
            await _publishEndpoint.Publish(mapOrderCheckoutModel);

            // Remove exist basket
            var deleteShopingCart = await _basketRepository.DeleteAsync(orderCheckout.OrderModel.UserName);

            return "Ok";
        }
    }
}