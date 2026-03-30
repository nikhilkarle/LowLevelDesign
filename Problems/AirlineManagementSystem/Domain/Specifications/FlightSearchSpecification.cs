using System;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.Specifications;

public class FlightSearchSpecification : ISpecification<Flight>
{
    private readonly string _source;
    private readonly string _destination;
    private readonly DateOnly _date;

    public FlightSearchSpecification(string source, string destination, DateOnly date)
    {
        _source = source;
        _destination = destination;
        _date = date;
    }

    public bool IsSatisfiedBy(Flight item)
    {
        return item.Source.Equals(_source, StringComparison.OrdinalIgnoreCase)
               && item.Destination.Equals(_destination, StringComparison.OrdinalIgnoreCase)
               && DateOnly.FromDateTime(item.Schedule.DepartureTimeUtc) == _date;
    }
}