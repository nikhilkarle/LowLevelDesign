using System;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IPaymentService
{
    Payment ProcessPayment(Guid bookingId, double amount, PaymentMethod paymentMethod);
    Payment RefundPayment(Guid bookingId);
}