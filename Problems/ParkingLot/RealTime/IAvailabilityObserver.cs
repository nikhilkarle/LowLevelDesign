namespace ParkingLot.RealTime;

public interface IAvailabilityObserver
{
    void OnAvailabilityChanged(AvailabilitySnapshot snapshot);
}

public sealed record AvailabilitySnapshot(
    int TotalFreeMotorcycle,
    int TotalFreeCompact,
    int TotalFreeLarge
);