using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Application.Services;

public sealed class ReportService : IReportService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IPaymentRepository _paymentRepository;

    public ReportService(
        IRoomRepository roomRepository,
        IReservationRepository reservationRepository,
        IPaymentRepository paymentRepository)
    {
        _roomRepository = roomRepository;
        _reservationRepository = reservationRepository;
        _paymentRepository = paymentRepository;
    }

    public decimal GetOccupancyRate(DateTime date)
    {
        var totalRooms = _roomRepository.GetAll().Count;
        if (totalRooms == 0) return 0;

        var occupied = _reservationRepository.GetAll()
            .Count(r => r.Status == ReservationStatus.CheckedIn &&
                        r.CheckInDate.Date <= date.Date &&
                        r.CheckOutDate.Date > date.Date);

        return (decimal)occupied / totalRooms * 100;
    }

    public decimal GetRevenue(DateTime from, DateTime to)
    {
        var reservationIds = _reservationRepository.GetAll()
            .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
            .Select(r => r.Id)
            .ToHashSet();

        decimal revenue = 0;

        foreach (var reservationId in reservationIds)
        {
            var payments = _paymentRepository.GetByReservationId(reservationId);
            revenue += payments
                .Where(p => p.Status == PaymentStatus.Paid)
                .Sum(p => p.Amount);
        }

        return revenue;
    }
}