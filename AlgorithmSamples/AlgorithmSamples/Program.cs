using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace Samples
{
    public sealed class Program
    {
        private static void Main()
        {
            // TODO write actual unit tests for this
            // that prove the shuffle yields an even
            // distribution.
            List<string> collection1 = new List<string>
            {
                "A", "B", "C", "D", "E"
            };

            collection1.Shuffle();
            foreach (string item in collection1)
                Console.Write($"{item} ");

            int[] collection2 = new int[]
            {
                1, 2, 3, 4, 5, 6, 7
            };

            collection2.Shuffle();
            foreach (int item in collection2)
                Console.Write($"{item} ");
        }
    }
}
