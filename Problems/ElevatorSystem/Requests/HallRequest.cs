using ElevatorSystem.Enums;

namespace ElevatorSystem.Requests;

public sealed class HallRequest : ElevatorRequest
{
    public Direction Direction { get; }

    public HallRequest(int floorNumber, Direction direction)
        : base(floorNumber)
    {
        Direction = direction;
    }

    public override string ToString()
    {
        return $"HallRequest(Floor={FloorNumber}, Direction={Direction})";
    }
}