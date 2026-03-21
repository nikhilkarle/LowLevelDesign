using HotelManagementSystem.Application.DTOs;
using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Application.Services;

public sealed class ReservationService : IReservationService
{
    private readonly IGuestRepository _guestRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IAvailabilityService _availabilityService;
    private readonly IBillingService _billingService;
    private readonly IRateStrategy _rateStrategy;
    private readonly INotificationService _notificationService;
    private readonly IDictionary<PaymentMethod, IPaymentProcessor> _paymentProcessors;

    private readonly object _bookingLock = new();

    public ReservationService(
        IGuestRepository guestRepository,
        IRoomRepository roomRepository,
        IReservationRepository reservationRepository,
        IPaymentRepository paymentRepository,
        IAvailabilityService availabilityService,
        IBillingService billingService,
        IRateStrategy rateStrategy,
        INotificationService notificationService,
        IDictionary<PaymentMethod, IPaymentProcessor> paymentProcessors)
    {
        _guestRepository = guestRepository;
        _roomRepository = roomRepository;
        _reservationRepository = reservationRepository;
        _paymentRepository = paymentRepository;
        _availabilityService = availabilityService;
        _billingService = billingService;
        _rateStrategy = rateStrategy;
        _notificationService = notificationService;
        _paymentProcessors = paymentProcessors;
    }

    public Reservation CreateReservation(CreateReservationRequest request)
    {
        lock (_bookingLock)
        {
            var guest = _guestRepository.GetById(request.GuestId)
                        ?? throw new InvalidOperationException("Guest not found.");

            var availableRooms = _availabilityService.GetAvailableRooms(
                request.RoomType,
                request.CheckInDate,
                request.CheckOutDate);

            var room = availableRooms.FirstOrDefault()
                       ?? throw new InvalidOperationException("No rooms available for the selected date range.");

            var totalAmount = _rateStrategy.Calculate(
                room.RoomType,
                room.BasePricePerNight,
                request.CheckInDate,
                request.CheckOutDate);

            var reservation = new Reservation(
                Guid.NewGuid(),
                guest.Id,
                request.RoomType,
                request.CheckInDate,
                request.CheckOutDate,
                totalAmount);

            reservation.AssignRoom(room.Id);

            var paymentProcessor = _paymentProcessors[request.PaymentMethod];
            var payment = paymentProcessor.Process(new PaymentRequest
            {
                ReservationId = reservation.Id,
                Amount = totalAmount,
                PaymentMethod = request.PaymentMethod
            });

            if (payment.Status != PaymentStatus.Paid)
                throw new InvalidOperationException("Payment failed. Reservation cannot be confirmed.");

            reservation.Confirm();
            room.Reserve();

            _reservationRepository.Add(reservation);
            _roomRepository.Update(room);
            _paymentRepository.Add(payment);
            _billingService.CreateInvoice(reservation.Id, totalAmount);

            _notificationService.SendReservationConfirmation(guest.Id, reservation.Id);

            return reservation;
        }
    }

    public void CancelReservation(Guid reservationId)
    {
        var reservation = _reservationRepository.GetById(reservationId)
                         ?? throw new InvalidOperationException("Reservation not found.");

        reservation.Cancel();

        if (reservation.AssignedRoomId.HasValue)
        {
            var room = _roomRepository.GetById(reservation.AssignedRoomId.Value)
                       ?? throw new InvalidOperationException("Assigned room not found.");

            room.MarkAvailable();
            _roomRepository.Update(room);
        }

        _reservationRepository.Update(reservation);
    }

    public void CheckIn(CheckInRequest request)
    {
        var reservation = _reservationRepository.GetById(request.ReservationId)
                         ?? throw new InvalidOperationException("Reservation not found.");

        if (!reservation.AssignedRoomId.HasValue)
            throw new InvalidOperationException("No room assigned to reservation.");

        var room = _roomRepository.GetById(reservation.AssignedRoomId.Value)
                   ?? throw new InvalidOperationException("Room not found.");

        reservation.CheckIn();
        room.Occupy();

        _reservationRepository.Update(reservation);
        _roomRepository.Update(room);
    }

    public void CheckOut(CheckOutRequest request)
    {
        var reservation = _reservationRepository.GetById(request.ReservationId)
                         ?? throw new InvalidOperationException("Reservation not found.");

        if (!reservation.AssignedRoomId.HasValue)
            throw new InvalidOperationException("No room assigned.");

        var room = _roomRepository.GetById(reservation.AssignedRoomId.Value)
                   ?? throw new InvalidOperationException("Room not found.");

        var finalAmount = _billingService.CloseInvoice(reservation.Id);

        var paymentProcessor = _paymentProcessors[request.PaymentMethod];
        var payment = paymentProcessor.Process(new PaymentRequest
        {
            ReservationId = reservation.Id,
            Amount = finalAmount,
            PaymentMethod = request.PaymentMethod
        });

        if (payment.Status != PaymentStatus.Paid)
            throw new InvalidOperationException("Final payment failed.");

        reservation.CheckOut();
        room.MarkAvailable();

        _paymentRepository.Add(payment);
        _reservationRepository.Update(reservation);
        _roomRepository.Update(room);

        _notificationService.SendCheckOutReceipt(reservation.GuestId, reservation.Id);
    }
}