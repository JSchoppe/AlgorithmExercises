using System;
using System.Collections.Generic;

namespace SortingAlgorithms.Sorting
{
    /// <summary>
    /// Used to specify a sorting strategy when
    /// the caller is aware of the type of data they have.
    /// </summary>
    public enum SortAlgorithm : byte
    {
        Bubble, Insertion, Selection, Heap, Quick, Merge
    };

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
    }
}
