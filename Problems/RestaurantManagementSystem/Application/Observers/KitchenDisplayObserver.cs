using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Observers;

public class KitchenDisplayObserver : IOrderObserver
{
    public void OnOrderStatusChanged(Order order)
        => Console.WriteLine($"  [Kitchen Display] Order {order.Id} → {order.Status} | Items: {string.Join(", ", order.Items.Select(i => $"{i.MenuItemName} x{i.Quantity}"))}");
}
