using System;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.States;

public class CreatedBookingState : IBookingState
{
    public void Confirm(Booking booking)
    {
        booking.SetStatus(BookingStatus.Confirmed);
        booking.SetState(new ConfirmedBookingState());
    }

    public void Cancel(Booking booking)
    {
        booking.SetStatus(BookingStatus.Cancelled);
        booking.SetState(new CancelledBookingState());
    }

    public void Refund(Booking booking)
    {
        throw new InvalidOperationException("Cannot refund a booking before confirmation/payment.");
    }
}