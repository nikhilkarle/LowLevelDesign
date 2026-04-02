using System;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Domain.States;

public class RefundedBookingState : IBookingState
{
    public void Confirm(Booking booking)
    {
        throw new InvalidOperationException("Refunded booking cannot be confirmed.");
    }

    public void Cancel(Booking booking)
    {
        throw new InvalidOperationException("Refunded booking cannot be cancelled again.");
    }

    public void Refund(Booking booking)
    {
        throw new InvalidOperationException("Booking is already refunded.");
    }
}