using IMS.Application.Features.Orders.Commands.CreateOrder;
using IMS.Application.Features.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOrderCommand createCommand)
        {
            var id = await _mediator.Send(createCommand);
            return Ok(new { id });
        }
    }
}
