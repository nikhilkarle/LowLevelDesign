using System;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.Observers;

public class EmailNotificationObserver : IBookingObserver
{
    public void OnBookingCreated(Booking booking)
    {
        Console.WriteLine($"[EMAIL] Booking confirmed for PNR {booking.Pnr}");
    }

    public void OnBookingCancelled(Booking booking)
    {
        Console.WriteLine($"[EMAIL] Booking cancelled for PNR {booking.Pnr}");
    }

    public void OnBookingChanged(Booking booking)
    {
        Console.WriteLine($"[EMAIL] Booking changed for PNR {booking.Pnr}");
    }
}