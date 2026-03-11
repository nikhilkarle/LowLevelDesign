using ElevatorSystem.Enums;

namespace ElevatorSystem.Requests;

public abstract class ElevatorRequest
{
    public int FloorNumber { get; }
    public DateTime RequestedAtUtc { get; }
    public RequestStatus Status { get; set; }

    protected ElevatorRequest(int floorNumber)
    {
        FloorNumber = floorNumber;
        RequestedAtUtc = DateTime.UtcNow;
        Status = RequestStatus.Pending;
    }
}