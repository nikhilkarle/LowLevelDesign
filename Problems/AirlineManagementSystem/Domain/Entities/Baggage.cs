using System;

namespace AirlineManagementSystem.Domain.Entities;

public class Baggage
{
    public Guid Id { get; }
    public double WeightInKg { get; private set; }
    public string Description { get; private set; }

    public Baggage(Guid id, double weightInKg, string description)
    {
        Id = id;
        WeightInKg = weightInKg;
        Description = description;
    }
}