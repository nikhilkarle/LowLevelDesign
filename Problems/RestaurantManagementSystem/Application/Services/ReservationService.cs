using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class ReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITableRepository _tableRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        ICustomerRepository customerRepository,
        ITableRepository tableRepository)
    {
        _reservationRepository = reservationRepository;
        _customerRepository    = customerRepository;
        _tableRepository       = tableRepository;
    }

    public Reservation MakeReservation(MakeReservationRequest request)
    {
        _ = _customerRepository.GetById(request.CustomerId)
            ?? throw new InvalidOperationException($"Customer {request.CustomerId} not found.");

        var reservation = new Reservation(
            Guid.NewGuid(), request.CustomerId,
            request.PartySize, request.Date, request.TimeSlot);

        _reservationRepository.Add(reservation);
        Console.WriteLine($"  [Reservation] Pending reservation {reservation.Id} for party of {request.PartySize}.");
        return reservation;
    }

    public Reservation ConfirmReservation(Guid reservationId)
    {
        var reservation = GetOrThrow(reservationId);

        var table = _tableRepository.GetAvailable(reservation.PartySize).FirstOrDefault()
            ?? throw new InvalidOperationException("No available table fits the party size.");

        table.Reserve();
        reservation.Confirm(table.Id);

        Console.WriteLine($"  [Reservation] Confirmed reservation {reservation.Id} → Table #{table.Number}.");
        return reservation;
    }

    public void CompleteReservation(Guid reservationId)
    {
        var reservation = GetOrThrow(reservationId);
        reservation.Complete();

        if (reservation.TableId.HasValue)
        {
            var table = _tableRepository.GetById(reservation.TableId.Value);
            table?.Release();
        }
    }

    public void CancelReservation(Guid reservationId)
    {
        var reservation = GetOrThrow(reservationId);
        reservation.Cancel();

        if (reservation.TableId.HasValue)
        {
            var table = _tableRepository.GetById(reservation.TableId.Value);
            table?.Release();
        }
    }

    public IReadOnlyList<Reservation> GetReservationsForDate(DateTime date)
        => _reservationRepository.GetByDate(date);

    private Reservation GetOrThrow(Guid id)
        => _reservationRepository.GetById(id)
           ?? throw new InvalidOperationException($"Reservation {id} not found.");
}
