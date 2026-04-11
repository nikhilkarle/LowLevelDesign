using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Factories;

public interface IOrderFactory
{
    Order Create(Guid customerId, Guid tableId);
}
