using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace SortingAlgorithms.Sorting
{
    public static partial class SortingExtensions
    {
        private static void BubbleSort<T>(ref IList<T> collection)
            where T : IComparable
        {
            // Declare persistent loop variables.
            bool isSorted = false;
            int i;
            int secondLastIndex = collection.Count - 1;
            // Iterate through the collection while
            // it is unsorted.
            while (!isSorted)
            {
                isSorted = true;
                for (i = 0; i < secondLastIndex; i++)
                {
                    // Compare each adjacent pair of values.
                    if (collection[i].CompareTo(collection[i + 1]) > 0)
                    {
                        // Swap them if they are out of order,
                        // and ensure the loop runs at least one more time.
                        collection.Swap(i, i + 1);
                        isSorted = false;
                    }
                }
            }
            // Once a loop completes where every element
            // is sorted the operation is complete.
        }
    }
}
