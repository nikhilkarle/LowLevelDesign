namespace Classic.Collections
{
    public class NameItirator : IIterator<string>
    {
        private readonly NameAggregate _aggregate;
        private int _index;

        public NameItirator(NameAggregate aggregate)
        {
            _aggregate = aggregate;
            _index = 0;
        } 

        public bool HasNext()
        {
            if (_index < _aggregate.Count)
            {
                return true;
            }
            return false;
        }

        public string Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No items left to itirate");
            }

            string value = _aggregate.GetAt(_index);
            _index = _index + 1;

            return value;
        }
    }
}