using ElevatorSystem.Enums;
using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Controllers;

public class ElevatorController
{
    private readonly object _lock = new();
    private readonly IElevatorMovementStrategy _movementStrategy;
    private readonly List<IElevatorObserver> _observers = new();

    public Elevator Elevator { get; }

    public ElevatorController(Elevator elevator, IElevatorMovementStrategy movementStrategy)
    {
        Elevator = elevator ?? throw new ArgumentNullException(nameof(elevator));
        _movementStrategy = movementStrategy ?? throw new ArgumentNullException(nameof(movementStrategy));
    }

    public void Subscribe(IElevatorObserver observer)
    {
        lock (_lock)
        {
            _observers.Add(observer);
        }
    }

    public void AssignHallRequest(HallRequest request)
    {
        lock (_lock)
        {
            if (Elevator.State == ElevatorState.Maintenance)
                throw new InvalidOperationException($"Elevator {Elevator.Id} is under maintenance.");

            Elevator.AddRequest(request);
            Notify($"Assigned hall request to floor {request.FloorNumber} ({request.Direction}).");

            if (Elevator.State == ElevatorState.Idle)
                Elevator.SetState(ElevatorState.Moving);
        }
    }

    public void AddCarRequest(CarRequest request, int maxFloor)
    {
        lock (_lock)
        {
            if (request.FloorNumber < 1 || request.FloorNumber > maxFloor)
                throw new ArgumentOutOfRangeException(nameof(request), "Destination floor is invalid.");

            if (request.FloorNumber == Elevator.CurrentFloor)
            {
                Notify($"Elevator {Elevator.Id} already at floor {request.FloorNumber}.");
                return;
            }

            Elevator.AddRequest(request);
            Notify($"Added internal destination floor {request.FloorNumber}.");

            if (Elevator.State == ElevatorState.Idle)
                Elevator.SetState(ElevatorState.Moving);
        }
    }

    public void Step(int minFloor, int maxFloor)
    {
        lock (_lock)
        {
            if (Elevator.State == ElevatorState.Maintenance)
            {
                Notify("Skipped step because elevator is in maintenance.");
                return;
            }

            if (Elevator.State == ElevatorState.Overloaded)
            {
                Notify("Skipped step because elevator is overloaded.");
                return;
            }

            if (!Elevator.HasPendingStops)
            {
                Elevator.StayIdle();
                Notify("No pending stops. Elevator is idle.");
                return;
            }

            int? nextStop = _movementStrategy.GetNextStop(Elevator);

            if (nextStop is null)
            {
                Elevator.StayIdle();
                Notify("No next stop found. Elevator is idle.");
                return;
            }

            if (Elevator.CurrentFloor == nextStop.Value)
            {
                StopAtCurrentFloor();
                return;
            }

            if (nextStop.Value > Elevator.CurrentFloor)
            {
                if (Elevator.CurrentFloor + 1 > maxFloor)
                    throw new InvalidOperationException("Elevator cannot move above max floor.");

                Elevator.MoveUp();
                Notify($"Moved up to floor {Elevator.CurrentFloor}.");

                if (Elevator.CurrentFloor == nextStop.Value)
                    StopAtCurrentFloor();
            }
            else
            {
                if (Elevator.CurrentFloor - 1 < minFloor)
                    throw new InvalidOperationException("Elevator cannot move below min floor.");

                Elevator.MoveDown();
                Notify($"Moved down to floor {Elevator.CurrentFloor}.");

                if (Elevator.CurrentFloor == nextStop.Value)
                    StopAtCurrentFloor();
            }
        }
    }

    public void SetMaintenanceMode(bool enabled)
    {
        lock (_lock)
        {
            Elevator.SetState(enabled ? ElevatorState.Maintenance : ElevatorState.Idle);
            Notify(enabled ? "Elevator entered maintenance mode." : "Elevator exited maintenance mode.");
        }
    }

    public bool TryUpdateLoad(int delta)
    {
        lock (_lock)
        {
            bool success = Elevator.TryChangeLoad(delta);

            if (!success)
                Notify($"Load update failed. Current load={Elevator.CurrentLoad}, max={Elevator.MaxCapacity}.");
            else
                Notify($"Load updated. Current load={Elevator.CurrentLoad}.");

            return success;
        }
    }

    private void StopAtCurrentFloor()
    {
        Elevator.RemoveStop(Elevator.CurrentFloor);
        Elevator.OpenDoor();
        Notify($"Stopped at floor {Elevator.CurrentFloor}. Door opened.");

        Elevator.CloseDoor();
        Notify($"Door closed at floor {Elevator.CurrentFloor}.");

        if (!Elevator.HasPendingStops)
            Elevator.StayIdle();
    }

    private void Notify(string message)
    {
        foreach (var observer in _observers)
        {
            observer.OnElevatorUpdated(Elevator, message);
        }
    }
}