using System;
using System.Collections.Generic;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IPaymentRepository
{
    void Add(Payment payment);
    Payment? GetById(Guid id);
    List<Payment> GetAll();
    void Update(Payment payment);
}