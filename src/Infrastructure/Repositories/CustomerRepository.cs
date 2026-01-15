using BackendTuya.src.Domain.Customers;
using BackendTuya.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendTuya.src.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _db;

        public CustomerRepository(AppDbContext db) => _db = db;

        public Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct)
            => _db.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id, ct);

        public Task<List<Customer>> GetAllAsync(CancellationToken ct)
            => _db.Customers.AsNoTracking().ToListAsync(ct);

        public Task AddAsync(Customer customer, CancellationToken ct)
            => _db.Customers.AddAsync(customer, ct).AsTask();

        public Task SaveChangesAsync(CancellationToken ct)
            => _db.SaveChangesAsync(ct);
    }
}