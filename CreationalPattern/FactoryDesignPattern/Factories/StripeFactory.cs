using FactoryDesignPattern.Products;

namespace FactoryDesignPattern.Factories
{
    public class StripeFactory : PaymentProcessorFactory
    {
        public override IPaymentProcessor CreateProcessor()
            => new StripePaymentProcessor();
    }
}