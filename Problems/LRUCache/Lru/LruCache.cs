namespace LRUCache.Lru;

public sealed class LruCache<TKey, TValue> : ILruCache<TKey, TValue>
{
    public sealed class Entry
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<Entry>> _map;
    private readonly LinkedList<Entry> _lruList;
    private readonly object _gate = new();
    public int Capacity => _capacity;
    public int Count
    {
        get { lock (_gate) return _map.Count; }
    }
    public LruCache(int capacity, IEqualityComparer<TKey>? comparer = null)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be > 0.");

        _capacity = capacity;
        _map = new Dictionary<TKey, LinkedListNode<Entry>>(comparer);
        _lruList = new LinkedList<Entry>();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        lock (_gate)
        {
            if (!_map.TryGetValue(key, out var node))
            {
                value = default!;
                return false;
            }

            MoveToFront(node);
            value = node.Value.Value;
            return true;
        }
    }
    public void Put(TKey key, TValue value)
    {
        lock (_gate)
        {
            if (_map.TryGetValue(key, out var existingNode))
            {
                existingNode.Value.Value = value;
                MoveToFront(existingNode);
                return;
            }

            if (_map.Count >= _capacity)
                EvictLeastRecentlyUsed();

            var node = _lruList.AddFirst(new Entry(key, value));
            _map[key] = node;
        }
    }

    private void MoveToFront(LinkedListNode<Entry> node)
    {
        if (node == _lruList.First) return;

        _lruList.Remove(node);
        _lruList.AddFirst(node);
    }
    private void EvictLeastRecentlyUsed()
    {
        var lru = _lruList.Last;
        if (lru is null) return;

        _lruList.RemoveLast();
        _map.Remove(lru.Value.Key);
    }

}

