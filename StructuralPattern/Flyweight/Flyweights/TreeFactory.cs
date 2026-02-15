using System;
using System.Collections.Generic;

namespace FlyweightPattern.Flyweights
{
    public class TreeFactory
    {
        private readonly Dictionary<string, ITreeType> _cache;

        public TreeFactory()
        {
            _cache = new Dictionary<string, ITreeType>(StringComparer.OrdinalIgnoreCase);
        }

        public ITreeType GetTreeType(string name, string textureFile, int width, int height)
        {
            string key = name + "|" + textureFile + "|" + width + "|" + height;

            ITreeType existing;
            bool found = _cache.TryGetValue(key, out existing);

            if (found)
            {
                return existing;
            }

            ITreeType created = new TreeType(name, textureFile, width, height);
            _cache[key] = created;
            return created;
        }

        public int CachedCount()
        {
            return _cache.Count;
        }
    }
}