using ElevatorSystem.Controllers;
using ElevatorSystem.Enums;
using ElevatorSystem.Interfaces;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Strategies;

public class NearestCarDispatchStrategy : IElevatorDispatchStrategy
{
    public ElevatorController SelectBestElevator(
        IReadOnlyList<ElevatorController> elevators,
        HallRequest request)
    {
        ElevatorController? best = null;
        int bestScore = int.MaxValue;

        foreach (var controller in elevators)
        {
            var elevator = controller.Elevator;

            if (elevator.State == ElevatorState.Maintenance)
                continue;

            if (elevator.State == ElevatorState.Overloaded)
                continue;

            int score = CalculateScore(controller, request);

            if (score < bestScore)
            {
                bestScore = score;
                best = controller;
            }
        }

        if (best is null)
            throw new InvalidOperationException("No elevator is available to serve the request.");

        return best;
    }

    private int CalculateScore(ElevatorController controller, HallRequest request)
    {
        var elevator = controller.Elevator;
        int distance = Math.Abs(elevator.CurrentFloor - request.FloorNumber);

        if (elevator.IsIdle)
            return distance;

        bool sameDirection = elevator.Direction == request.Direction;

        if (sameDirection)
        {
            if (elevator.Direction == Direction.Up && elevator.CurrentFloor <= request.FloorNumber)
                return distance + 1;

            if (elevator.Direction == Direction.Down && elevator.CurrentFloor >= request.FloorNumber)
                return distance + 1;
        }

        return distance + 100;
    }
}