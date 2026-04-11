using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
        => _customerRepository = customerRepository;

    public Customer Register(string name, string email, string phone)
    {
        if (_customerRepository.GetByEmail(email) is not null)
            throw new InvalidOperationException($"A customer with email '{email}' already exists.");

        var customer = new Customer(Guid.NewGuid(), name, email, phone);
        _customerRepository.Add(customer);
        return customer;
    }

    public void UpdateProfile(Guid customerId, string name, string email, string phone)
    {
        var customer = GetOrThrow(customerId);
        customer.UpdateProfile(name, email, phone);
    }

    public Customer GetOrThrow(Guid id)
        => _customerRepository.GetById(id)
           ?? throw new InvalidOperationException($"Customer {id} not found.");
}
