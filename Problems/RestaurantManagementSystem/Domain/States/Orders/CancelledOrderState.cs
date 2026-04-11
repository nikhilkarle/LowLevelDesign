using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Orders;

public sealed class CancelledOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Cancelled;

    public void StartPreparing(Order order) => throw new InvalidOperationException("Cannot restart a cancelled order.");
    public void MarkReady(Order order)      => throw new InvalidOperationException("Cannot mark a cancelled order as ready.");
    public void MarkServed(Order order)     => throw new InvalidOperationException("Cannot serve a cancelled order.");
    public void Cancel(Order order)         => throw new InvalidOperationException("Order is already cancelled.");
}
