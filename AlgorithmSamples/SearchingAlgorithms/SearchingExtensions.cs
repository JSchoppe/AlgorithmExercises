using System;
using System.Collections.Generic;

namespace SearchingAlgorithms.Searching
{
    /// <summary>
    /// Used to specify a searching strategy when
    /// the caller is aware of the type of data they have.
    /// </summary>
    public enum SearchAlgorithm : byte
    {
        Linear, Binary
    };

    /// <summary>
    /// Contains extensions methods for searching collections.
    /// </summary>
    public static partial class SearchingExtensions
    {
        #region Search Exposed Method
        /// <summary>
        /// Searches the collection for the index of a value.
        /// </summary>
        /// <param name="collection">The collection to search in.</param>
        /// <param name="value">The value to find the index of.</param>
        /// <returns>The index of the found item, or -1 if not found.</returns>
        public static int FindIndexOf<T>(this IList<T> collection, T value)
            where T : IComparable
        {
            return collection.FindIndexOf(value, SearchAlgorithm.Linear);
        }
        /// <summary>
        /// Searches the collection for the index of a value with the given algorithm.
        /// </summary>
        /// <param name="collection">The collection to search in.</param>
        /// <param name="value">The value to find the index of.</param>
        /// <param name="algorithm">The searching algorithm to use.</param>
        /// <returns>The index of the found item, or -1 if not found.</returns>
        public static int FindIndexOf<T>(this IList<T> collection, T value, SearchAlgorithm algorithm)
            where T : IComparable
        {
            return algorithm switch
            {
                SearchAlgorithm.Linear => LinearSearch(collection, value),
                SearchAlgorithm.Binary => BinarySearch(collection, value),
                _ => throw new NotImplementedException(),
            };
        }
        /// <summary>
        /// Searches the collection for the index of a value using interpolation.
        /// </summary>
        /// <param name="collection">The collection to search in.</param>
        /// <param name="value">The value to find the index of.</param>
        /// <returns>The index of the found item, or -1 if not found.</returns>
        public static int InterpolateIndexOf(this IList<int> collection, int value)
        {
            return InterpolationSearch(collection, value);
        }
        #endregion
    }
}
