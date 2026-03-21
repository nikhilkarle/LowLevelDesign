using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryRoomRepository : IRoomRepository
{
    private readonly Dictionary<Guid, Room> _rooms = new();

    public Room? GetById(Guid roomId) => _rooms.GetValueOrDefault(roomId);

    public IReadOnlyList<Room> GetAll() => _rooms.Values.ToList();

    public IReadOnlyList<Room> GetByType(RoomType roomType) =>
        _rooms.Values.Where(x => x.RoomType == roomType).ToList();

    public void Add(Room room) => _rooms[room.Id] = room;

    public void Update(Room room) => _rooms[room.Id] = room;
}