using RestaurantManagementSystem.Application.Interfaces;

namespace RestaurantManagementSystem.Application.Strategies;

public class InventoryReportStrategy : IReportStrategy
{
    public ReportResult Generate(
        IOrderRepository orderRepo,
        IInventoryRepository inventoryRepo,
        IPaymentRepository paymentRepo,
        DateTime from,
        DateTime to)
    {
        var all      = inventoryRepo.GetAll();
        var lowStock = inventoryRepo.GetLowStockItems();

        return new ReportResult
        {
            Title = "Inventory Analysis",
            Data = new Dictionary<string, object>
            {
                ["TotalItems"]      = all.Count,
                ["LowStockItems"]   = lowStock.Select(i => $"{i.Name}: {i.Quantity} {i.Unit} (threshold: {i.ReorderThreshold})").ToList(),
                ["LowStockCount"]   = lowStock.Count
            }
        };
    }
}
