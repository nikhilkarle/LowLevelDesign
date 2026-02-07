using System; 

namespace BuilderDesignPattern{
    class Program
    {
        static void Main()
        {
            var user = new UserBuilder()
            .WithName("Niku")
            .WithAge(25)
            .WithEmail("nk@gmail.com")
            .WithPhone("951")
            .Build();

            Console.WriteLine(user);
        }
    }
}