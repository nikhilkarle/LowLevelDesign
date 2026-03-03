namespace AtmSystem.Domain;

public sealed record Account(string AccountId, long BalanceCents, bool IsActive, int Version);