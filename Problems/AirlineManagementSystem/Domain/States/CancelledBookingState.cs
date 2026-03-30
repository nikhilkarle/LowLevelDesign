using System;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.States;

public class CancelledBookingState : IBookingState
{
    public void Confirm(Booking booking)
    {
        throw new InvalidOperationException("Cancelled booking cannot be confirmed.");
    }

    public void Cancel(Booking booking)
    {
        throw new InvalidOperationException("Booking is already cancelled.");
    }

    public void Refund(Booking booking)
    {
        booking.SetState(new RefundedBookingState());
    }
}