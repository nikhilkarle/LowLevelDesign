using System;
using System.Linq;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Application.Pay;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly PaymentProcessor _paymentProcessor;

    public PaymentService(IPaymentRepository paymentRepository, PaymentProcessor paymentProcessor)
    {
        _paymentRepository = paymentRepository;
        _paymentProcessor = paymentProcessor;
    }

    public Payment ProcessPayment(Guid bookingId, double amount, PaymentMethod paymentMethod)
    {
        var payment = new Payment(Guid.NewGuid(), bookingId, amount, paymentMethod);
        _paymentProcessor.Process(payment);
        _paymentRepository.Add(payment);
        return payment;
    }

    public Payment RefundPayment(Guid bookingId)
    {
        var payment = _paymentRepository.GetAll()
            .LastOrDefault(p => p.BookingId == bookingId && p.Status == PaymentStatus.Success)
            ?? throw new InvalidOperationException("Successful payment not found for booking.");

        payment.MarkRefunded();
        _paymentRepository.Update(payment);
        return payment;
    }
}