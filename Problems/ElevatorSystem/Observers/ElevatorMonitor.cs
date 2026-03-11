using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.Observers;

public class ElevatorMonitor : IElevatorObserver
{
    public void OnElevatorUpdated(Elevator elevator, string message)
    {
        Console.WriteLine($"[Elevator {elevator.Id}] Floor={elevator.CurrentFloor}, State={elevator.State}, Direction={elevator.Direction} -> {message}");
    }
}