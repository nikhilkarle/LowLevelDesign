using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryStaffRepository : IStaffRepository
{
    private readonly ConcurrentDictionary<Guid, Staff> _store = new();

    public void Add(Staff staff) => _store[staff.Id] = staff;

    public Staff? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Staff> GetAll() => _store.Values.ToList();

    public IReadOnlyList<Staff> GetByRole(StaffRole role)
        => _store.Values.Where(s => s.Role == role).ToList();
}
