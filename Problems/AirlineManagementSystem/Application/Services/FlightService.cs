using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Application.DTOs;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Specifications;

namespace AirlineManagementSystem.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public List<Flight> SearchFlights(FlightSearchRequest request)
    {
        var spec = new FlightSearchSpecification(request.Source, request.Destination, request.Date);
        return _flightRepository.GetAll().Where(spec.IsSatisfiedBy).ToList();
    }
}