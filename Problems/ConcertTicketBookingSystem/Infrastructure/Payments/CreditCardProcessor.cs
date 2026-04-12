using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.Payments;

public class CreditCardProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request)
    {
        Console.WriteLine($"  [CreditCard] Charging ${request.Amount:F2} for booking {request.BookingId}");
        return new PaymentResult { Success = true, TransactionId = $"CC-{Guid.NewGuid():N}"[..12] };
    }
}
