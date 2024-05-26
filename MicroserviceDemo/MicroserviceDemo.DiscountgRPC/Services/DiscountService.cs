using Grpc.Core;
using MicroserviceDemo.DiscountgRPC.Protos;
using MicroserviceDemo.DiscountgRPC.Repository;

namespace MicroserviceDemo.DiscountgRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger)
        {
            _couponRepository = couponRepository;
            _logger = logger;
        }

        public override async Task<Coupon> GetDiscountByProductId(ProductId request, ServerCallContext context)
        {
            var existProductDiscount = await _couponRepository.GetDiscountByProductId(request.ProductId_);

            if (existProductDiscount is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Product cannot found! Please, try again."));

            var mapExistProductDiscount = new Coupon
            {
                Id = existProductDiscount.Id,
                ProductId = existProductDiscount.ProductId,
                Amount = existProductDiscount.Amount,
                ProductName = existProductDiscount.ProductName,
                Discription = existProductDiscount.Discription
            };

            //Set log
            _logger.LogInformation($"Discount retrive for Product Name: {mapExistProductDiscount.ProductName}, Amount: {mapExistProductDiscount.Amount}");

            return mapExistProductDiscount;
        }
    }
}