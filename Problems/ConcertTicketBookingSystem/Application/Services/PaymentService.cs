using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.Services;

public class PaymentService
{
    private readonly Dictionary<PaymentMethod, IPaymentProcessor> _processors;

    public PaymentService(Dictionary<PaymentMethod, IPaymentProcessor> processors)
    {
        _processors = processors;
    }

    public PaymentResult Process(PaymentRequest request)
    {
        if (!_processors.TryGetValue(request.Method, out var processor))
            throw new InvalidOperationException($"No processor registered for {request.Method}.");

        return processor.Process(request);
    }
}
