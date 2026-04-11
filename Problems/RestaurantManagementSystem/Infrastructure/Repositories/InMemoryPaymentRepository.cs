using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly ConcurrentDictionary<Guid, Payment> _store = new();

    public void Add(Payment payment) => _store[payment.Id] = payment;

    public Payment? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Payment> GetByInvoiceId(Guid invoiceId)
        => _store.Values.Where(p => p.InvoiceId == invoiceId).ToList();

    public IReadOnlyList<Payment> GetCompletedBetween(DateTime from, DateTime to)
        => _store.Values
            .Where(p => p.Status == PaymentStatus.Completed
                     && p.ProcessedAt >= from
                     && p.ProcessedAt <= to)
            .ToList();
}
