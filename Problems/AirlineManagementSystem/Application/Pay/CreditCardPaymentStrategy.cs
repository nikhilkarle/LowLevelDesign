using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Pay;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public void Pay(Payment payment)
    {
        payment.MarkSuccess();
    }
}