using AutoMapper;
using EventBus.Messages.Events;
using MicroserviceDemo.BasketApi.Models;

namespace MicroserviceDemo.BasketApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderCheckout, OrderCheckoutEvent>()
                .ForMember(d => d.OrderModel, s => s.MapFrom(m => m.OrderModel))
                .ForMember(d => d.PaymentModel, s => s.MapFrom(m => m.PaymentModel))
                .ForMember(d => d.AddressModel, s => s.MapFrom(m => m.AddressModel));
        }
    }
}