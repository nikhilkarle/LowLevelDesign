using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Rooms;

public sealed class OccupiedRoomState : IRoomState
{
    public RoomStatus Status => RoomStatus.Occupied;

    public void Reserve(Room room) => throw new InvalidOperationException("Occupied room cannot be reserved.");

    public void Occupy(Room room) => throw new InvalidOperationException("Room is already occupied.");

    public void MarkAvailable(Room room) => room.ChangeState(new AvailableRoomState());

    public void MarkOutOfService(Room room) => throw new InvalidOperationException("Occupied room cannot be taken out of service directly.");
}