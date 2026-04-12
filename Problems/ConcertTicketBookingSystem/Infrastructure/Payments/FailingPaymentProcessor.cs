using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.Payments;

public class FailingPaymentProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request)
    {
        Console.WriteLine($"  [Failing] Simulating payment decline for booking {request.BookingId}");
        return new PaymentResult { Success = false, ErrorMessage = "Card declined by issuing bank." };
    }
}
