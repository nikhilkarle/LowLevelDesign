using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Observers;

public class CustomerNotificationObserver : IOrderObserver
{
    public void OnOrderStatusChanged(Order order)
    {
        var message = order.Status switch
        {
            OrderStatus.Preparing => "Your order is being prepared.",
            OrderStatus.Ready     => "Your order is ready and will be served shortly!",
            OrderStatus.Served    => "Enjoy your meal! Your bill is available on request.",
            OrderStatus.Cancelled => "Your order has been cancelled.",
            _ => null
        };

        if (message is not null)
            Console.WriteLine($"  [Customer Notification] → {message}");
    }
}
