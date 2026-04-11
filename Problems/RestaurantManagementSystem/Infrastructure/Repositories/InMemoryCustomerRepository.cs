using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly ConcurrentDictionary<Guid, Customer> _store = new();

    public void Add(Customer customer) => _store[customer.Id] = customer;

    public Customer? GetById(Guid id) => _store.GetValueOrDefault(id);

    public Customer? GetByEmail(string email)
        => _store.Values.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Customer> GetAll() => _store.Values.ToList();
}
