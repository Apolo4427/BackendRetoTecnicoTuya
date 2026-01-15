namespace BackendTuya.src.Application.Orders
{
    // podriamos crear diferentes sipos de orders en el futuro
    public record OrderDetailsDto(string Description, decimal Total);
}