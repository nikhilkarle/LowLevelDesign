using HotelManagementSystem.Application.DTOs;
using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Infrastructure.Payments;

public sealed class CreditCardPaymentProcessor : IPaymentProcessor
{
    public Payment Process(PaymentRequest request)
    {
        var payment = new Payment(Guid.NewGuid(), request.ReservationId, request.Amount, PaymentMethod.CreditCard);

        // simulate gateway success
        payment.MarkPaid();

        return payment;
    }
}