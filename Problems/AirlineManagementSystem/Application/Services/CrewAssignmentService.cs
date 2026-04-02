using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Services;

public class CrewAssignmentService
{
    public void AssignCrew(Flight flight, List<CrewMember> crewMembers)
    {
        if (!crewMembers.Any(c => c.Role == Domain.Enums.CrewRole.Pilot))
            throw new InvalidOperationException("At least one pilot is required.");

        if (!crewMembers.Any(c => c.Role == Domain.Enums.CrewRole.CoPilot))
            throw new InvalidOperationException("At least one co-pilot is required.");

        flight.AssignCrew(crewMembers);
    }
}