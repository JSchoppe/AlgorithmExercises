using System.Collections.Generic;

namespace AlgorithmsLibrary
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Swaps items in a collection.
        /// </summary>
        /// <param name="collection">The collection to swap items in.</param>
        /// <param name="indexA">Index of the first item to swap.</param>
        /// <param name="indexB">Index of the second item to swap.</param>
        public static void Swap<T>(this IList<T> collection, int indexA, int indexB)
        {
            // Improvement?: If the requested swap is redundant time may be
            // saved if the collection accessor is a time consuming operation.
            if (indexA != indexB)
            {
                T holder = collection[indexA];
                collection[indexA] = collection[indexB];
                collection[indexB] = holder;
            }
        }
        /// <summary>
        /// Copys a slice of the collection into a new array.
        /// </summary>
        /// <param name="collection">The collection to slice from.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The included end index.</param>
        /// <returns>The slice of the collection as an array.</returns>
        public static T[] Slice<T>(this IList<T> collection, int startIndex, int endIndex)
        {
            T[] slice = new T[endIndex - startIndex + 1];
            for (int i = startIndex; i <= endIndex; i++)
                slice[i - startIndex] = collection[i];
            return slice;
        }
        /// <summary>
        /// Copys a slice of the collection into a new array.
        /// </summary>
        /// <param name="collection">The collection to slice from.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The slice of the collection as an array.</returns>
        public static T[] Slice<T>(this IList<T> collection, int startIndex)
        {
            return collection.Slice(startIndex, collection.Count - 1);
        }
        /// <summary>
        /// Copys the entire collection into a new array.
        /// </summary>
        /// <param name="collection">The collection to copy from.</param>
        /// <returns>The collection as an array.</returns>
        public static T[] Slice<T>(this IList<T> collection)
        {
            return collection.Slice(0);
        }
    }
}
