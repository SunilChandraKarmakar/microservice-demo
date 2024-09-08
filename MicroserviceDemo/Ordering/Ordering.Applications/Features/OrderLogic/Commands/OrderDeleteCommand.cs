using MediatR;
using Ordering.Applications.Contracts.Persistence.OrderRepositories;
using Ordering.Model.Models;

namespace Ordering.Applications.Features.OrderLogic.Commands
{
    public class OrderDeleteCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<OrderDeleteCommand, bool>
        {
            private readonly IOrderRepository _orderRepository;
            public Handler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

            public async Task<bool> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
            {
                Order orderEntity;
                orderEntity = await _orderRepository.GetFirstOrDefaultAsync(o => o.Id == request.Id);

                if (orderEntity is null)
                    throw new Exception("Selected order cannot found! Please, try again.");

                var result = await _orderRepository.DeleteAsync(orderEntity);
                return result;
            }
        }
    }
}