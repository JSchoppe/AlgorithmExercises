using System;
using System.Collections.Generic;

namespace AlgorithmsLibrary
{
    public static class Shuffles
    {
        // Improvement: Random object is made readonly
        // to enforce that it is not to be reinitialized.
        // TODO Should be moved to a higher level in the library
        // and marked as the unmanaged PRNG.
        private static readonly Random PRNG = new Random();

        // Improvement: Instead of only working on Arrays
        // of reference type objects, these extensions are
        // made generic to work on any collection with accessors
        // with values of both reference and value types.

        /// <summary>
        /// Shuffles a collection using the Fisher-Yates method.
        /// </summary>
        /// <param name="collection">The collection to shuffle.</param>
        public static void Shuffle<T>(this IList<T> collection)
        {
            for (int i = collection.Count - 1; i > 0; i--)
                collection.Swap(i, PRNG.Next(i + 1));
        }

        // TODO Swap should be elevated in the library.

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
    }
}
