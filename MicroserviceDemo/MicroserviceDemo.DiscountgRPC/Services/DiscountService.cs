using AutoMapper;
using Grpc.Core;
using MicroserviceDemo.DiscountgRPC.Protos;
using MicroserviceDemo.DiscountgRPC.Repository;

namespace MicroserviceDemo.DiscountgRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<Coupon> GetDiscountByProductId(ProductId request, ServerCallContext context)
        {
            var existProductDiscount = await _couponRepository.GetDiscountByProductId(request.ProductId_);

            if (existProductDiscount is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Product cannot found! Please, try again."));

            var mapExistProductDiscount = _mapper.Map<Coupon>(existProductDiscount);

            //Set log
            _logger.LogInformation($"Discount retrive for Product Name: {mapExistProductDiscount.ProductName}, Amount: {mapExistProductDiscount.Amount}");

            return mapExistProductDiscount;
        }
    }
}