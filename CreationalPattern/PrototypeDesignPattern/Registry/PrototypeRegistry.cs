using PrototypeDesignPattern.Models;

namespace PrototypeDesignPattern.Registry
{
    public class PrototypeRegistry
    {
        private readonly Dictionary<string, Person> _personPrototypes = new();

        public void RegisterPerson(string key, Person prototype)
        {
            _personPrototypes[key] = prototype;
        }

        public Person CreatePerson(string key)
        {
            if (!_personPrototypes.TryGetValue(key, out var prototype))
                throw new KeyNotFoundException($"No Prototype Registered for this key: {key}");

            return prototype.Clone();
        }
    }
}