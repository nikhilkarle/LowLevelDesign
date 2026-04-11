using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IPaymentRepository
{
    void Add(Payment payment);
    Payment? GetById(Guid id);
    IReadOnlyList<Payment> GetByInvoiceId(Guid invoiceId);
    IReadOnlyList<Payment> GetCompletedBetween(DateTime from, DateTime to);
}
