using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Customer? GetById(Guid id);
    Customer? GetByEmail(string email);
    IReadOnlyList<Customer> GetAll();
}
