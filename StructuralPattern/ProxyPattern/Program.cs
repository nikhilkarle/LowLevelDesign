using System;
using ProxyPattern.Services;

namespace ProxyPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserProfileService service = new CachedUserProfileProxy();

            Console.WriteLine("First call (slow):");
            var p1 = service.GetProfile("A123");
            Console.WriteLine(p1.FullName + " | " + p1.Email);

            Console.WriteLine();
            Console.WriteLine("Second call same user (fast):");
            var p2 = service.GetProfile("A123");
            Console.WriteLine(p2.FullName + " | " + p2.Email);

            Console.WriteLine();
            Console.WriteLine("Different user (slow again):");
            var p3 = service.GetProfile("B999");
            Console.WriteLine(p3.FullName + " | " + p3.Email);
        }
    }
}
