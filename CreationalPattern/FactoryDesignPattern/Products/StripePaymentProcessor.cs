using System;

namespace FactoryDesignPattern.Products
{
    public class StripePaymentProcessor : IPaymentProcessor
    {
        public void Charge(decimal amount)
        {
            Console.WriteLine($"Stripe charged {amount}");
        }
    }

}