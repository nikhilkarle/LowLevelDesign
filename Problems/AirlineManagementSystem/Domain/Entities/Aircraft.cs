using System;

namespace AirlineManagementSystem.Domain.Entities;

public class Aircraft
{
    public Guid Id { get; }
    public string Model { get; private set; }
    public string RegistrationNumber { get; private set; }
    public int Rows { get; private set; }
    public int SeatsPerRow { get; private set; }

    public Aircraft(Guid id, string model, string registrationNumber, int rows, int seatsPerRow)
    {
        Id = id;
        Model = model;
        RegistrationNumber = registrationNumber;
        Rows = rows;
        SeatsPerRow = seatsPerRow;
    }
}