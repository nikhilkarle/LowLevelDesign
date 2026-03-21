using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Infrastructure.Interfaces;

public interface IRoomRepository
{
    Room? GetById(Guid roomId);
    IReadOnlyList<Room> GetAll();
    IReadOnlyList<Room> GetByType(RoomType roomType);
    void Add(Room room);
    void Update(Room room);
}