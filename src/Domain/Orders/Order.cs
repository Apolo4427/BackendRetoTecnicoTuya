namespace BackendTuya.src.Domain.Orders
{
    public enum OrderStatus { Created, Cancelled }

    public class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public string Description { get; private set; }
        public decimal Total { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Created;

        private Order() { } // Para EF

        public Order(Guid customerId, string description, decimal total)
        {
            if (customerId == Guid.Empty) throw new ArgumentException("El CustomerId es obligarorio");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("La Description es obligatoria");
            if (total <= 0) throw new ArgumentException("El total debe ser > 0");

            CustomerId = customerId;
            Description = description;
            Total = total;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Cancelled) return;
            Status = OrderStatus.Cancelled;
        }
    }
}