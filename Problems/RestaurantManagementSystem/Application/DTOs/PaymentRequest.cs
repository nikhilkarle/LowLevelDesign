using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.DTOs;

public class PaymentRequest
{
    public Guid InvoiceId { get; init; }
    public PaymentMethod Method { get; init; }
}
