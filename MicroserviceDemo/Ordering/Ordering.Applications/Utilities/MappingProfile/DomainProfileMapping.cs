using AutoMapper;
using Ordering.Applications.Features.OrderLogic.Model;
using Ordering.Model.Models;

namespace Ordering.Applications.Utilities.MappingProfile
{
    internal class DomainProfileMapping : Profile
    {
        public DomainProfileMapping()
        {
            // Order mapping
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>()
                .ForMember(d => d.Address, s => s.MapFrom(m => m.Address))
                .ForMember(d => d.Payment, s => s.MapFrom(m => m.Payment));
            CreateMap<Order, OrderGridModel>();
            CreateMap<OrderGridModel, Order>();

            // Address mapping
            CreateMap<Address, AddressModel>();
            CreateMap<AddressModel, Address>();

            // Payment mapping
            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel, Payment>();
        }
    }
}