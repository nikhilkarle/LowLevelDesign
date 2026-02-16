using ParkingLot.Domain;

namespace ParkingLot.Strategies;

public interface ISpotAssignmentStrategy
{
    ParkingSpot? FindSpot(IReadOnlyList<ParkingSpot> spots, Vehicle vehicle);
}