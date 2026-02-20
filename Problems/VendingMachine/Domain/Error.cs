namespace VendingMachine.Domain;

public abstract record VendingError(string Message);

public sealed record InvalidProductError(string ProductId)
    : VendingError($"Invalid product: {ProductId}");

public sealed record OutOfStockError(string ProductId)
    : VendingError($"Product out of stock: {ProductId}");

public sealed record InsufficientFundsError(int RequiredCents, int InsertedCents)
    : VendingError($"Insufficient funds. Required={RequiredCents}c, Inserted={InsertedCents}c");

public sealed record CannotMakeChangeError(int ChangeCents)
    : VendingError($"Cannot make change for {ChangeCents}c with available denominations.");

public sealed record TransactionStateError(string Details)
    : VendingError($"Invalid operation for current state: {Details}");
