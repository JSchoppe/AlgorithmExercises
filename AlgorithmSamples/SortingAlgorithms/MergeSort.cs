using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace SortingAlgorithms.Sorting
{
    public static partial class SortingExtensions
    {
        private static void MergeSort<T>(ref IList<T> collection)
            where T : IComparable
        {
            MergeSortRecursive(ref collection, 0, collection.Count - 1);
        }
        private static void MergeSortRecursive<T>(ref IList<T> collection, int startIndex, int endIndex)
            where T : IComparable
        {
            // Is this subdivision of size > 1?
            // If not this recursion loop has completed.
            if (startIndex < endIndex)
            {
                int middle = (startIndex + endIndex) / 2;
                // Continue splitting the collection in half.
                MergeSortRecursive(ref collection, startIndex, middle);
                MergeSortRecursive(ref collection, middle + 1, endIndex);
                // Combine the individual chunks at each level.
                Merge(ref collection, startIndex, middle, endIndex);
            }
        }
        private static void Merge<T>(ref IList<T> collection, int start, int middle, int end)
            where T : IComparable
        {
            // Create arrays to temporarily store
            // the values from the marge halfs.
            T[] left = collection.Slice(start, middle);
            T[] right = collection.Slice(middle + 1, end);
            // Keep track of location in each sorted
            // half as we combine them together.
            int leftI = 0, rightI = 0;
            // Step through the section of the array
            // to assembly, pulling values from either
            // of the halfs.
            int i = start;
            while (leftI < left.Length && rightI < right.Length)
            {
                // Insert the next least value into the array.
                if (left[leftI].CompareTo(right[rightI]) < 0)
                {
                    collection[i] = left[leftI];
                    leftI++;
                }
                else
                {
                    collection[i] = right[rightI];
                    rightI++;
                }
                i++;
            }
            // Pull any remaining elements from left
            // or right halfs (in the case of uneven halfs).
            while (leftI < left.Length)
            {
                collection[i] = left[leftI];
                leftI++; i++;
            }
            while (rightI < right.Length)
            {
                collection[i] = right[rightI];
                rightI++; i++;
            }
        }
    }
}
