using MicroserviceDemo.DiscountgRPC.Protos;

namespace MicroserviceDemo.BasketApi.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoGrpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoGrpcClient = discountProtoServiceClient;
        }

        public async Task<CouponReturns> GetDiscountByProductId(GetDiscountByProductIdRequest request)
        {
            var getDiscount = await _discountProtoGrpcClient.GetDiscountByProductIdAsync(request);
            return getDiscount;
        }
    }
}