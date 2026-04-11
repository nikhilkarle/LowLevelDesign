using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Payments;

public class MobilePaymentProcessor : IPaymentProcessor
{
    public bool Process(Payment payment)
    {
        Console.WriteLine($"  [Mobile Pay] QR scan confirmed. ${payment.Amount:F2} received.");
        return true;
    }
}
