namespace BackendTuya.src.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Customer>> GetAllAsync(CancellationToken ct);
        Task AddAsync(Customer customer, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}