using SingletonDesignPattern;
using System;

class Program
{
    static void Main()
    {
        Logger.Instance.Log("Hello from Singleton");

        var a = Logger.Instance;
        var b = Logger.Instance;

        Console.WriteLine(object.ReferenceEquals(a,b));
    }
}