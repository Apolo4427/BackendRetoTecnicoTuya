using BackendTuya.src.Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTuya.src.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderRequest body, CancellationToken ct)
        {
            var id = await _mediator.Send(new CreateOrderCommand(body.CustomerId, body.Description, body.Total), ct);
            return Ok(id);
        }

        [HttpPost("{orderId:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid orderId, CancellationToken ct)
        {
            await _mediator.Send(new CancelOrderCommand(orderId), ct);
            return NoContent();
        }
    }

    public record CreateOrderRequest(Guid CustomerId, string Description, decimal Total); // DTO de la capa API para comunicarse con el exterior
}