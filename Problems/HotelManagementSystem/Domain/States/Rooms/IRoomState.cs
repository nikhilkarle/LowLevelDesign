using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Rooms;

public interface IRoomState
{
    RoomStatus Status { get; }

    void Reserve(Room room);
    void Occupy(Room room);
    void MarkAvailable(Room room);
    void MarkOutOfService(Room room);
}