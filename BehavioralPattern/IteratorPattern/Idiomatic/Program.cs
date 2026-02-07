using System;
using Idiomatic.Collections;

namespace Idiomatic
{
    internal class Program
    {
        static void Main()
        {
            NameCollection names = new NameCollection();        
            names.Add("NK");
            names.Add("NK2");
            names.Add("NK3");

            foreach(string n in names)
            {
                Console.WriteLine(n);
            }
            
        }
    }
}