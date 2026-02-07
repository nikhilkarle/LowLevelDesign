namespace Classic.Collections
{
    public class NameAggregate : IAggregator<string>
    {
        private readonly List<string> _names;

        public NameAggregate()
        {
            _names = new List<string>();
        }

        public void Add(string s)
        {
            _names.Add(s);
        }

        internal int Count
        {
            get {return _names.Count; }
        }

        internal string GetAt(int index)
        {
            return _names[index];
        }

        public IIterator<string> CreateItirator()
        {
            NameItirator itirator = new NameItirator(this);
            return itirator;
        }
    }
}