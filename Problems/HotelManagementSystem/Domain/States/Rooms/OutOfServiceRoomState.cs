using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Rooms;

public sealed class OutOfServiceRoomState : IRoomState
{
    public RoomStatus Status => RoomStatus.OutOfService;

    public void Reserve(Room room) => throw new InvalidOperationException("Out-of-service room cannot be reserved.");

    public void Occupy(Room room) => throw new InvalidOperationException("Out-of-service room cannot be occupied.");

    public void MarkAvailable(Room room) => room.ChangeState(new AvailableRoomState());

    public void MarkOutOfService(Room room) => throw new InvalidOperationException("Room is already out of service.");
}