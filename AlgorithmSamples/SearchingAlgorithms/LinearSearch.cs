using System;
using System.Collections.Generic;

namespace SearchingAlgorithms.Searching
{
    public static partial class SearchingExtensions
    {
        private static int LinearSearch<T>(IList<T> collection, T value)
            where T : IComparable
        {
            // Check each index of the collection and
            // see if it is equal to the value.
            for (int i = 0; i < collection.Count; i++)
                if (collection[i].CompareTo(value) == 0)
                    return i;
            // Otherwise return a sentinel value.
            return -1;
        }
    }
}
