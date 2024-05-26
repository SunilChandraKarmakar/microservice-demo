using AutoMapper;
using MicroserviceDemo.DiscountgRPC.Models;

namespace MicroserviceDemo.DiscountgRPC.AutomapperProfile
{
    public class AutomapperProfileMapping : Profile
    {
        public AutomapperProfileMapping()
        {
            CreateMap<Coupon, Protos.Coupon>().ReverseMap();
        }
    }
}