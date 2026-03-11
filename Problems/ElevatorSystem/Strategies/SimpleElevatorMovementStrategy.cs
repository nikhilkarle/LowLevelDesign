using ElevatorSystem.Enums;
using ElevatorSystem.Interfaces;
using ElevatorSystem.Models;

namespace ElevatorSystem.Strategies;

public class SimpleElevatorMovementStrategy : IElevatorMovementStrategy
{
    public int? GetNextStop(Elevator elevator)
    {
        if (!elevator.HasPendingStops)
            return null;

        if (elevator.Direction == Direction.Up)
        {
            int? nextUp = elevator.UpStops.FirstOrDefault();
            if (nextUp != 0)
                return nextUp;

            int? nextDown = elevator.DownStops.FirstOrDefault();
            if (nextDown != 0)
                return nextDown;
        }
        else if (elevator.Direction == Direction.Down)
        {
            int? nextDown = elevator.DownStops.FirstOrDefault();
            if (nextDown != 0)
                return nextDown;

            int? nextUp = elevator.UpStops.FirstOrDefault();
            if (nextUp != 0)
                return nextUp;
        }
        else
        {
            int? nearestUp = elevator.UpStops.FirstOrDefault();
            int? nearestDown = elevator.DownStops.FirstOrDefault();

            if (nearestUp != 0 && nearestDown != 0)
            {
                int upDistance = Math.Abs(nearestUp.Value - elevator.CurrentFloor);
                int downDistance = Math.Abs(nearestDown.Value - elevator.CurrentFloor);
                return upDistance <= downDistance ? nearestUp : nearestDown;
            }

            if (nearestUp != 0) return nearestUp;
            if (nearestDown != 0) return nearestDown;
        }

        return null;
    }
}