using System;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace SortingAlgorithms.Sorting
{
    public static partial class SortingExtensions
    {
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
    }
}
