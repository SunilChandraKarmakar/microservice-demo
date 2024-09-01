using AutoMapper;
using MediatR;
using Ordering.Applications.Contracts.Persistence.OrderRepositories;
using Ordering.Applications.Features.OrderLogic.Model;
using Ordering.Model.Models;

namespace Ordering.Applications.Features.OrderLogic.Commands
{
    public class UpsertOrderCommand : OrderModel, IRequest<bool>
    {
        public class Handler : IRequestHandler<UpsertOrderCommand, bool>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpsertOrderCommand request, CancellationToken cancellationToken)
            {
                Order orderEntity;
                orderEntity = await _orderRepository.GetFirstOrDefaultAsync(o => o.Id == request.Id);

                if(orderEntity is null)
                {
                    var mapOrderCreateModel = _mapper.Map<Order>(request);
                    var createResult = await _orderRepository.AddAsync(mapOrderCreateModel);

                    return createResult; ;
                }

                orderEntity = _mapper.Map((OrderModel)request, orderEntity);
                var updateResult = await _orderRepository.UpdateAsync(orderEntity);

                return updateResult;
            }
        }
    }
}