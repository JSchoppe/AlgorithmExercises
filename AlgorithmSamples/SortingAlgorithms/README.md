# Sorting Algorithms
There are a handful of different approaches to the problem of sorting a single
dimensional array of comparable items. Some of the approaches are easier to understand
but do not perform well. The performant algorithms take advantage of the fact that
most collections are already sorted to some degree.

To see these algorithms in action; run the console application under this project
which will use each method described below to sort 20,000+ objects on your machine.
The results on my machine were (milliseconds):

Bubble Sort: 12200

Insertion Sort: 6695

Selection Sort: 5062

Heap Sort: 22

Quick Sort: 107

Merge Sort: 10

# Algorithm Specifics
<details>
<summary>Bubble Sort</summary>

## Bubble Sort
Bubble sort is the most obvious solution to sorting a collection. It simply
iterates over the array repeatedly swapping adjacent elements until the array
is sorted. It is easy to see why this solution is so slow; it constantly makes
repetitive comparisons it already made in previous iterations. Because of this
bubble sort commonly runs at its O(n<sup>2</sup>) time.

Here is the pseudocode from [Wikipedia](https://en.wikipedia.org/wiki/Bubble_sort):
```
procedure bubbleSort(A : list of sortable items)
    n := length(A)
    repeat
        swapped := false
        for i := 1 to n-1 inclusive do
            if A[i-1] > A[i] then
                swap(A[i-1], A[i])
                swapped := true
            end if
        end for
    until not swapped
end procedure
```

### My Implementation

```cs
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
```
</details>
