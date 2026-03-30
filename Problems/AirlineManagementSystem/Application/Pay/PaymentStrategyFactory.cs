using System;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Application.Pay;

public class PaymentStrategyFactory
{
    public IPaymentStrategy Create(PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.CreditCard => new CreditCardPaymentStrategy(),
            PaymentMethod.UPI => new UPIPaymentStrategy(),
            _ => throw new NotSupportedException($"Payment method {paymentMethod} is not supported.")
        };
    }
}