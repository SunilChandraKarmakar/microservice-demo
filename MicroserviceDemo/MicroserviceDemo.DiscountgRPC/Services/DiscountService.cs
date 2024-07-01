using AutoMapper;
using Grpc.Core;
using MicroserviceDemo.DiscountgRPC.Models;
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

        public override async Task<CouponReturns> GetDiscountByProductId(GetDiscountByProductIdRequest request, ServerCallContext context)
        {
            var getDiscount = await _couponRepository.GetDiscountByProductId(request.ProductId);

            if (getDiscount is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Discount cannot found."));

            var mapGetCoupon = _mapper.Map<CouponReturns>(getDiscount);
            _logger.LogInformation($"Discount is retrive for Product Name = {mapGetCoupon.ProductName}, Amount = {mapGetCoupon.Amount}");

            return mapGetCoupon;
        }

        public override async Task<CouponReturns> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var createCoupon = _mapper.Map<Coupon>(request);
            var isSaved = await _couponRepository.CreateDiscount(createCoupon);

            if(isSaved)
            {
                _logger.LogInformation($"Discount is ceated. Product Name = {createCoupon.ProductName}, Amount = {createCoupon.Amount}");

                var mapCreateCoupon = _mapper.Map<CouponReturns>(createCoupon);
                return mapCreateCoupon;
            }

            _logger.LogInformation($"Discount is not ceated. Try again.");
            return new CouponReturns { Id = request.Id, ProductName = request.ProductName, ProductId = request.ProductId };
        }

        public override async Task<CouponReturns> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var updateCoupon = _mapper.Map<Coupon>(request);
            var isUpdate = await _couponRepository.UpdateDiscount(updateCoupon);

            if (isUpdate)
            {
                _logger.LogInformation($"Discount is updated. Product Name = {updateCoupon.ProductName}, Amount = {updateCoupon.Amount}");

                var mapUpdateCoupon = _mapper.Map<CouponReturns>(updateCoupon);
                return mapUpdateCoupon;
            }

            _logger.LogInformation($"Discount is not ceated. Try again.");
            return new CouponReturns { Id = request.Id, ProductName = request.ProductName, ProductId = request.ProductId };
        }

        public override async Task<DeleteDiscountReturns> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var isDeleted = await _couponRepository.DeleteDiscount(request.ProductId);

            if(isDeleted)
            {
                _logger.LogInformation($"Discount is deleted.");
                return new DeleteDiscountReturns { Success = true };
            }

            _logger.LogInformation($"Discount is not deleted.");
            return new DeleteDiscountReturns { Success = false };
        }
    }
}