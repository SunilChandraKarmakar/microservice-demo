using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Applications.Features.OrderLogic.Commands;
using Ordering.Applications.Features.OrderLogic.Model;
using Ordering.Applications.Features.OrderLogic.Queries;
using System.Net;

namespace Ordering.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderGridModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderGridModel>>> GetOrdersByUserName(string userName)
        {
            var getOrdersByUserName = await _mediator.Send(new GetOrdersByUserNameQuery { UserName = userName });
            return Ok(getOrdersByUserName);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<bool>> Upsert(UpsertOrderCommand upsertOrderCommand)
        {
            var isUpsertSuccess = await _mediator.Send(upsertOrderCommand);
            return Ok(isUpsertSuccess);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var isDelete = await _mediator.Send(new OrderDeleteCommand { Id = id });
            return Ok(isDelete);
        }
    }
}