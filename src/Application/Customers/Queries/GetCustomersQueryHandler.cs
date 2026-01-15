using BackendTuya.src.Domain.Customers;
using MediatR;

namespace BackendTuya.src.Application.Customers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerListItemDto>>
    {
        private readonly ICustomerRepository _customers;

        public GetCustomersQueryHandler(ICustomerRepository customers) => _customers = customers;

        public async Task<List<CustomerListItemDto>> Handle(GetCustomersQuery request, CancellationToken ct)
        {
            var all = await _customers.GetAllAsync(ct);
            return all.Select(c => new CustomerListItemDto(c.Id, c.Name, c.Email)).ToList();
        }
    }

}