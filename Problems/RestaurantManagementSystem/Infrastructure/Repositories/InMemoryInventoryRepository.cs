using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryInventoryRepository : IInventoryRepository
{
    private readonly ConcurrentDictionary<Guid, InventoryItem> _store = new();

    public void Add(InventoryItem item) => _store[item.Id] = item;

    public InventoryItem? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<InventoryItem> GetAll() => _store.Values.ToList();

    public IReadOnlyList<InventoryItem> GetLowStockItems()
        => _store.Values.Where(i => i.NeedsReorder).ToList();
}
