using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.States;

public interface IBookingState
{
    void Confirm(Booking booking);
    void Cancel(Booking booking);
    void Refund(Booking booking);
}