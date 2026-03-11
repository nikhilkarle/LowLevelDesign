using ElevatorSystem.Enums;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Models;

public class Elevator
{
    private readonly SortedSet<int> _upStops = new();
    private readonly SortedSet<int> _downStops = new(Comparer<int>.Create((a, b) => b.CompareTo(a)));

    public int Id { get; }
    public int CurrentFloor { get; private set; }
    public int MaxCapacity { get; }
    public int CurrentLoad { get; private set; }

    public Direction Direction { get; private set; }
    public ElevatorState State { get; private set; }
    public Door Door { get; }

    public IReadOnlyCollection<int> UpStops => _upStops;
    public IReadOnlyCollection<int> DownStops => _downStops;

    public Elevator(int id, int startingFloor, int maxCapacity)
    {
        if (maxCapacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxCapacity));

        Id = id;
        CurrentFloor = startingFloor;
        MaxCapacity = maxCapacity;
        CurrentLoad = 0;
        Direction = Direction.None;
        State = ElevatorState.Idle;
        Door = new Door();
    }

    public bool IsIdle => State == ElevatorState.Idle;
    public bool IsOverloaded => CurrentLoad > MaxCapacity;
    public bool HasPendingStops => _upStops.Count > 0 || _downStops.Count > 0;

    public void AddStop(int floor)
    {
        if (floor == CurrentFloor)
            return;

        if (floor > CurrentFloor)
            _upStops.Add(floor);
        else
            _downStops.Add(floor);
    }

    public void AddRequest(ElevatorRequest request)
    {
        AddStop(request.FloorNumber);
    }

    public bool ContainsStop(int floor)
    {
        return _upStops.Contains(floor) || _downStops.Contains(floor);
    }

    public void RemoveStop(int floor)
    {
        _upStops.Remove(floor);
        _downStops.Remove(floor);
    }

    public void SetDirection(Direction direction)
    {
        Direction = direction;
    }

    public void SetState(ElevatorState state)
    {
        State = state;
    }

    public void OpenDoor()
    {
        Door.Open();
        State = ElevatorState.DoorOpen;
    }

    public void CloseDoor()
    {
        Door.Close();

        if (HasPendingStops)
            State = ElevatorState.Moving;
        else
            State = ElevatorState.Idle;
    }

    public void MoveUp()
    {
        if (Door.State == DoorState.Open)
            throw new InvalidOperationException("Cannot move while door is open.");

        CurrentFloor++;
        Direction = Direction.Up;
        State = ElevatorState.Moving;
    }

    public void MoveDown()
    {
        if (Door.State == DoorState.Open)
            throw new InvalidOperationException("Cannot move while door is open.");

        CurrentFloor--;
        Direction = Direction.Down;
        State = ElevatorState.Moving;
    }

    public void StayIdle()
    {
        Direction = Direction.None;
        State = ElevatorState.Idle;
    }

    public bool TryChangeLoad(int delta)
    {
        int newLoad = CurrentLoad + delta;
        if (newLoad < 0)
            return false;

        CurrentLoad = newLoad;

        if (CurrentLoad > MaxCapacity)
        {
            State = ElevatorState.Overloaded;
            return false;
        }

        if (State == ElevatorState.Overloaded && CurrentLoad <= MaxCapacity)
        {
            State = HasPendingStops ? ElevatorState.Moving : ElevatorState.Idle;
        }

        return true;
    }
}