using System;
using System.Collections.Generic;
using AirlineManagementSystem.Domain.Enums;
using AirlineManagementSystem.Domain.States;

namespace AirlineManagementSystem.Domain.Entities;

public class Booking
{
    public Guid Id { get; }
    public string Pnr { get; }
    public Guid PassengerId { get; }
    public Guid FlightId { get; private set; }
    public string SeatNumber { get; private set; }
    public List<Baggage> BaggageItems { get; private set; }
    public BookingStatus Status { get; private set; }
    public IBookingState State { get; private set; }
    public DateTime CreatedAtUtc { get; }

    public Booking(Guid id, string pnr, Guid passengerId, Guid flightId, string seatNumber, List<Baggage> baggageItems)
    {
        Id = id;
        Pnr = pnr;
        PassengerId = passengerId;
        FlightId = flightId;
        SeatNumber = seatNumber;
        BaggageItems = baggageItems;
        Status = BookingStatus.Created;
        State = new CreatedBookingState();
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Confirm()
    {
        State.Confirm(this);
    }

    public void Cancel()
    {
        State.Cancel(this);
    }

    public void Refund()
    {
        State.Refund(this);
    }

    public void ChangeFlight(Guid newFlightId, string newSeatNumber)
    {
        FlightId = newFlightId;
        SeatNumber = newSeatNumber;
        Status = BookingStatus.Changed;
    }

    public void SetStatus(BookingStatus status)
    {
        Status = status;
    }

    public void SetState(IBookingState state)
    {
        State = state;
    }
}