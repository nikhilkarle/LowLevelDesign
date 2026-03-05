namespace LRUCache.Lru;

public interface ILruCache<TKey, TValue>
{
    bool TryGetValue(TKey key, out TValue value);
    void Put(TKey key, TValue value);

    int Count { get; }
    int Capacity { get; }
}