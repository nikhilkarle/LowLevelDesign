using System;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.States;

public class ConfirmedBookingState : IBookingState
{
    public void Confirm(Booking booking)
    {
        throw new InvalidOperationException("Booking is already confirmed.");
    }

    public void Cancel(Booking booking)
    {
        booking.SetStatus(BookingStatus.Cancelled);
        booking.SetState(new CancelledBookingState());
    }

    public void Refund(Booking booking)
    {
        booking.SetStatus(BookingStatus.Refunded);
        booking.SetState(new RefundedBookingState());
    }
}