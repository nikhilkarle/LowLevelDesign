using System;
using System.Collections.Generic;
using FlyweightPattern.Flyweights;
using FlyweightPattern.Models;

namespace FlyweightPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TreeFactory factory = new TreeFactory();
            List<TreeInstance> forest = new List<TreeInstance>();

            for (int i = 0; i < 5; i++)
            {
                ITreeType oakType = factory.GetTreeType("Oak", "oak.png", 40, 80);
                forest.Add(new TreeInstance(oakType, x: i * 10, y: 5, scale: 1));
            }

            for (int i = 0; i < 5; i++)
            {
                ITreeType pineType = factory.GetTreeType("Pine", "pine.png", 30, 90);
                forest.Add(new TreeInstance(pineType, x: i * 12, y: 30, scale: 2));
            }

            for (int i = 0; i < forest.Count; i++)
            {
                forest[i].Draw();
            }

            Console.WriteLine();
            Console.WriteLine("Total instances: " + forest.Count);
            Console.WriteLine("Flyweights cached: " + factory.CachedCount());
        }
    }
}
