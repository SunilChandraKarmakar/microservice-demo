using AutoMapper;
using MediatR;
using Ordering.Applications.Contracts.Persistence.OrderRepositories;
using Ordering.Applications.Features.OrderLogic.Model;

namespace Ordering.Applications.Features.OrderLogic.Queries
{
    public class GetOrdersByUserNameQuery : IRequest<IEnumerable<OrderModel>>
    {
        public string UserName { get; set; }

        public class Handler : IRequestHandler<GetOrdersByUserNameQuery, IEnumerable<OrderModel>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<OrderModel>> Handle(GetOrdersByUserNameQuery request, 
                CancellationToken cancellationToken)
            {
                var getOrdersByUserName = await _orderRepository.GetOrdersByUserNameAsync(request.UserName);
                var mapResult = _mapper.Map<IEnumerable<OrderModel>>(getOrdersByUserName);

                return mapResult;
            }
        }
    }
}