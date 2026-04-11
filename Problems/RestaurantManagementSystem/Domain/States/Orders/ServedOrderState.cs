using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Orders;

public sealed class ServedOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Served;

    public void StartPreparing(Order order) => throw new InvalidOperationException("Order has already been served.");
    public void MarkReady(Order order)      => throw new InvalidOperationException("Order has already been served.");
    public void MarkServed(Order order)     => throw new InvalidOperationException("Order has already been served.");
    public void Cancel(Order order)         => throw new InvalidOperationException("Cannot cancel an order that has already been served.");
}
