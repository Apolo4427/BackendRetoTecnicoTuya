using BackendTuya.src.Domain.Customers;
using BackendTuya.src.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace BackendTuya.src.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(cfg =>
            {
                cfg.HasKey(x => x.Id);
                cfg.Property(x => x.Name).IsRequired();
                cfg.Property(x => x.Email).IsRequired();
                cfg.HasMany(x => x.Orders)
                   .WithOne()
                   .HasForeignKey(o => o.CustomerId);
            });

            modelBuilder.Entity<Order>(cfg =>
            {
                cfg.HasKey(x => x.Id);
                cfg.Property(x => x.Description).IsRequired();
                cfg.Property(x => x.Total).HasColumnType("decimal(18,2)");
                cfg.Property(x => x.Status).IsRequired();
            });
        }
    }
}