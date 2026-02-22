namespace TrafficSignalSystem.Events;

public sealed record VehicleCountEvent(int Count, DateTimeOffset OccurredAt) : IIntersectionEvent;