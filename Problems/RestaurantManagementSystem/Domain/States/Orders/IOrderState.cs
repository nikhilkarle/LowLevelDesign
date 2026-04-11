using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Orders;

public interface IOrderState
{
    OrderStatus Status { get; }
    void StartPreparing(Order order);
    void MarkReady(Order order);
    void MarkServed(Order order);
    void Cancel(Order order);
}
