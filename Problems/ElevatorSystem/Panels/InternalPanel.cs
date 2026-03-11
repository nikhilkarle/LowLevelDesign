using ElevatorSystem.Requests;

namespace ElevatorSystem.Models.Panels;

public class InternalPanel
{
    public CarRequest SelectFloor(int floorNumber)
    {
        return new CarRequest(floorNumber);
    }
}