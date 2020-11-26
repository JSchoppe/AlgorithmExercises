using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using SortingAlgorithms.Sorting;
using AlgorithmsLibrary;

namespace SortingAlgorithms
{
    public sealed class Program
    {
        public static void Main()
        {
            // Retrieve some data to operate on.
            int[] unsortedValues = FetchData();
            // Clone the data so that in place operations
            // can be used for each method.
            int[] bubbleValues = unsortedValues.Slice();
            int[] insertionValues = unsortedValues.Slice();
            int[] selectionValues = unsortedValues.Slice();
            int[] heapValues = unsortedValues.Slice();
            int[] quickValues = unsortedValues.Slice();
            int[] mergeValues = unsortedValues.Slice();
            // Run each sorting algorithm.
            Stopwatch stopwatch = new Stopwatch();
            #region Run Sorting Test Against Data
            stopwatch.Restart();
            bubbleValues.SortInPlace(SortAlgorithm.Bubble);
            stopwatch.Stop();
            long bubbleMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            insertionValues.SortInPlace(SortAlgorithm.Insertion);
            stopwatch.Stop();
            long insertionMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            selectionValues.SortInPlace(SortAlgorithm.Selection);
            stopwatch.Stop();
            long selectionMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            heapValues.SortInPlace(SortAlgorithm.Heap);
            stopwatch.Stop();
            long heapMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            quickValues.SortInPlace(SortAlgorithm.Quick);
            stopwatch.Stop();
            long quickMillis = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            mergeValues.SortInPlace(SortAlgorithm.Merge);
            stopwatch.Stop();
            long mergeMillis = stopwatch.ElapsedMilliseconds;
            #endregion
            // Print the time taken for each sorting strategy.
            Console.WriteLine($"Sort Time Comparison ({unsortedValues.Length} items)");
            Console.WriteLine($"Bubble Sort Millis:    {bubbleMillis}");
            Console.WriteLine($"Insertion Sort Millis: {insertionMillis}");
            Console.WriteLine($"Selection Sort Millis: {selectionMillis}");
            Console.WriteLine($"Heap Sort Millis:      {heapMillis}");
            Console.WriteLine($"Quick Sort Millis:     {quickMillis}");
            Console.WriteLine($"Merge Sort Millis:     {mergeMillis}");
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
            // Duplicate the data so it takes longer to sort
            // so that output time taken is more precise.
            List<int> largeDataset = new List<int>();
            for (int i = 0; i < 50; i++)
                largeDataset.AddRange(values);
            // Shuffle the data.
            largeDataset.Shuffle();
            return largeDataset.ToArray();
        }
    }
}
