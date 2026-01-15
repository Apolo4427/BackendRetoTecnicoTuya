using BackendTuya.src.Domain.Orders;

namespace BackendTuya.src.Domain.Customers
{
    public class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid(); // podriamos configurar en sql server para guid secuenciales y evitar fragmentacion
        public string Name { get; private set; }
        public string Email { get; private set; }

        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private Customer() { } // Para EF
        public Customer(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("El email es obligatorio");

            Name = name;
            Email = email;
        }

        public Order CreateOrder(string description, decimal total)
        {
            if (total <= 0) throw new ArgumentException("El total debe ser > 0");
            var order = new Order(Id, description, total);
            _orders.Add(order);
            return order;
        }
    }
}