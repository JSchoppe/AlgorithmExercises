using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace SortingAlgorithms.Sorting
{
    public static partial class SortingExtensions
    {
        private static void InsertionSort<T>(ref IList<T> collection)
            where T : IComparable
        {
            // Step along the array.
            for (int i = 1; i < collection.Count; i++)
                // Look backwards at each prior adjacent pair.
                for (int j = i; j > 0; j--)
                    // Fix order of the prior pair if neccasary.
                    if (collection[j].CompareTo(collection[j - 1]) < 0)
                        collection.Swap(j - 1, j);
        }
    }
}
