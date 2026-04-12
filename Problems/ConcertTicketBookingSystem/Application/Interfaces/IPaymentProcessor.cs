using ConcertTicketBookingSystem.Application.DTOs;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IPaymentProcessor
{
    PaymentResult Process(PaymentRequest request);
}
