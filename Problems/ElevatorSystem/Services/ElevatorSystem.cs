using ElevatorSystem.Controllers;
using ElevatorSystem.Enums;
using ElevatorSystem.Models;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Services;

public class ElevatorSystem
{
    private readonly Building _building;
    private readonly IReadOnlyList<ElevatorController> _controllers;
    private readonly ElevatorDispatcher _dispatcher;

    public ElevatorSystem(
        Building building,
        IReadOnlyList<ElevatorController> controllers,
        ElevatorDispatcher dispatcher)
    {
        _building = building ?? throw new ArgumentNullException(nameof(building));
        _controllers = controllers ?? throw new ArgumentNullException(nameof(controllers));
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    public void RequestElevator(int floorNumber, Direction direction)
    {
        ValidateFloor(floorNumber);

        if (direction == Direction.None)
            throw new ArgumentException("Hall request must have Up or Down direction.", nameof(direction));

        var request = new HallRequest(floorNumber, direction);
        _dispatcher.SubmitHallRequest(request);
    }

    public void SelectDestination(int elevatorId, int destinationFloor)
    {
        ValidateFloor(destinationFloor);

        var controller = GetElevatorController(elevatorId);
        var request = new CarRequest(destinationFloor);
        controller.AddCarRequest(request, _building.TotalFloors);
    }

    public void StepAll()
    {
        foreach (var controller in _controllers)
        {
            controller.Step(1, _building.TotalFloors);
        }
    }

    public void ChangeLoad(int elevatorId, int delta)
    {
        var controller = GetElevatorController(elevatorId);
        controller.TryUpdateLoad(delta);
    }

    public void SetMaintenance(int elevatorId, bool enabled)
    {
        var controller = GetElevatorController(elevatorId);
        controller.SetMaintenanceMode(enabled);
    }

    public void PrintStatus()
    {
        Console.WriteLine("---- Elevator Status ----");
        foreach (var controller in _controllers)
        {
            var e = controller.Elevator;
            Console.WriteLine($"Elevator {e.Id}: Floor={e.CurrentFloor}, State={e.State}, Direction={e.Direction}, Load={e.CurrentLoad}/{e.MaxCapacity}");
        }
        Console.WriteLine("-------------------------");
    }

    private ElevatorController GetElevatorController(int elevatorId)
    {
        var controller = _controllers.FirstOrDefault(c => c.Elevator.Id == elevatorId);
        if (controller is null)
            throw new InvalidOperationException($"Elevator with ID {elevatorId} was not found.");

        return controller;
    }

    private void ValidateFloor(int floorNumber)
    {
        if (!_building.IsValidFloor(floorNumber))
            throw new ArgumentOutOfRangeException(nameof(floorNumber), $"Floor {floorNumber} is invalid.");
    }
}