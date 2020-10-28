using System;
using System.Collections.Generic;
using AlgorithmsLibrary;
using Samples.Collection_Type_Comparisons;

namespace Samples
{
    public sealed class Program
    {
        private static void Main()
        {
            // Uncomment to run example code.

            //DemoShuffle();
            //ArrayVsMap.DemoAccess();
        }

        private static void DemoShuffle()
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
