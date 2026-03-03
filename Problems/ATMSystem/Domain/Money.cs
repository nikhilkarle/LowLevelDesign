namespace AtmSystem.Domain;

public readonly record struct Money(long Cents)
{
    public static Money Zero => new(0);
    public override string ToString()
        => $"${Cents / 100.0:0.00}";
      public static Money FromDollars(decimal dollars)
        => new((long)Math.Round(dollars * 100m, MidpointRounding.AwayFromZero));
}