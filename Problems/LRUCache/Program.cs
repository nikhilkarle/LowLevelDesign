using LRUCache.Lru;

namespace LRUCache;

internal static class Program
{
    private sealed class IntLruCache
    {
        private readonly ILruCache<int, int> _cache;

        public IntLruCache(int capacity) => _cache = new LruCache<int, int>(capacity);

        public int Get(int key) => _cache.TryGetValue(key, out var value) ? value : -1;

        public void Put(int key, int value) => _cache.Put(key, value);
    }

    public static void Main()
    {
        var cache = new IntLruCache(capacity: 2);

        cache.Put(1, 10);
        cache.Put(2, 20);

        Console.WriteLine(cache.Get(1)); 
        cache.Put(3, 30);                
        Console.WriteLine(cache.Get(2)); 
        cache.Put(4, 40);                
        Console.WriteLine(cache.Get(1)); 
        Console.WriteLine(cache.Get(3)); 
        Console.WriteLine(cache.Get(4));
    }
}