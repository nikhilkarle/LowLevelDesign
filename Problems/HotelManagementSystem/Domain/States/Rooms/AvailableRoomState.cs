using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Rooms;

public sealed class AvailableRoomState : IRoomState
{
    public RoomStatus Status => RoomStatus.Available;

    public void Reserve(Room room) => room.ChangeState(new ReservedRoomState());

    public void Occupy(Room room) => throw new InvalidOperationException("Cannot occupy directly from available without reservation/check-in.");

    public void MarkAvailable(Room room) => throw new InvalidOperationException("Room is already available.");

    public void MarkOutOfService(Room room) => room.ChangeState(new OutOfServiceRoomState());
}