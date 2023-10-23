using IMS.Application.Features.Products.Commands.CreateProduct;
using IMS.Application.Features.Products.Commands.UpdateInventoryCount;
using IMS.Application.Features.Products.Queries.GetProductDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDetailVm>> GetProductById(int id)
        {
            var response = await _mediator.Send(new GetProductDetailQuery() { Id = id });
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductCommand createCommand)
        {
            var id = await _mediator.Send(createCommand);
            return CreatedAtRoute("GetProduct", new { id }, createCommand);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateInventoryCountCommand updateCommand)
        {
            await _mediator.Send(updateCommand);
            return NoContent();
        }
    }
}
