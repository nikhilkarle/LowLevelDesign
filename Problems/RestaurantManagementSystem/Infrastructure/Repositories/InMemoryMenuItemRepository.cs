using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryMenuItemRepository : IMenuItemRepository
{
    private readonly ConcurrentDictionary<Guid, MenuItem> _store = new();

    public void Add(MenuItem item) => _store[item.Id] = item;

    public MenuItem? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<MenuItem> GetAll() => _store.Values.ToList();

    public IReadOnlyList<MenuItem> GetByCategory(MenuCategory category)
        => _store.Values.Where(i => i.Category == category).ToList();

    public IReadOnlyList<MenuItem> GetAvailable()
        => _store.Values.Where(i => i.IsAvailable).ToList();
}
