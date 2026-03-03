namespace AtmSystem.Atm;

public enum AtmState
{
    Idle,
    CardInserted,
    PinEntry,
    Authenticated,
    TransactionSelection,
    Processing,
    Ejecting
}