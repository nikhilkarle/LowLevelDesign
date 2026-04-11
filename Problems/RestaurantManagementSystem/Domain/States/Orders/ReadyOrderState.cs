using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Orders;

public sealed class ReadyOrderState : IOrderState
{
    public OrderStatus Status => OrderStatus.Ready;

    public void StartPreparing(Order order) => throw new InvalidOperationException("Order is already ready.");
    public void MarkReady(Order order)      => throw new InvalidOperationException("Order is already ready.");
    public void MarkServed(Order order)     => order.ChangeState(new ServedOrderState());
    public void Cancel(Order order)         => order.ChangeState(new CancelledOrderState());
}
