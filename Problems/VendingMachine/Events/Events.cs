namespace VendingMachine.Events;

public sealed record ProductDispensed(string ProductId) : IVendingEvent;
public sealed record ProductOutOfStock(string ProductId) : IVendingEvent;
public sealed record ProductRestocked(string ProductId, int AddedQty) : IVendingEvent;
public sealed record CashCollected(int TotalCents) : IVendingEvent;
