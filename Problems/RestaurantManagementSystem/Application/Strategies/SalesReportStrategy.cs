using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Strategies;

public class SalesReportStrategy : IReportStrategy
{
    public ReportResult Generate(
        IOrderRepository orderRepo,
        IInventoryRepository inventoryRepo,
        IPaymentRepository paymentRepo,
        DateTime from,
        DateTime to)
    {
        var completedOrders = orderRepo.GetCompletedBetween(from, to);
        var payments        = paymentRepo.GetCompletedBetween(from, to);

        var totalRevenue    = payments.Sum(p => p.Amount);
        var totalOrders     = completedOrders.Count;
        var avgOrderValue   = totalOrders > 0 ? totalRevenue / totalOrders : 0m;

        var itemSales = completedOrders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.MenuItemName)
            .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity));

        return new ReportResult
        {
            Title = "Sales Report",
            Data = new Dictionary<string, object>
            {
                ["Period"]          = $"{from:yyyy-MM-dd} to {to:yyyy-MM-dd}",
                ["TotalRevenue"]    = totalRevenue,
                ["TotalOrders"]     = totalOrders,
                ["AvgOrderValue"]   = avgOrderValue,
                ["TopSellingItems"] = itemSales.OrderByDescending(kv => kv.Value)
                                               .Take(5)
                                               .ToDictionary(kv => kv.Key, kv => (object)kv.Value)
            }
        };
    }
}
