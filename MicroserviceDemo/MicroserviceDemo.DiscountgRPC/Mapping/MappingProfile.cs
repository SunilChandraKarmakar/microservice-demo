using AutoMapper;
using MicroserviceDemo.DiscountgRPC.Models;
using MicroserviceDemo.DiscountgRPC.Protos;

namespace MicroserviceDemo.DiscountgRPC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coupon, CouponReturns>().ReverseMap();
        }
    }
}