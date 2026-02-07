using System.Collections.Generic;
using System.Collections;

namespace Idiomatic.Collections
{
    public class NameCollection : IEnumerable<string>
    {
        private readonly List<string> _names = new List<string>();

        public void Add(string name)
        {
            _names.Add(name);
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < _names.Count; i++)
            {
                yield return _names[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}