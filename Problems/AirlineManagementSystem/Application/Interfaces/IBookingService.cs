using AirlineManagementSystem.Application.DTOs;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IBookingService
{
    Booking BookFlight(BookFlightRequest request);
    void CancelBooking(CancelBookingRequest request);
    Booking ChangeFlight(ChangeFlightRequest request);
}