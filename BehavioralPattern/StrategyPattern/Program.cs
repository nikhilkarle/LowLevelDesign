using StrategyPattern.Stratagies;
using StrategyPattern.Models;
using StrategyPattern.Context;

class Program
{
    static void Main()
    {
        var cart = new List<CartItem>
        {
            new CartItem("Notebook", 5.00m, 3),
            new CartItem("Pen", 1.50m, 4)
        };

        var checkout = new ContextService(new RegularPricingStrategy());
        Console.WriteLine($"Regular: {checkout.Checkout(cart)}");

        checkout.SetStrategy(new PercentDiscountPricingStrategy(0.5m));
        Console.WriteLine($"Percent: {checkout.Checkout(cart)}");

        checkout.SetStrategy(new FlatDiscountPricingStrategy(5));
        Console.WriteLine($"Flat: {checkout.Checkout(cart)}");
    }
}