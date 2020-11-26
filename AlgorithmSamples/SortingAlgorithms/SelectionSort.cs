using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace SortingAlgorithms.Sorting
{
    public static partial class SortingExtensions
    {
        private static void SelectionSort<T>(ref IList<T> collection)
            where T : IComparable
        {
            // Store the current minimum value index
            // in the partition we are inspecting.
            int minIndex;
            // Step through the array and select the
            // minimum value in the shrinking partition.
            for (int i = 0; i < collection.Count - 1; i++)
            {
                minIndex = i;
                // Find the smallest remaining value.
                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (collection[j].CompareTo(collection[minIndex]) < 0)
                        minIndex = j;
                }
                // Move the smallest value to the end
                // of the current partition.
                collection.Swap(i, minIndex);
            }
        }
    }
}
