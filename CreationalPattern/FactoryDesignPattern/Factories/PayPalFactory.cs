using FactoryDesignPattern.Products;

namespace FactoryDesignPattern.Factories
{
    public class PayPalFactory : PaymentProcessorFactory
    {
        public override IPaymentProcessor CreateProcessor()
            => new PayPalPaymentProcessor();
    }
}