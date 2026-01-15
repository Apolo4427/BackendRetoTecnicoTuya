using MediatR;

namespace BackendTuya.src.Application.Orders.Commands
{
    public record CreateOrderCommand( // record para buscar inmutabilidad
        Guid CustomerId, 
        string Description, 
        decimal Total) : IRequest<Guid>;
}