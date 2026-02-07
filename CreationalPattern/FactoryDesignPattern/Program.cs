using FactoryDesignPattern.Factories;

class Program
{
    static void Main()
    {
        PaymentProcessorFactory factory = new StripeFactory();
        var processor = factory.CreateProcessor();
        processor.Charge(50);

        factory = new PayPalFactory();
        processor = factory.CreateProcessor();
        processor.Charge(75);        
    }
}