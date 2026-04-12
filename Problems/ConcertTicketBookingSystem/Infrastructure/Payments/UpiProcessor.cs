using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.Payments;

public class UpiProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request)
    {
        Console.WriteLine($"  [UPI] Charging ${request.Amount:F2} for booking {request.BookingId}");
        return new PaymentResult { Success = true, TransactionId = $"UPI-{Guid.NewGuid():N}"[..11] };
    }
}
