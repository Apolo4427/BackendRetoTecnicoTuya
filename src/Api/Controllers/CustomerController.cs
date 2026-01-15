using BackendTuya.src.Application.Customers.Commands;
using BackendTuya.src.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTuya.src.Api.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<CustomerListItemDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCustomersQuery(), ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCustomerRequest createCustomerRequest, CancellationToken ct)
        {
            var id = await _mediator.Send(new CreateCustomerCommand(createCustomerRequest.Name, createCustomerRequest.Email), ct);
            return Ok(id);
        }

        public record CreateCustomerRequest(string Name, string Email); 
    }
}