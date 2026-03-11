using ElevatorSystem.Controllers;
using ElevatorSystem.Models;
using ElevatorSystem.Observers;
using ElevatorSystem.Services;
using ElevatorSystem.Strategies;
using ElevatorSystem.Enums;

namespace ElevatorSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var building = new Building(totalFloors: 10);

        var movementStrategy = new SimpleElevatorMovementStrategy();
        var dispatchStrategy = new NearestCarDispatchStrategy();

        var monitor = new ElevatorMonitor();

        var controller1 = new ElevatorController(
            new Elevator(id: 1, startingFloor: 1, maxCapacity: 8),
            movementStrategy);

        var controller2 = new ElevatorController(
            new Elevator(id: 2, startingFloor: 5, maxCapacity: 8),
            movementStrategy);

        controller1.Subscribe(monitor);
        controller2.Subscribe(monitor);

        var controllers = new List<ElevatorController> { controller1, controller2 };

        var dispatcher = new ElevatorDispatcher(controllers, dispatchStrategy);

        var system = new ElevatorSystem.Services.ElevatorSystem(building, controllers, dispatcher);

        system.PrintStatus();

        system.RequestElevator(3, Direction.Up);
        system.RequestElevator(8, Direction.Down);

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"\nStep {i + 1}");
            system.StepAll();
            system.PrintStatus();
        }

        system.SelectDestination(1, 9);
        system.SelectDestination(2, 2);

        for (int i = 0; i < 8; i++)
        {
            Console.WriteLine($"\nStep {i + 6}");
            system.StepAll();
            system.PrintStatus();
        }

        system.ChangeLoad(1, 9); // overload attempt
        system.ChangeLoad(1, -2);

        system.SetMaintenance(2, true);
        system.PrintStatus();
    }
}