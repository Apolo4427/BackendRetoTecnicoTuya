using MediatR;

namespace BackendTuya.src.Application.Orders.Commands
{
    public record CancelOrderCommand(Guid OrderId) : IRequest<Unit>;
}