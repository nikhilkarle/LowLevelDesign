using System;
using System.Linq;
using AirlineManagementSystem.Application.DTOs;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;
using AirlineManagementSystem.Domain.Observers;
using AirlineManagementSystem.Infrastructure.Concurrency;

namespace AirlineManagementSystem.Application.Services;

public class BookingService : IBookingService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPaymentService _paymentService;
    private readonly BookingSubject _bookingSubject;
    private readonly FlightSeatLockProvider _lockProvider;

    public BookingService(
        IFlightRepository flightRepository,
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        IPaymentService paymentService,
        BookingSubject bookingSubject,
        FlightSeatLockProvider lockProvider)
    {
        _flightRepository = flightRepository;
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _paymentService = paymentService;
        _bookingSubject = bookingSubject;
        _lockProvider = lockProvider;
    }

    public Booking BookFlight(BookFlightRequest request)
    {
        var user = _userRepository.GetById(request.PassengerId)
                   ?? throw new InvalidOperationException("Passenger not found.");

        if (user.Role != UserRole.Passenger)
            throw new InvalidOperationException("Only passengers can book flights.");

        var flight = _flightRepository.GetById(request.FlightId)
                     ?? throw new InvalidOperationException("Flight not found.");

        if (flight.Status != FlightStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled flights can be booked.");

        var flightLock = _lockProvider.GetLock(flight.Id);
        lock (flightLock)
        {
            var seat = flight.GetSeat(request.SeatNumber);
            seat.Reserve();
            _flightRepository.Update(flight);

            var booking = new Booking(
                Guid.NewGuid(),
                GeneratePnr(),
                request.PassengerId,
                request.FlightId,
                request.SeatNumber,
                request.BaggageItems);

            var payment = _paymentService.ProcessPayment(booking.Id, request.Amount, request.PaymentMethod);
            if (payment.Status != PaymentStatus.Success)
            {
                seat.Release();
                _flightRepository.Update(flight);
                throw new InvalidOperationException("Payment failed. Booking aborted.");
            }

            booking.Confirm();
            _bookingRepository.Add(booking);
            _bookingSubject.NotifyBookingCreated(booking);
            return booking;
        }
    }

    public void CancelBooking(CancelBookingRequest request)
    {
        var booking = _bookingRepository.GetById(request.BookingId)
                      ?? throw new InvalidOperationException("Booking not found.");

        var flight = _flightRepository.GetById(booking.FlightId)
                     ?? throw new InvalidOperationException("Flight not found.");

        var flightLock = _lockProvider.GetLock(flight.Id);
        lock (flightLock)
        {
            booking.Cancel();
            var seat = flight.GetSeat(booking.SeatNumber);
            seat.Release();
            _flightRepository.Update(flight);

            _paymentService.RefundPayment(booking.Id);
            booking.Refund();
            _bookingRepository.Update(booking);
            _bookingSubject.NotifyBookingCancelled(booking);
        }
    }

    public Booking ChangeFlight(ChangeFlightRequest request)
    {
        var booking = _bookingRepository.GetById(request.BookingId)
                      ?? throw new InvalidOperationException("Booking not found.");

        var oldFlight = _flightRepository.GetById(booking.FlightId)
                        ?? throw new InvalidOperationException("Current flight not found.");

        var newFlight = _flightRepository.GetById(request.NewFlightId)
                        ?? throw new InvalidOperationException("New flight not found.");

        var orderedFlightIds = new[] { oldFlight.Id, newFlight.Id }.OrderBy(x => x).ToList();
        var firstLock = _lockProvider.GetLock(orderedFlightIds[0]);
        var secondLock = _lockProvider.GetLock(orderedFlightIds[1]);

        lock (firstLock)
        {
            lock (secondLock)
            {
                var oldSeat = oldFlight.GetSeat(booking.SeatNumber);
                var newSeat = newFlight.GetSeat(request.NewSeatNumber);

                newSeat.Reserve();
                oldSeat.Release();

                _flightRepository.Update(oldFlight);
                _flightRepository.Update(newFlight);

                booking.ChangeFlight(newFlight.Id, request.NewSeatNumber);
                _bookingRepository.Update(booking);
                _bookingSubject.NotifyBookingChanged(booking);
                return booking;
            }
        }
    }

    private static string GeneratePnr()
    {
        return Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();
    }
}