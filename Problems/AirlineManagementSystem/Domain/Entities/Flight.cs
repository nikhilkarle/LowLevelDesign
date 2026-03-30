using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.Entities;

public class Flight
{
    public Guid Id { get; }
    public string FlightNumber { get; private set; }
    public string Source { get; private set; }
    public string Destination { get; private set; }
    public FlightSchedule Schedule { get; private set; }
    public Aircraft Aircraft { get; private set; }
    public List<CrewMember> CrewMembers { get; private set; }
    public List<Seat> Seats { get; private set; }
    public FlightStatus Status { get; private set; }

    private Flight(
        Guid id,
        string flightNumber,
        string source,
        string destination,
        FlightSchedule schedule,
        Aircraft aircraft,
        List<CrewMember> crewMembers,
        List<Seat> seats,
        FlightStatus status)
    {
        Id = id;
        FlightNumber = flightNumber;
        Source = source;
        Destination = destination;
        Schedule = schedule;
        Aircraft = aircraft;
        CrewMembers = crewMembers;
        Seats = seats;
        Status = status;
    }

    public static Flight Create(
        Guid id,
        string flightNumber,
        string source,
        string destination,
        FlightSchedule schedule,
        Aircraft aircraft,
        List<CrewMember> crewMembers,
        FlightStatus status)
    {
        var seats = GenerateSeats(aircraft.Rows, aircraft.SeatsPerRow);
        return new Flight(id, flightNumber, source, destination, schedule, aircraft, crewMembers, seats, status);
    }

    private static List<Seat> GenerateSeats(int rows, int seatsPerRow)
    {
        var result = new List<Seat>();
        for (int row = 1; row <= rows; row++)
        {
            for (int col = 0; col < seatsPerRow; col++)
            {
                var seatLetter = (char)('A' + col);
                var seatNumber = $"{row}{seatLetter}";
                result.Add(new Seat(seatNumber, row <= 1 ? SeatClass.Business : SeatClass.Economy));
            }
        }
        return result;
    }

    public Seat GetSeat(string seatNumber)
    {
        var seat = Seats.FirstOrDefault(s => s.SeatNumber.Equals(seatNumber, StringComparison.OrdinalIgnoreCase));
        if (seat is null)
            throw new InvalidOperationException($"Seat {seatNumber} does not exist on flight {FlightNumber}.");

        return seat;
    }

    public void AssignAircraft(Aircraft aircraft)
    {
        Aircraft = aircraft;
    }

    public void AssignCrew(List<CrewMember> crewMembers)
    {
        CrewMembers = crewMembers;
    }
}