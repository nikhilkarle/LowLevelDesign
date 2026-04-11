using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Orders;

public sealed class PendingOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Pending;

    public void StartPreparing(Order order) => order.ChangeState(new PreparingOrderState());

    public void MarkReady(Order order)   => throw new InvalidOperationException("Order must be in preparation before it can be marked ready.");
    public void MarkServed(Order order)  => throw new InvalidOperationException("Order must be ready before it can be served.");
    public void Cancel(Order order)      => order.ChangeState(new CancelledOrderState());
}
