namespace PrototypeDesignPattern.Prototypes
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}