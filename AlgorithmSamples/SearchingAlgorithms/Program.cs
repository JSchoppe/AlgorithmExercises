using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using AlgorithmsLibrary;
using AlgorithmsLibrary.Sorting;
using SearchingAlgorithms.Searching;

namespace SearchingAlgorithms
{
    public sealed class Program
    {
        public static void Main()
        {
            // Retrieves a shuffled collection of data.
            int[] data = FetchData();
            // Prepare a sorted copy of the dataset,
            // this is required for Binary and Interpolation
            // search algorithms.
            int[] sortedData = data.Slice();
            sortedData.SortInPlace();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            // Run linear element by element search.
            int linearFoundIndex =
                data.FindIndexOf(31, SearchAlgorithm.Linear);
            // Record elapsed time for this strategy.
            // This will be very random based on where the
            // shuffle landed our target element.
            stopwatch.Stop();
            long linearMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            // Run binary search (requires the sorted collection).
            int binaryFoundIndex =
                sortedData.FindIndexOf(31, SearchAlgorithm.Binary);
            // Record elapsed time for this strategy. This will
            // run in O(logn) time or faster if the value is found
            // by chance on an early midpoint.
            stopwatch.Stop();
            long binaryMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            // Run interpolation search (requires the sorted collection).
            int interpolationFoundIndex =
                sortedData.InterpolateIndexOf(31);
            // Record elapsed time for this strategy. This approach runs
            // in O(loglogn) time but has very particular requirements
            // for the given data. If it is not normally distributed
            // then the algorithm may fail by stepping over the value
            // in an interpolation guess.
            stopwatch.Stop();
            long interpolationMillis = stopwatch.ElapsedMilliseconds;

            // Print the time taken for each sorting strategy.
            Console.WriteLine($"Sort Time Comparison ({data.Length} items)");
            Console.WriteLine($"Linear Search Millis:        {linearMillis}");
            Console.WriteLine($"Binary Search Millis:        {binaryMillis}");
            Console.WriteLine($"Interpolation Search Millis: {interpolationMillis}");
        }
        private static int[] FetchData()
        {
            // Quickly process input data from txt.
            // Input data is assumed clean for simplicity.
            string[] lines =
                File.ReadAllLines($"Resources{Path.DirectorySeparatorChar}sample-data.txt");
            // Convert data into integers to be sorted.
            int[] values = new int[lines.Length];
            for (int i = 0; i < values.Length; i++)
                values[i] = int.Parse(lines[i]);
            // Create a larger dataset to make the operation
            // take long enough to get comparable results.
            List<int> largerDataSet = new List<int>();
            for (int i = 0; i < 150; i++)
                largerDataSet.AddRange(values);
            // Add a unique value to seach for that does not
            // appear multiple times.
            largerDataSet.Add(31);
            // Shuffle the data.
            largerDataSet.Shuffle();
            return largerDataSet.ToArray();
        }
    }
}
