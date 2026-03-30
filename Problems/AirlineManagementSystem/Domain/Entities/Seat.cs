using System;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.Entities;

public class Seat
{
    public string SeatNumber { get; private set; }
    public SeatClass SeatClass { get; private set; }
    public SeatStatus Status { get; private set; }

    public Seat(string seatNumber, SeatClass seatClass)
    {
        SeatNumber = seatNumber;
        SeatClass = seatClass;
        Status = SeatStatus.Available;
    }

    public void Reserve()
    {
        if (Status != SeatStatus.Available)
            throw new InvalidOperationException($"Seat {SeatNumber} is not available.");

        Status = SeatStatus.Reserved;
    }

    public void Release()
    {
        Status = SeatStatus.Available;
    }
}