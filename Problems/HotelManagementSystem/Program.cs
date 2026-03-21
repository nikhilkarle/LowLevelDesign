using HotelManagementSystem.Application.DTOs;
using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Application.Services;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Infrastructure.Interfaces;
using HotelManagementSystem.Infrastructure.Payments;
using HotelManagementSystem.Infrastructure.Repositories;

IRoomRepository roomRepository = new InMemoryRoomRepository();
IGuestRepository guestRepository = new InMemoryGuestRepository();
IReservationRepository reservationRepository = new InMemoryReservationRepository();
IInvoiceRepository invoiceRepository = new InMemoryInvoiceRepository();
IPaymentRepository paymentRepository = new InMemoryPaymentRepository();

var paymentProcessors = new Dictionary<PaymentMethod, IPaymentProcessor>
{
    { PaymentMethod.Cash, new CashPaymentProcessor() },
    { PaymentMethod.CreditCard, new CreditCardPaymentProcessor() },
    { PaymentMethod.Online, new OnlinePaymentProcessor() }
};

IRateStrategy rateStrategy = new WeekendSurgeRateStrategy();
INotificationService notificationService = new NotificationService();
IAvailabilityService availabilityService = new AvailabilityService(roomRepository, reservationRepository);
IBillingService billingService = new BillingService(invoiceRepository);

IReservationService reservationService = new ReservationService(
    guestRepository,
    roomRepository,
    reservationRepository,
    paymentRepository,
    availabilityService,
    billingService,
    rateStrategy,
    notificationService,
    paymentProcessors);

IReportService reportService = new ReportService(roomRepository, reservationRepository, paymentRepository);

// seed data
var guest = new Guest(Guid.NewGuid(), "John Doe", "john@example.com", "1234567890");
guestRepository.Add(guest);

var room1 = new Room(Guid.NewGuid(), "101", RoomType.Single, 100);
var room2 = new Room(Guid.NewGuid(), "102", RoomType.Single, 120);
var room3 = new Room(Guid.NewGuid(), "201", RoomType.Suite, 300);

roomRepository.Add(room1);
roomRepository.Add(room2);
roomRepository.Add(room3);

// create reservation
var reservation = reservationService.CreateReservation(new CreateReservationRequest
{
    GuestId = guest.Id,
    RoomType = RoomType.Single,
    CheckInDate = DateTime.UtcNow.Date,
    CheckOutDate = DateTime.UtcNow.Date.AddDays(2),
    PaymentMethod = PaymentMethod.CreditCard
});

Console.WriteLine($"Reservation created: {reservation.Id}, Status: {reservation.Status}");

// check-in
reservationService.CheckIn(new CheckInRequest
{
    ReservationId = reservation.Id
});

Console.WriteLine("Guest checked in.");

// add minibar charge
billingService.AddCharge(reservation.Id, "Mini Bar", 50);

// check-out
reservationService.CheckOut(new CheckOutRequest
{
    ReservationId = reservation.Id,
    PaymentMethod = PaymentMethod.Cash
});

Console.WriteLine("Guest checked out.");

var occupancyRate = reportService.GetOccupancyRate(DateTime.UtcNow.Date);
var revenue = reportService.GetRevenue(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow);

Console.WriteLine($"Occupancy Rate: {occupancyRate}%");
Console.WriteLine($"Revenue: {revenue}");