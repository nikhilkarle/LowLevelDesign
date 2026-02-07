namespace Classic.Collections
{
    public interface IAggregator<T>
    {
        IIterator<T> CreateItirator();
    }
}