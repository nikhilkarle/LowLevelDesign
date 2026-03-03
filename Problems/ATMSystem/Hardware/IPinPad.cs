namespace AtmSystem.Hardware;

public interface IPinPad
{
    Task<string?> ReadPinAsync();
}