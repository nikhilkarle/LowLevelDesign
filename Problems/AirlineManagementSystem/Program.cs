using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Application.DTOs;
using AirlineManagementSystem.Application.Facades;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Application.Pay;
using AirlineManagementSystem.Application.Services;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;
using AirlineManagementSystem.Domain.Observers;
using AirlineManagementSystem.Infrastructure.Concurrency;
using AirlineManagementSystem.Infrastructure.Repositories;

namespace AirlineManagementSystem;

public static class Program
{
    public static void Main()
    {
        IFlightRepository flightRepository = new InMemoryFlightRepository();
        IBookingRepository bookingRepository = new InMemoryBookingRepository();
        IUserRepository userRepository = new InMemoryUserRepository();
        IPaymentRepository paymentRepository = new InMemoryPaymentRepository();

        SeedData(flightRepository, userRepository);

        var lockProvider = new FlightSeatLockProvider();
        var bookingSubject = new BookingSubject();
        bookingSubject.RegisterObserver(new EmailNotificationObserver());
        bookingSubject.RegisterObserver(new SmsNotificationObserver());

        var paymentFactory = new PaymentStrategyFactory();
        var paymentProcessor = new PaymentProcessor(paymentFactory);

        IFlightService flightService = new FlightService(flightRepository);
        IPaymentService paymentService = new PaymentService(paymentRepository, paymentProcessor);
        IBookingService bookingService = new BookingService(
            flightRepository,
            bookingRepository,
            userRepository,
            paymentService,
            bookingSubject,
            lockProvider);

        var bookingFacade = new BookingFacade(flightService, bookingService);

        Console.WriteLine("=== AVAILABLE FLIGHTS ===");
        var searchRequest = new FlightSearchRequest("DEL", "BLR", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));
        var flights = bookingFacade.SearchFlights(searchRequest);

        foreach (var flight in flights)
        {
            Console.WriteLine($"Flight: {flight.FlightNumber}, Route: {flight.Source}->{flight.Destination}, Departure: {flight.Schedule.DepartureTimeUtc}");
        }

        var passenger = userRepository.GetAll().OfType<Passenger>().First();
        var selectedFlight = flights.First();
        var chosenSeat = selectedFlight.Seats.First(s => s.Status == SeatStatus.Available).SeatNumber;

        Console.WriteLine();
        Console.WriteLine("=== BOOKING FLIGHT ===");
        var bookingRequest = new BookFlightRequest(
            passenger.Id,
            selectedFlight.Id,
            chosenSeat,
            PaymentMethod.CreditCard,
            15000,
            new List<Baggage>
            {
                new Baggage(Guid.NewGuid(), 15, "Cabin Bag"),
                new Baggage(Guid.NewGuid(), 20, "Check-in Bag")
            });

        var booking = bookingFacade.BookFlight(bookingRequest);
        Console.WriteLine($"Booking created. PNR: {booking.Pnr}, Status: {booking.Status}, Seat: {booking.SeatNumber}");

        Console.WriteLine();
        Console.WriteLine("=== CHANGING FLIGHT ===");
        var anotherFlight = flightRepository.GetAll()
            .First(f => f.Id != selectedFlight.Id && f.Source == "DEL" && f.Destination == "BLR");
        var newSeat = anotherFlight.Seats.First(s => s.Status == SeatStatus.Available).SeatNumber;
        var changedBooking = bookingService.ChangeFlight(new ChangeFlightRequest(booking.Id, anotherFlight.Id, newSeat));
        Console.WriteLine($"Booking changed. New flight: {changedBooking.FlightId}, New seat: {changedBooking.SeatNumber}, Status: {changedBooking.Status}");

        Console.WriteLine();
        Console.WriteLine("=== CANCELLING BOOKING ===");
        bookingService.CancelBooking(new CancelBookingRequest(changedBooking.Id, "Passenger requested cancellation"));
        var cancelled = bookingRepository.GetById(changedBooking.Id)!;
        Console.WriteLine($"Booking cancelled. Status: {cancelled.Status}");
    }

    private static void SeedData(IFlightRepository flightRepository, IUserRepository userRepository)
    {
        var passenger = new Passenger(
            Guid.NewGuid(),
            "Alex Passenger",
            "alex@example.com",
            UserRole.Passenger,
            "P123456",
            new DateOnly(1998, 5, 20));

        var admin = new User(Guid.NewGuid(), "Admin User", "admin@example.com", UserRole.Admin);
        var staff = new User(Guid.NewGuid(), "Ground Staff", "staff@example.com", UserRole.AirlineStaff);

        userRepository.Add(passenger);
        userRepository.Add(admin);
        userRepository.Add(staff);

        var aircraft1 = new Aircraft(Guid.NewGuid(), "A320", "VT-AX1", 6, 5);
        var aircraft2 = new Aircraft(Guid.NewGuid(), "B737", "VT-BX2", 6, 5);

        var crew1 = new List<CrewMember>
        {
            new(Guid.NewGuid(), "Captain John", CrewRole.Pilot),
            new(Guid.NewGuid(), "Officer Jane", CrewRole.CoPilot),
            new(Guid.NewGuid(), "Crew Sam", CrewRole.CabinCrew)
        };

        var crew2 = new List<CrewMember>
        {
            new(Guid.NewGuid(), "Captain Mike", CrewRole.Pilot),
            new(Guid.NewGuid(), "Officer Rose", CrewRole.CoPilot),
            new(Guid.NewGuid(), "Crew Nina", CrewRole.CabinCrew)
        };

        var tomorrow = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

        var flight1 = Flight.Create(
            Guid.NewGuid(),
            "AI101",
            "DEL",
            "BLR",
            new FlightSchedule(Guid.NewGuid(), DateTime.UtcNow.AddDays(1).Date.AddHours(6), DateTime.UtcNow.AddDays(1).Date.AddHours(8).AddMinutes(30)),
            aircraft1,
            crew1,
            FlightStatus.Scheduled);

        var flight2 = Flight.Create(
            Guid.NewGuid(),
            "AI102",
            "DEL",
            "BLR",
            new FlightSchedule(Guid.NewGuid(), DateTime.UtcNow.AddDays(1).Date.AddHours(12), DateTime.UtcNow.AddDays(1).Date.AddHours(14).AddMinutes(30)),
            aircraft2,
            crew2,
            FlightStatus.Scheduled);

        flightRepository.Add(flight1);
        flightRepository.Add(flight2);
    }
}