namespace FactoryDesignPattern.Products
{
    public interface IPaymentProcessor
    {
        void Charge(decimal amount);
    }
}