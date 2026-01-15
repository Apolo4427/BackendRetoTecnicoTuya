using MediatR;

namespace BackendTuya.src.Application.Orders.Commands
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
    {
        private readonly IOrderService _orderService;

        public CancelOrderCommandHandler(IOrderService orderService) => _orderService = orderService;

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken ct)
        {
            await _orderService.CancelOrderAsync(request.OrderId, ct);
            return Unit.Value;
        }
    }
}