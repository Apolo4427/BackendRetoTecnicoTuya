using MediatR;

namespace BackendTuya.src.Application.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            var details = new OrderDetailsDto(request.Description, request.Total);
            return await _orderService.CreateOrderAsync(request.CustomerId, details, ct);
        }
    }
}