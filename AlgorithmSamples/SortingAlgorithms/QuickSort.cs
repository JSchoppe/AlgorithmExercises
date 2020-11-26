using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace SortingAlgorithms.Sorting
{
    public static partial class SortingExtensions
    {
        private static void QuickSort<T>(ref IList<T> collection)
            where T : IComparable
        {
            QuickSortRecursive(ref collection, 0, collection.Count - 1);
        }
        private static void QuickSortRecursive<T>(ref IList<T> collection, int startIndex, int endIndex)
            where T : IComparable
        {
            // Is this subdivision of size > 1?
            // If not this recursion loop has completed.
            if (startIndex < endIndex)
            {
                // Quick sort around the partition for this set.
                int partitionIndex = QuickSortPartition(ref collection, startIndex, endIndex);
                // Recursively sort elements before the
                // partition and after the partition.
                QuickSortRecursive(ref collection, startIndex, partitionIndex - 1);
                QuickSortRecursive(ref collection, partitionIndex + 1, endIndex);
            }
        }
        private static int QuickSortPartition<T>(ref IList<T> collection, int startIndex, int endIndex)
            where T : IComparable
        {
            // Grab the rightmost element as the pivot.
            T pivotValue = collection[endIndex];
            // Keep track of where the partition will
            // be inserted (elements to the left).
            int pivotIndex = startIndex - 1;
            // Step through this section and divide the values
            // around the partition.
            for (int i = startIndex; i < endIndex; i++)
            {
                // Is this value smaller than the pivot?
                if (collection[i].CompareTo(pivotValue) < 0)
                {
                    // Push the pivot insertion point forward.
                    pivotIndex++;
                    // Pull the element behind the pivot.
                    collection.Swap(pivotIndex, i);
                }
            }
            // Ensure the pivot is placed after all smaller elements.
            pivotIndex++;
            // Insert the properly sorted partition element.
            collection.Swap(pivotIndex, endIndex);
            return pivotIndex;
        }
    }
}
