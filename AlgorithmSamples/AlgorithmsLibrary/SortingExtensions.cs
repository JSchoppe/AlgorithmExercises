using System;
using System.Collections.Generic;

namespace AlgorithmsLibrary.Sorting
{
    #region Exposed Enums
    /// <summary>
    /// Used to specify a sorting strategy when
    /// the caller is aware of the type of data they have.
    /// </summary>
    public enum SortAlgorithm : byte
    {
        Bubble, Insertion, Selection, Heap, Quick, Merge
    };
    #endregion
    /// <summary>
    /// Contains extensions methods for sorting collections.
    /// </summary>
    public static partial class SortingExtensions
    {
        #region Sort Exposed Method
        /// <summary>
        /// Sorts this collection into non-decreasing order.
        /// </summary>
        /// <param name="collection">The collection to sort.</param>
        public static void SortInPlace<T>(this IList<T> collection)
            where T : IComparable
        {
            collection.SortInPlace(SortAlgorithm.Quick);
        }
        /// <summary>
        /// Sorts this collection into non-decreasing order with the given algorithm.
        /// </summary>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="algorithm">The sorting algorithm to use.</param>
        public static void SortInPlace<T>(this IList<T> collection, SortAlgorithm algorithm)
            where T : IComparable
        {
            switch (algorithm)
            {
                case SortAlgorithm.Bubble:
                    BubbleSort(ref collection); break;
                case SortAlgorithm.Insertion:
                    InsertionSort(ref collection); break;
                case SortAlgorithm.Selection:
                    SelectionSort(ref collection); break;
                case SortAlgorithm.Heap:
                    HeapSort(ref collection); break;
                case SortAlgorithm.Quick:
                    QuickSort(ref collection); break;
                case SortAlgorithm.Merge:
                    MergeSort(ref collection); break;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
        #region Sort Implementations
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
        private static void HeapSort<T>(ref IList<T> collection)
            where T : IComparable
        {
            int size = collection.Count;

            // Step backwards to create the initial max-heap
            // by sorting out parent-child-child groups.
            for (int i = size / 2 - 1; i >= 0; i--)
                HeapifyRecursive(size, i, ref collection);

            // One by one extract an element from heap
            for (int i = size - 1; i > 0; i--)
            {
                // Move current root to end
                collection.Swap(0, i);

                // call max heapify on the reduced heap
                HeapifyRecursive(i, 0, ref collection);
            }
        }
        private static void HeapifyRecursive<T>(int size, int rootIndex, ref IList<T> collection)
            where T : IComparable
        {
            // Get the indices of the two children
            // by the definition of a heap.
            int leftIndex = 2 * rootIndex + 1;
            int rightIndex = 2 * rootIndex + 2;
            // Find the largest value between the root
            // and its two children.
            int largestIndex = rootIndex;
            // Ensure that children are within the collection
            // and not truncated branches on the tree end.
            if (leftIndex < size)
                if (collection[leftIndex].CompareTo(collection[largestIndex]) > 0)
                    largestIndex = leftIndex;
            if (rightIndex < size)
                if (collection[rightIndex].CompareTo(collection[largestIndex]) > 0)
                    largestIndex = rightIndex;
            // Fix a misordered heap relationship if necassary.
            if (largestIndex != rootIndex)
            {
                // Ensure that the parent node is greater
                // than or equal to both its children.
                collection.Swap(rootIndex, largestIndex);
                // Fix misordered heaps that this may have caused.
                HeapifyRecursive(size, largestIndex, ref collection);
            }
        }
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
        #endregion
    }
}
