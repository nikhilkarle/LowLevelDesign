using ParkingLot.Domain;

namespace ParkingLot.Strategies;

public sealed class BestFitStrategy : ISpotAssignmentStrategy
{
    public ParkingSpot? FindSpot(IReadOnlyList<ParkingSpot> spots, Vehicle vehicle)
    {
        SpotType[] pref = vehicle.Type switch
        {
            VehicleType.Motorcycle => new[] {SpotType.Motorcycle, SpotType.Compact},
            VehicleType.Car => new[] {SpotType.Large, SpotType.Compact},
            VehicleType.Truck => new[] {SpotType.Large},
            _ => new[] {SpotType.Compact}

        };

        foreach (var spotType in pref)
        {
            var spot = spots.FirstOrDefault(s => s.IsFree && s.SpotType == spotType && s.CanFit(vehicle));
            if (spot is not null) return spot;
        }

        return null;
    }
}