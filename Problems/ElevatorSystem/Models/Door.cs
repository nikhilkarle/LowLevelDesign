using ElevatorSystem.Enums;

namespace ElevatorSystem.Models;

public class Door
{
    public DoorState State{get; private set;} = DoorState.Closed;

    public void Open()
    {
        State = DoorState.Open;
    }
    public void Close()
    {
        State = DoorState.Closed;
    }
}