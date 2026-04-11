using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Payments;

public class CreditCardPaymentProcessor : IPaymentProcessor
{
    public bool Process(Payment payment)
    {
        Console.WriteLine($"  [Credit Card] Charged ${payment.Amount:F2} to card ending in ****.");
        return true;
    }
}
