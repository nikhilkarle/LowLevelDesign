using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(Guid id);
    IReadOnlyList<Order> GetByCustomer(Guid customerId);
    IReadOnlyList<Order> GetByStatus(OrderStatus status);
    IReadOnlyList<Order> GetCompletedBetween(DateTime from, DateTime to);
}
