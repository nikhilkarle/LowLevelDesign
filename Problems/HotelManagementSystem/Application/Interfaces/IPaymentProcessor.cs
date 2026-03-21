using HotelManagementSystem.Application.DTOs;
using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Application.Interfaces;

public interface IPaymentProcessor
{
    Payment Process(PaymentRequest request);
}