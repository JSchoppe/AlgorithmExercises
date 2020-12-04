using System;
using System.Collections.Generic;

namespace SearchingAlgorithms.Searching
{
    public static partial class SearchingExtensions
    {
        // This approach assumes the collection is sorted ahead of time.
        private static int BinarySearch<T>(IList<T> collection, T value)
            where T : IComparable
        {
            // Check the input collection to avoid
            // out of range exceptions.
            if (collection.Count == 0)
                return -1;
            // Place search brackets at each end of the collection.
            int leftIndex = 0;
            int rightIndex = collection.Count - 1;
            // Split the bracket range in half repeatedly
            // to find the value using an optimized high-low game.
            int middleIndex, comparisonValue;
            while (leftIndex != rightIndex)
            {
                middleIndex = (leftIndex + rightIndex) / 2;
                // Check to see if we found the value,
                // otherwise we move one of the brackets in.
                comparisonValue = collection[middleIndex].CompareTo(value);
                if (comparisonValue > 0)
                    rightIndex = middleIndex;
                else if (comparisonValue < 0)
                    leftIndex = middleIndex;
                else
                    return middleIndex;
            }
            // If we didn't find the value, return a sentinel.
            // This search may fail if the collection isn't sorted.
            return -1;
        }
    }
}
