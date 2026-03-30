using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.Observers;

public interface IBookingObserver
{
    void OnBookingCreated(Booking booking);
    void OnBookingCancelled(Booking booking);
    void OnBookingChanged(Booking booking);
}