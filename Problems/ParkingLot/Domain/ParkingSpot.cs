namespace ParkingLot.Domain;

public sealed class ParkingSpot
{
    public string Id {get;}
    public int LevelNumber {get; }
    public SpotType SpotType {get;}

    public bool IsFree => ParkedVehicle is null;
    public Vehicle? ParkedVehicle{get; private set;}

    public ParkingSpot(string id, int levelNumber, SpotType spotType)
    {
        Id = id;
        LevelNumber = levelNumber;
        SpotType = spotType;
    }

    public bool CanFit(Vehicle v)
    {
        switch (SpotType)
        {
            case SpotType.Motorcycle:
                return v.Type == VehicleType.Motorcycle;

            case SpotType.Compact:
                return v.Type == VehicleType.Motorcycle || v.Type == VehicleType.Car;

            case SpotType.Large:
                return v.Type == VehicleType.Car || v.Type == VehicleType.Truck;

            default:
                return false;
        }
    }

    public void Park(Vehicle v)
    {
        if (!IsFree) throw new InvalidOperationException("Spot occupied.");
        if (!CanFit(v)) throw new InvalidOperationException("Vehicle cannot fit.");
        ParkedVehicle = v;
    }

    public void Unpark()
    {
        if (IsFree) throw new InvalidOperationException("Spot already free.");
        ParkedVehicle = null;
    }
}