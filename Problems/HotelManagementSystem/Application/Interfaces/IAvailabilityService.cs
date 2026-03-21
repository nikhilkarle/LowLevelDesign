using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.Interfaces;

public interface IAvailabilityService
{
    IReadOnlyList<Room> GetAvailableRooms(RoomType roomType, DateTime checkInDate, DateTime checkOutDate);
    bool IsRoomAvailable(Guid roomId, DateTime checkInDate, DateTime checkOutDate);
}