using PrototypeDesignPattern.Prototypes;

namespace PrototypeDesignPattern.Models
{
    public class Person: IPrototype<Person>
    {
        public string Name {get;}
        public Address Address {get;}
        public List<string> Skills {get;}

        public Person(string name, Address address, List<string> skills)
        {
            Name = name;
            Address = address;
            Skills = skills;
        }

        public Person Clone()
        {
            var clonedAddress = Address.Clone();
            var clonesSkills = new List<string>(Skills);

            return new Person(Name, clonedAddress, clonesSkills);
        }

        public Person With(string? name=null, Address? address=null, List<string>? skills=null)
        {
            return new Person(
                name ?? Name,
                address ?? Address,
                skills ?? new List<string>(Skills)
            );
        }

        public override string ToString()
        {
            return $"{Name} | {Address} | Skills: [{string.Join(",", Skills)}]";
        }
    }
}