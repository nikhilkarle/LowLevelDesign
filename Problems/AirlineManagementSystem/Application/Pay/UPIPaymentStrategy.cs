using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Pay;

public class UPIPaymentStrategy : IPaymentStrategy
{
    public void Pay(Payment payment)
    {
        payment.MarkSuccess();
    }
}