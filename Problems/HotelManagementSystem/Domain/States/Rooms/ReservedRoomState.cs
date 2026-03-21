using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Rooms;

public sealed class ReservedRoomState : IRoomState
{
    public RoomStatus Status => RoomStatus.Reserved;

    public void Reserve(Room room) => throw new InvalidOperationException("Room is already reserved.");

    public void Occupy(Room room) => room.ChangeState(new OccupiedRoomState());

    public void MarkAvailable(Room room) => room.ChangeState(new AvailableRoomState());

    public void MarkOutOfService(Room room) => room.ChangeState(new OutOfServiceRoomState());
}