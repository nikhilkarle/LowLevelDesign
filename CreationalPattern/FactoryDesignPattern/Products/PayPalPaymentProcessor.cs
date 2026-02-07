using System;

namespace FactoryDesignPattern.Products
{
    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void Charge(decimal amount)
        {
            Console.WriteLine($"PayPal charged {amount}");
        }
    }
}