using AutoMapper;
using MediatR;
using Ordering.Applications.Contracts.Infrastructure.EmailServices;
using Ordering.Applications.Contracts.Persistence.OrderRepositories;
using Ordering.Applications.Features.OrderLogic.Model;
using Ordering.Applications.Models;
using Ordering.Model.Models;

namespace Ordering.Applications.Features.OrderLogic.Commands
{
    public class UpsertOrderCommand : OrderModel, IRequest<bool>
    {
        public class Handler : IRequestHandler<UpsertOrderCommand, bool>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IEmailService _emailService;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
                _emailService = emailService;
            }

            public async Task<bool> Handle(UpsertOrderCommand request, CancellationToken cancellationToken)
            {
                Order orderEntity;
                orderEntity = await _orderRepository.GetFirstOrDefaultAsync(o => o.Id == request.Id);

                if(orderEntity is null)
                {
                    var mapOrderCreateModel = _mapper.Map<Order>(request);
                    var createResult = await _orderRepository.AddAsync(mapOrderCreateModel);
                    var isSendOrderEmail = SendCreateOrderEmail(request.UserName, mapOrderCreateModel.Id.ToString());

                    return createResult; ;
                }

                orderEntity = _mapper.Map((OrderModel)request, orderEntity);
                var updateResult = await _orderRepository.UpdateAsync(orderEntity);

                return updateResult;
            }

            public async Task<bool> SendCreateOrderEmail(string to, string orderId)
            {
                var sendEmailModel = new Email
                {
                    To = to,
                    Subject = "Create Order Notification",
                    Body = $"You order is created. Order id is {orderId}" 
                };

                var isSendOrderEmail = await _emailService.SendEmailAsync(sendEmailModel);
                return isSendOrderEmail;
            }
        }
    }
}