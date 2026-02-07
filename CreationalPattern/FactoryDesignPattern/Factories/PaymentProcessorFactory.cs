using FactoryDesignPattern.Products;

namespace FactoryDesignPattern.Factories
{
    public abstract class PaymentProcessorFactory
    {
        public abstract IPaymentProcessor CreateProcessor();
    }
}