namespace AtmSystem.Hardware;

public interface ICardReader
{
    Task<string?> ReadCardAsync();
}