namespace AtmSystem.Hardware;

public interface IReceiptPrinter
{
    Task PrintAsync(string text);
}