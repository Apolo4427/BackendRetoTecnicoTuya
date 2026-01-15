using BackendTuya.src.Application.Common.Exceptions;
using BackendTuya.src.Domain.Customers;
using BackendTuya.src.Domain.Orders;

namespace BackendTuya.src.Application.Orders
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid customerId, OrderDetailsDto details, CancellationToken ct);
        Task CancelOrderAsync(Guid orderId, CancellationToken ct);
    }

    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customers;
        private readonly IOrderRepository _orders;

        public OrderService(ICustomerRepository customers, IOrderRepository orders)
        {
            _customers = customers;
            _orders = orders;
        }

        public async Task<Guid> CreateOrderAsync(Guid customerId, OrderDetailsDto details, CancellationToken ct)
        {
            var customer = await _customers.GetByIdAsync(customerId, ct)
                ?? throw new NotFoundException(nameof(Customer),customerId );

            // Regla de negocio en el dominio:
            var order = customer.CreateOrder(details.Description, details.Total);

            await _orders.AddAsync(order, ct);
            await _orders.SaveChangesAsync(ct);

            return order.Id;
        }

        public async Task CancelOrderAsync(Guid orderId, CancellationToken ct)
        {
            var order = await _orders.GetByIdAsync(orderId, ct)
                ?? throw new NotFoundException(nameof(Order), orderId);

            order.Cancel();
            await _orders.SaveChangesAsync(ct);
        }
    }
}