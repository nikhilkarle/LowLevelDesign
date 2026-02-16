using ParkingLot.Domain;
using ParkingLot.RealTime;
using ParkingLot.Services;
using ParkingLot.Strategies;

var levels = new List<ParkingLevel>
{
    new ParkingLevel(1, new []
    {
        new ParkingSpot("L1-M1", 1, SpotType.Motorcycle),
        new ParkingSpot("L1-C1", 1, SpotType.Compact),
        new ParkingSpot("L1-L1", 1, SpotType.Large),
    }),
    new ParkingLevel(2, new []
    {
        new ParkingSpot("L2-C1", 2, SpotType.Compact),
        new ParkingSpot("L2-L2", 2, SpotType.Large),
    })
};

var publisher = new AvailabilityPublisher();
publisher.Subscribe(new ConsoleBoard());

var svc = new ParkingLotService(
    levels,
    new TicketStore(),
    new BestFitStrategy(),
    publisher
);

var t = svc.Enter(new Vehicle("ABC-123", VehicleType.Car));
svc.Exit(t.TicketId);

sealed class ConsoleBoard : IAvailabilityObserver
{
    public void OnAvailabilityChanged(AvailabilitySnapshot snapshot)
        => Console.WriteLine($"Free: M={snapshot.TotalFreeMotorcycle}, C={snapshot.TotalFreeCompact}, L={snapshot.TotalFreeLarge}");
}
