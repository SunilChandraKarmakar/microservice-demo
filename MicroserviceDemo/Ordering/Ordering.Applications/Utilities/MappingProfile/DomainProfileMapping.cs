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
            CreateMap<OrderModel, Order>();
            CreateMap<Order, OrderGridModel>();
            CreateMap<OrderGridModel, Order>();

            // Address mapping
            CreateMap<Address, AddressModel>();
            CreateMap<AddressModel, AddressModel>();

            // Payment mapping
            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel, Payment>();
        }
    }
}