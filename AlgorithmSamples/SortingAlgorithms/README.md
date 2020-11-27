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
<details>
<summary>Insertion Sort</summary>

## Insertion Sort
Insertion sort improves on bubble sort slightly by ensuring each element is
sorted as it steps through the array once. It does not need to run an additional
loop to ensure everything is sorted. Insertion sort iterates through the collection
moving each element back until it sees a lesser or equal element. Since this
operation runs forwards through the array we can always be sure that the first comparison
we make to a lesser or equal element means all preceding elements are also lesser or equal
to the current one (this lets us avoid some redundant comparisons). In the worst case
though insertion sort can still run in O(n<sup>2</sup>) time. The upside is that it is very fast
on smaller collections compared to the more sophisticated sorting algorithms. On smaller
collections it is much more likely to run closer to O(n) time.

Here is the pseudocode from [Wikipedia](https://en.wikipedia.org/wiki/Insertion_sort):
```
i ← 1
while i < length(A)
    j ← i
    while j > 0 and A[j-1] > A[j]
        swap A[j] and A[j-1]
        j ← j - 1
    end while
    i ← i + 1
end while
```

### My Implementation

```cs
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
```
</details>
<details>
<summary>Selection Sort</summary>

## Selection Sort
Selection sort is similar to insertion sort but is notably less efficient in the average case.
The average case typically runs around the worst case O(n<sup>2</sup>) time. Selection sort works
by looking at a shrinking subsection of the array and finding the minimum value in said section. Once
it checks every element and finds the minimum it pushes it to the front of the section and shrinks
the section by one element. The upside to selection sort is that it requires the minimum number of swaps
against the collection. Unlike insertion sort that has to "bubble" items forwards by performing several
swaps.

Here is the pseudocode from [Wikipedia](https://en.wikipedia.org/wiki/Selection_sort):
```
arr[] = 64 25 12 22 11

// Find the minimum element in arr[0...4]
// and place it at beginning
11 25 12 22 64

// Find the minimum element in arr[1...4]
// and place it at beginning of arr[1...4]
11 12 25 22 64

// Find the minimum element in arr[2...4]
// and place it at beginning of arr[2...4]
11 12 22 25 64

// Find the minimum element in arr[3...4]
// and place it at beginning of arr[3...4]
11 12 22 25 64 
```

### My Implementation

```cs
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
```
</details>
