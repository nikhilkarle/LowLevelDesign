using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryTableRepository : ITableRepository
{
    private readonly ConcurrentDictionary<Guid, Table> _store = new();

    public void Add(Table table) => _store[table.Id] = table;

    public Table? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Table> GetAll() => _store.Values.ToList();

    public IReadOnlyList<Table> GetAvailable(int minCapacity)
        => _store.Values
            .Where(t => t.IsAvailable && t.Capacity >= minCapacity)
            .OrderBy(t => t.Capacity)   
            .ToList();
}
