namespace ElevatorSystem.Requests;

public sealed class CarRequest : ElevatorRequest
{
    public CarRequest(int floorNumber) : base(floorNumber)
    {
    }

    public override string ToString()
    {
        return $"CarRequest(Floor={FloorNumber})";
    }
}