using Classic.Collections;
using System;

namespace Classic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NameAggregate names = new NameAggregate();
            names.Add("NK");
            names.Add("NK1");
            names.Add("NK2");

            IIterator<string> itirator = names.CreateItirator();
            while (itirator.HasNext())
            {
                string value = itirator.Next();
                Console.WriteLine(value);
            }
        }
    }
}