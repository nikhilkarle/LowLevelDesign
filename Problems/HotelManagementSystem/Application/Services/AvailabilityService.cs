using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Application.Services;

public sealed class AvailabilityService : IAvailabilityService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IReservationRepository _reservationRepository;

    public AvailabilityService(IRoomRepository roomRepository, IReservationRepository reservationRepository)
    {
        _roomRepository = roomRepository;
        _reservationRepository = reservationRepository;
    }

    public IReadOnlyList<Room> GetAvailableRooms(RoomType roomType, DateTime checkInDate, DateTime checkOutDate)
    {
        var candidateRooms = _roomRepository.GetByType(roomType)
            .Where(x => x.Status != RoomStatus.OutOfService)
            .ToList();

        return candidateRooms
            .Where(room => IsRoomAvailable(room.Id, checkInDate, checkOutDate))
            .ToList();
    }

    public bool IsRoomAvailable(Guid roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        var activeReservations = _reservationRepository.GetActiveReservationsForRoom(roomId);

        foreach (var reservation in activeReservations)
        {
            var overlaps = checkInDate < reservation.CheckOutDate &&
                           checkOutDate > reservation.CheckInDate;

            if (overlaps)
                return false;
        }

        return true;
    }
}