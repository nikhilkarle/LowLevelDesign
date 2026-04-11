using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Payments;

public class CashPaymentProcessor : IPaymentProcessor
{
    public bool Process(Payment payment)
    {
        Console.WriteLine($"  [Cash] Collected ${payment.Amount:F2} in cash.");
        return true;
    }
}
