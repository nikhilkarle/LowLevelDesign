using System;

namespace AirlineManagementSystem.Domain.Entities;

public class FlightSchedule
{
    public Guid Id { get; }
    public DateTime DepartureTimeUtc { get; private set; }
    public DateTime ArrivalTimeUtc { get; private set; }

    public FlightSchedule(Guid id, DateTime departureTimeUtc, DateTime arrivalTimeUtc)
    {
        if (arrivalTimeUtc <= departureTimeUtc)
            throw new ArgumentException("Arrival time must be greater than departure time.");

        Id = id;
        DepartureTimeUtc = departureTimeUtc;
        ArrivalTimeUtc = arrivalTimeUtc;
    }
}