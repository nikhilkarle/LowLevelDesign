using ElevatorSystem.Models;

namespace ElevatorSystem.Interfaces;

public interface IElevatorObserver
{
    void OnElevatorUpdated(Elevator elevator, string message);
}