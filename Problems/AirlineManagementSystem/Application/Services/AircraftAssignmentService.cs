using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Services;

public class AircraftAssignmentService
{
    public void AssignAircraft(Flight flight, Aircraft aircraft)
    {
        flight.AssignAircraft(aircraft);
    }
}