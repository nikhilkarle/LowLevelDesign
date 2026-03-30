using System;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.Observers;

public class SmsNotificationObserver : IBookingObserver
{
    public void OnBookingCreated(Booking booking)
    {
        Console.WriteLine($"[SMS] Booking confirmed for PNR {booking.Pnr}");
    }

    public void OnBookingCancelled(Booking booking)
    {
        Console.WriteLine($"[SMS] Booking cancelled for PNR {booking.Pnr}");
    }

    public void OnBookingChanged(Booking booking)
    {
        Console.WriteLine($"[SMS] Booking changed for PNR {booking.Pnr}");
    }
}