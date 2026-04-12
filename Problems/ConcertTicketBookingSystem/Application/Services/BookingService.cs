using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Application.Observers;
using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.Services;

public class BookingService
{
    private readonly IConcertSeatRepository        _seatRepo;
    private readonly IBookingRepository            _bookingRepo;
    private readonly PaymentService                _paymentService;
    private readonly List<IBookingEventHandler>    _handlers = [];

    public BookingService(IConcertSeatRepository seatRepo,
                          IBookingRepository bookingRepo,
                          PaymentService paymentService)
    {
        _seatRepo       = seatRepo;
        _bookingRepo    = bookingRepo;
        _paymentService = paymentService;
    }

    public void Subscribe(IBookingEventHandler handler) => _handlers.Add(handler);

    public Booking Book(BookingRequest request)
    {
        var seats   = LockSeats(request.SeatIds, request.UserId);
        var booking = CreateBookingRecord(request, seats);
        ProcessPayment(booking, request.PaymentMethod);
        return booking;
    }

    private List<ConcertSeat> LockSeats(List<Guid> seatIds, Guid userId)
    {
        var locked = new List<ConcertSeat>();

        foreach (var seatId in seatIds)
        {
            var seat = _seatRepo.GetById(seatId)
                ?? throw new InvalidOperationException($"Seat {seatId} not found.");

            if (!_seatRepo.TryReserve(seatId, userId, seat.Version))
            {
                foreach (var s in locked)
                    s.Release();

                throw new InvalidOperationException(
                    $"Seat {seatId} is no longer available. Please choose different seats.");
            }

            locked.Add(seat);
        }

        return locked;
    }

    private Booking CreateBookingRecord(BookingRequest request, List<ConcertSeat> seats)
    {
        var total   = seats.Sum(s => s.Price);
        var booking = new Booking(
            Guid.NewGuid(), request.UserId, request.ConcertId,
            seats.Select(s => s.Id).ToList(), total);

        _bookingRepo.Save(booking);
        return booking;
    }

    private void ProcessPayment(Booking booking, PaymentMethod method)
    {
        booking.SetStatus(BookingStatus.PaymentProcessing);

        var result = _paymentService.Process(new PaymentRequest
        {
            BookingId = booking.Id,
            Amount    = booking.TotalAmount,
            Method    = method
        });

        if (result.Success)
        {
            foreach (var seatId in booking.ConcertSeatIds)
                _seatRepo.GetById(seatId)!.Book();

            booking.SetStatus(BookingStatus.Confirmed);
            NotifyAll(h => h.OnBookingConfirmed(booking));
        }
        else
        {
            foreach (var seatId in booking.ConcertSeatIds)
                _seatRepo.GetById(seatId)!.Release();

            booking.SetStatus(BookingStatus.Cancelled);
            throw new InvalidOperationException($"Payment failed: {result.ErrorMessage}");
        }
    }

    public void CancelBooking(Guid bookingId)
    {
        var booking = _bookingRepo.GetById(bookingId)
            ?? throw new InvalidOperationException($"Booking {bookingId} not found.");

        if (booking.Status != BookingStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed bookings can be cancelled.");

        foreach (var seatId in booking.ConcertSeatIds)
            _seatRepo.GetById(seatId)!.Release();

        booking.SetStatus(BookingStatus.Cancelled);
        NotifyAll(h => h.OnBookingCancelled(booking));
    }

    private void NotifyAll(Action<IBookingEventHandler> action)
    {
        foreach (var h in _handlers)
            action(h);
    }
}
