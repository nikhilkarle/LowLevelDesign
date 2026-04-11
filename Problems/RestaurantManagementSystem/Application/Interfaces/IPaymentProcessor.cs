using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IPaymentProcessor
{
    bool Process(Payment payment);
}
