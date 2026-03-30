using System.Collections.Generic;
using AirlineManagementSystem.Application.DTOs;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Facades;

public class BookingFacade
{
    private readonly IFlightService _flightService;
    private readonly IBookingService _bookingService;

    public BookingFacade(IFlightService flightService, IBookingService bookingService)
    {
        _flightService = flightService;
        _bookingService = bookingService;
    }

    public List<Flight> SearchFlights(FlightSearchRequest request)
    {
        return _flightService.SearchFlights(request);
    }

    public Booking BookFlight(BookFlightRequest request)
    {
        return _bookingService.BookFlight(request);
    }
}