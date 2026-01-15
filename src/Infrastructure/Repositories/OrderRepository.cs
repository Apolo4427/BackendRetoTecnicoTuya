using BackendTuya.src.Domain.Orders;
using BackendTuya.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendTuya.src.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public Task<Order?> GetByIdAsync(Guid id, CancellationToken ct)
            => _db.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);

        public Task<List<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct)
            => _db.Orders.Where(o => o.CustomerId == customerId).AsNoTracking().ToListAsync(ct);

        public Task AddAsync(Order order, CancellationToken ct)
            => _db.Orders.AddAsync(order, ct).AsTask();

        public Task SaveChangesAsync(CancellationToken ct)
            => _db.SaveChangesAsync(ct);
    }
}