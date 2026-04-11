using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryInvoiceRepository : IInvoiceRepository
{
    private readonly ConcurrentDictionary<Guid, Invoice> _store = new();

    public void Add(Invoice invoice) => _store[invoice.Id] = invoice;

    public Invoice? GetById(Guid id) => _store.GetValueOrDefault(id);

    public Invoice? GetByOrderId(Guid orderId)
        => _store.Values.FirstOrDefault(i => i.OrderId == orderId);
}
