using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Orders;

public sealed class PreparingOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Preparing;

    public void StartPreparing(Order order) => throw new InvalidOperationException("Order is already being prepared.");
    public void MarkReady(Order order)      => order.ChangeState(new ReadyOrderState());
    public void MarkServed(Order order)     => throw new InvalidOperationException("Order must be marked ready before serving.");
    public void Cancel(Order order)         => order.ChangeState(new CancelledOrderState());
}
