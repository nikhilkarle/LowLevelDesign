using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Observers;

public interface IOrderObserver
{
    void OnOrderStatusChanged(Order order);
}
