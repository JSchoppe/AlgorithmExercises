using System.Collections.Generic;

namespace SearchingAlgorithms.Searching
{
    public static partial class SearchingExtensions
    {
        // This approach assumes the collection is sorted ahead of time,
        // and that the distribution of values is even.
        private static dynamic InterpolationSearch(IList<int> collection, int value)
        {
            // Check the input collection to avoid
            // out of range exceptions.
            if (collection.Count == 0)
                return -1;
            // Place search brackets at each end of the collection.
            int leftIndex = 0;
            int rightIndex = collection.Count - 1;
            // Use interpolation to rapidly close in the brackets
            // onto the target value.
            int middleIndex, comparisonValue;
            while (leftIndex != rightIndex)
            {
                // Check to see if the interpolation has stepped
                // over the range we would suspect the value to be in.
                if (collection[leftIndex] > value
                    || collection[rightIndex] < value)
                {
                    return -1;
                }
                // Use interpolation to determine a new middle index target.
                // The first two lines figure out the interpolant between
                // the values at the brackets. The final line applies this
                // back into the indices.
                middleIndex = (int)((float)
                    (value - collection[leftIndex]) /
                    (collection[rightIndex] - collection[leftIndex])
                    * (rightIndex - leftIndex) + leftIndex);
                // Check to see if we found the value,
                // otherwise we move one of the brackets in.
                comparisonValue = collection[middleIndex] - value;
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
