using System.Collections.Generic;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.Observers;

public class BookingSubject
{
    private readonly List<IBookingObserver> _observers = new();

    public void RegisterObserver(IBookingObserver observer)
    {
        _observers.Add(observer);
    }

    public void NotifyBookingCreated(Booking booking)
    {
        foreach (var observer in _observers)
            observer.OnBookingCreated(booking);
    }

    public void NotifyBookingCancelled(Booking booking)
    {
        foreach (var observer in _observers)
            observer.OnBookingCancelled(booking);
    }

    public void NotifyBookingChanged(Booking booking)
    {
        foreach (var observer in _observers)
            observer.OnBookingChanged(booking);
    }
}