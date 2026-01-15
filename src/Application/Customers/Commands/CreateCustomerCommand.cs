using MediatR;

namespace BackendTuya.src.Application.Customers.Commands
{
    public record CreateCustomerCommand (
        string Name,
        string Email
    ):IRequest<Guid>;
}