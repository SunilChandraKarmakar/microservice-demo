using MicroserviceDemo.DiscountgRPC.Protos;

namespace MicroserviceDemo.BasketApi.GrpcServices
{
    public class DiscountGrpcService : DiscountProtoService.DiscountProtoServiceClient
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<Coupon> GetDiscountByProductIdAsync(string productId)
        {
            var productIdRequest = new ProductId { ProductId_ = productId };
            var getDiscountByProductId =  await _discountProtoServiceClient.GetDiscountByProductIdAsync(productIdRequest);

            return getDiscountByProductId;
        }
    }
}