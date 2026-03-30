using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Pay;

public interface IPaymentStrategy
{
    void Pay(Payment payment);
}