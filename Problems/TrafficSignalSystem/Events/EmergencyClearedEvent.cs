namespace TrafficSignalSystem.Events;

public sealed record EmergencyClearedEvent(DateTimeOffset OccurredAt) : IIntersectionEvent;