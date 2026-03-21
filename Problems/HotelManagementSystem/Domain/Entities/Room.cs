using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Domain.States.Rooms;

namespace HotelManagementSystem.Domain.Entities;

public sealed class Room
{
    public Guid Id { get; }
    public string RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal BasePricePerNight { get; }
    public RoomStatus Status { get; private set; }
    public int Version { get; private set; }

    private IRoomState _state;

    public Room(Guid id, string roomNumber, RoomType roomType, decimal basePricePerNight)
    {
        Id = id;
        RoomNumber = roomNumber;
        RoomType = roomType;
        BasePricePerNight = basePricePerNight;
        Status = RoomStatus.Available;
        _state = new AvailableRoomState();
        Version = 0;
    }

    public void ChangeState(IRoomState state)
    {
        _state = state;
        Status = state.Status;
        Version++;
    }

    public void Reserve() => _state.Reserve(this);
    public void Occupy() => _state.Occupy(this);
    public void MarkAvailable() => _state.MarkAvailable(this);
    public void MarkOutOfService() => _state.MarkOutOfService(this);
}