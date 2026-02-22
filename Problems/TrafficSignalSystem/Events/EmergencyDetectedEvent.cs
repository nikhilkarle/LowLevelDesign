namespace TrafficSignalSystem.Events;

public sealed record EmergencyDetectedEvent(DateTimeOffset OccurredAt) : IIntersectionEvent;

