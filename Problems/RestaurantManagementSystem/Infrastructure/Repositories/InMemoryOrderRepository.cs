using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _store = new();

    public void Add(Order order) => _store[order.Id] = order;

    public Order? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Order> GetByCustomer(Guid customerId)
        => _store.Values.Where(o => o.CustomerId == customerId).ToList();

    public IReadOnlyList<Order> GetByStatus(OrderStatus status)
        => _store.Values.Where(o => o.Status == status).ToList();

    public IReadOnlyList<Order> GetCompletedBetween(DateTime from, DateTime to)
        => _store.Values
            .Where(o => o.Status == OrderStatus.Served
                     && o.CompletedAt.HasValue
                     && o.CompletedAt >= from
                     && o.CompletedAt <= to)
            .ToList();
}
