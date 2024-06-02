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

            // Set log
            _logger.LogInformation($"Discount retrive for Product Name: {mapExistProductDiscount.ProductName}, Amount: {mapExistProductDiscount.Amount}");

            return mapExistProductDiscount;
        }

        public override async Task<Result> CreateDiscount(Coupon request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Models.Coupon>(request);
            var isCouponSaved = await _couponRepository.CreateDiscount(coupon);

            if (isCouponSaved)
            {
                _logger.LogInformation($"Discount is successfully created. Product name {coupon.ProductName}");
                return new Result { Success = true };
            }

            _logger.LogInformation($"Discount is not created. Please, try again.");
            return new Result { Success = false };
        }

        public override async Task<Result> UpdateDiscount(Coupon request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Models.Coupon>(request);
            var isCouponUpdated = await _couponRepository.UpdateDiscount(coupon);

            if (isCouponUpdated)
            {
                _logger.LogInformation($"Discount is updated. Product name {coupon.ProductName}");
                return new Result { Success = true };
            }

            _logger.LogInformation($"Discount is not updated. Please, try again.");
            return new Result { Success = false };
        }

        public override async Task<Result> DeleteDiscount(ProductId request, ServerCallContext context)
        {
            var isCoupnDeleted = await _couponRepository.DeleteDiscount(request.ProductId_);

            if (isCoupnDeleted)
            {
                _logger.LogInformation($"Discount is deleted.");
                return new Result { Success = true };
            }

            _logger.LogInformation($"Discount is not deleted. Please, try again.");
            return new Result { Success = false };
        }
    }
}