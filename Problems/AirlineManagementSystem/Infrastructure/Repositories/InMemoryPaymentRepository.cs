using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Infrastructure.Repositories;

public class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly List<Payment> _payments = new();

    public void Add(Payment payment)
    {
        _payments.Add(payment);
    }

    public Payment? GetById(Guid id)
    {
        return _payments.FirstOrDefault(p => p.Id == id);
    }

    public List<Payment> GetAll()
    {
        return _payments;
    }

    public void Update(Payment payment)
    {
    }
}