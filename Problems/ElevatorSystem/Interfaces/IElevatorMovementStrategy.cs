using ElevatorSystem.Models;

namespace ElevatorSystem.Interfaces;

public interface IElevatorMovementStrategy
{
    int? GetNextStop(Elevator elevator);
}