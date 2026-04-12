using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.Payments;

public class PayPalProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request)
    {
        Console.WriteLine($"  [PayPal] Charging ${request.Amount:F2} for booking {request.BookingId}");
        return new PaymentResult { Success = true, TransactionId = $"PP-{Guid.NewGuid():N}"[..12] };
    }
}
