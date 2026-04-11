using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Factories;

public class OrderFactory : IOrderFactory
{
    public Order Create(Guid customerId, Guid tableId)
        => new Order(Guid.NewGuid(), customerId, tableId);
}
