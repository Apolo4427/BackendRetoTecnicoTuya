using BackendTuya.src.Application.Orders;
using BackendTuya.src.Domain.Customers;
using BackendTuya.src.Domain.Orders;
using Moq;
using Xunit;

namespace BackendTuya.Application.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_CreatesOrder_WhenCustomerExists()
        {
            var customer = new Customer("Ana", "ana@test.com");

            var customerRepo = new Mock<ICustomerRepository>();
            customerRepo.Setup(r => r.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(customer);

            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask);
            orderRepo.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask);

            var service = new OrderService(customerRepo.Object, orderRepo.Object);

            var orderId = await service.CreateOrderAsync(
                customer.Id,
                new OrderDetailsDto("Laptop", 1500m),
                CancellationToken.None);

            Assert.NotEqual(Guid.Empty, orderId);
            orderRepo.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
            orderRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}