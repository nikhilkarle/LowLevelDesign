using PrototypeDesignPattern.Models;
using PrototypeDesignPattern.Registry;

namespace PrototypeDesignPattern
{
    internal class Program
    {
        static void Main()
        {
            var baseEmployee = new Person
            (
                name: "Template Employee",
                address: new Address("100 St", "Alpharetta"),
                skills: new List<string> {"C#", "Python"}
            );

            var registry = new PrototypeRegistry();
            registry.RegisterPerson("employee-default", baseEmployee);

            var emp1 = registry.CreatePerson("employee-default").With("Nikhil Karle");

            var emp2 = registry.CreatePerson("employee-default").With(name: "YOYO");
            emp2.Skills.Add("Azure");

            Console.WriteLine("Prototype:");
            Console.WriteLine(baseEmployee);
            Console.WriteLine();

            Console.WriteLine("Emp1");
            Console.WriteLine(emp1);
            Console.WriteLine();

            Console.WriteLine("Emp2");
            Console.WriteLine(emp2);
            Console.WriteLine();
        }
    }
}