using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Application.Pay;

public class PaymentProcessor
{
    private readonly PaymentStrategyFactory _factory;

    public PaymentProcessor(PaymentStrategyFactory factory)
    {
        _factory = factory;
    }

    public void Process(Payment payment)
    {
        var strategy = _factory.Create(payment.PaymentMethod);
        strategy.Pay(payment);
    }
}