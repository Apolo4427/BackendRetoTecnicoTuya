using MediatR;

namespace BackendTuya.src.Application.Customers.Queries
{
    public record CustomerListItemDto(Guid Id, string Name, string Email);

    public record GetCustomersQuery() : IRequest<List<CustomerListItemDto>>;
}