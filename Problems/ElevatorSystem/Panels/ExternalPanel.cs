using ElevatorSystem.Enums;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Models.Panels;

public class ExternalPanel
{
    public int FloorNumber { get; }

    public ExternalPanel(int floorNumber)
    {
        FloorNumber = floorNumber;
    }

    public HallRequest PressUp()
    {
        return new HallRequest(FloorNumber, Direction.Up);
    }

    public HallRequest PressDown()
    {
        return new HallRequest(FloorNumber, Direction.Down);
    }
}