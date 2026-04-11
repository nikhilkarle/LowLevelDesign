using RestaurantManagementSystem.Application.Interfaces;

namespace RestaurantManagementSystem.Application.Strategies;

public class ReportResult
{
    public string Title { get; init; } = "";
    public Dictionary<string, object> Data { get; init; } = new();
    public DateTime GeneratedAt { get; init; } = DateTime.UtcNow;
}

public interface IReportStrategy
{
    ReportResult Generate(
        IOrderRepository orderRepo,
        IInventoryRepository inventoryRepo,
        IPaymentRepository paymentRepo,
        DateTime from,
        DateTime to);
}
