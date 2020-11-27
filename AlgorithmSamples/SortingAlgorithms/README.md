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
bubble sort commonly runs at its O(*n*<sup>2</sup>) time.

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
though insertion sort can still run in O(*n*<sup>2</sup>) time. The upside is that it is very fast
on smaller collections compared to the more sophisticated sorting algorithms. On smaller
collections it is much more likely to run closer to O(*n*) time.

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
The average case typically runs around the worst case O(*n*<sup>2</sup>) time. Selection sort works
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
<details>
<summary>Heap Sort</summary>

## Heap Sort
Heap sort is the first advanced algorithm listed here that runs in O(*n*log*n*) time.
Heap sort does this by taking advantage of the notion of a max-heap to minimize the
number of required comparisons to sort the collection. A max-heap is defined by a tree
structure with a root where each node has two children of lesser or equal value. Nodes
at the bottom of the tree may be empty due to non-power-of-two sized collections. Once
this tree has been constructed the root element can be pushed to the back of the collection
and the remaining nodes can be reconsidered. Aside from appointing a new root node, large
branches of the tree can be left untouched because we can assume if the parent node did not
change then the remaining children in that branch are still in order from the prior iteration.
This is how heap sort saves so much time on larger collections.

Here is the pseudocode from [Wikipedia](https://en.wikipedia.org/wiki/Heapsort),
it is broken into multiple chunks since the algorithm is recursive:
```
procedure heapsort(a, count) is
    input: an unordered array a of length count
 
    (Build the heap in array a so that largest value is at the root)
    heapify(a, count)

    (The following loop maintains the invariants that a[0:end] is a heap and every element
     beyond end is greater than everything before it (so a[end:count] is in sorted order))
    end ← count - 1
    while end > 0 do
        (a[0] is the root and largest value. The swap moves it in front of the sorted elements.)
        swap(a[end], a[0])
        (the heap size is reduced by one)
        end ← end - 1
        (the swap ruined the heap property, so restore it)
        siftDown(a, 0, end)
```
```
(Put elements of 'a' in heap order, in-place)
procedure heapify(a, count) is
    (start is assigned the index in 'a' of the last parent node)
    (the last element in a 0-based array is at index count-1; find the parent of that element)
    start ← iParent(count-1)
    
    while start ≥ 0 do
        (sift down the node at index 'start' to the proper place such that all nodes below
         the start index are in heap order)
        siftDown(a, start, count - 1)
        (go to the next parent node)
        start ← start - 1
    (after sifting down the root all nodes/elements are in heap order)

(Repair the heap whose root element is at index 'start', assuming the heaps rooted at its children are valid)
procedure siftDown(a, start, end) is
    root ← start

    while iLeftChild(root) ≤ end do    (While the root has at least one child)
        child ← iLeftChild(root)   (Left child of root)
        swap ← root                (Keeps track of child to swap with)

        if a[swap] < a[child] then
            swap ← child
        (If there is a right child and that child is greater)
        if child+1 ≤ end and a[swap] < a[child+1] then
            swap ← child + 1
        if swap = root then
            (The root holds the largest element. Since we assume the heaps rooted at the
             children are valid, this means that we are done.)
            return
        else
            swap(a[root], a[swap])
            root ← swap            (repeat to continue sifting down the child now)
```

### My Implementation

```cs
private static void HeapSort<T>(ref IList<T> collection)
    where T : IComparable
{
    int size = collection.Count;
    // Step backwards to create the initial max-heap
    // by sorting out parent-child-child groups.
    for (int i = size / 2 - 1; i >= 0; i--)
        HeapifyRecursive(size, i, ref collection);
    // One by one extract an element from the heap.
    for (int i = size - 1; i > 0; i--)
    {
        // Move current root to end.
        collection.Swap(0, i);
        // Max heapify the entire heap by
        // starting from the root.
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
```
</details>
<details>
<summary>Quick Sort</summary>

## Quick Sort
Quick sort is in many cases considered to be the fastest sorting algorithm.
The key performance factor of quick sort is the choice of a partition element
that subdivides the collection into two halfs to sort (recursively). Given good
partition elements, that are near the median value in their section, the algorithm
will run in O(*n*log*n*) time. Quick sort works by choosing an element as the partition
and sorting everything to the back and front of it. After this it considers each new half
of the collection and does the same thing. This constant narrowing of comparisons scope
is what makes quick sort fast, but it is strongly dependent on good partition elements.

Here is the pseudocode from [Wikipedia](https://en.wikipedia.org/wiki/Quicksort),
it is broken into multiple chunks since the algorithm is recursive:
```
algorithm quicksort(A, lo, hi) is
    if lo < hi then
        p := partition(A, lo, hi)
        quicksort(A, lo, p)
        quicksort(A, p + 1, hi)

algorithm partition(A, lo, hi) is
    pivot := A[⌊(hi + lo) / 2⌋]
    i := lo - 1
    j := hi + 1
    loop forever
        do
            i := i + 1
        while A[i] < pivot
        do
            j := j - 1
        while A[j] > pivot
        if i ≥ j then
            return j
        swap A[i] with A[j]
```

### My Implementation

```cs
private static void QuickSort<T>(ref IList<T> collection)
    where T : IComparable
{
    QuickSortRecursive(ref collection, 0, collection.Count - 1);
}
private static void QuickSortRecursive<T>(ref IList<T> collection, int startIndex, int endIndex)
    where T : IComparable
{
    // Is this subdivision of size > 1?
    // If not this recursion loop has completed.
    if (startIndex < endIndex)
    {
        // Quick sort around the partition for this set.
        int partitionIndex = QuickSortPartition(ref collection, startIndex, endIndex);
        // Recursively sort elements before the
        // partition and after the partition.
        QuickSortRecursive(ref collection, startIndex, partitionIndex - 1);
        QuickSortRecursive(ref collection, partitionIndex + 1, endIndex);
    }
}
private static int QuickSortPartition<T>(ref IList<T> collection, int startIndex, int endIndex)
    where T : IComparable
{
    // Grab the rightmost element as the pivot.
    T pivotValue = collection[endIndex];
    // Keep track of where the partition will
    // be inserted (elements to the left).
    int pivotIndex = startIndex - 1;
    // Step through this section and divide the values
    // around the partition.
    for (int i = startIndex; i < endIndex; i++)
    {
        // Is this value smaller than the pivot?
        if (collection[i].CompareTo(pivotValue) < 0)
        {
            // Push the pivot insertion point forward.
            pivotIndex++;
            // Pull the element behind the pivot.
            collection.Swap(pivotIndex, i);
        }
    }
    // Ensure the pivot is placed after all smaller elements.
    pivotIndex++;
    // Insert the properly sorted partition element.
    collection.Swap(pivotIndex, endIndex);
    return pivotIndex;
}
```
</details>
<details>
<summary>Merge Sort</summary>

## Merge Sort
Merge sort is a strong competitor to quick sort. The key difference is that
instead of relying on choosing a good partition element, merge sort gains
its performance from requiring additional memory to allocate temporary arrays
for the merging process. Similar to quick sort and heap sort, this algorithm runs
at a best case of O(*n*log*n*) time. Merge sort works by repeatedly cutting the collection
in half (recursively) until the recursion reaches the most granular comparison of element to element.
Then the recursion unwinds and at each level the two children are merged. When merging
additional memory is allocated for the children. Since each child has already been sorted we
can merge them together by repeatedly pulling the lesser element until both children are exhausted.
This repeats until the entire recursion call-stack is exhausted.

Here is the pseudocode from [Wikipedia](https://en.wikipedia.org/wiki/Merge_sort),
it is broken into multiple chunks since the algorithm is recursive:
```
function merge_sort(list m) is
    // Base case. A list of zero or one elements is sorted, by definition.
    if length of m ≤ 1 then
        return m

    // Recursive case. First, divide the list into equal-sized sublists
    // consisting of the first half and second half of the list.
    // This assumes lists start at index 0.
    var left := empty list
    var right := empty list
    for each x with index i in m do
        if i < (length of m)/2 then
            add x to left
        else
            add x to right

    // Recursively sort both sublists.
    left := merge_sort(left)
    right := merge_sort(right)

    // Then merge the now-sorted sublists.
    return merge(left, right)
```
```
function merge(left, right) is
    var result := empty list

    while left is not empty and right is not empty do
        if first(left) ≤ first(right) then
            append first(left) to result
            left := rest(left)
        else
            append first(right) to result
            right := rest(right)

    // Either left or right may have elements left; consume them.
    // (Only one of the following loops will actually be entered.)
    while left is not empty do
        append first(left) to result
        left := rest(left)
    while right is not empty do
        append first(right) to result
        right := rest(right)
    return result
```

### My Implementation

```cs
private static void MergeSort<T>(ref IList<T> collection)
    where T : IComparable
{
    MergeSortRecursive(ref collection, 0, collection.Count - 1);
}
private static void MergeSortRecursive<T>(ref IList<T> collection, int startIndex, int endIndex)
    where T : IComparable
{
    // Is this subdivision of size > 1?
    // If not this recursion loop has completed.
    if (startIndex < endIndex)
    {
        int middle = (startIndex + endIndex) / 2;
        // Continue splitting the collection in half.
        MergeSortRecursive(ref collection, startIndex, middle);
        MergeSortRecursive(ref collection, middle + 1, endIndex);
        // Combine the individual chunks at each level.
        Merge(ref collection, startIndex, middle, endIndex);
    }
}
private static void Merge<T>(ref IList<T> collection, int start, int middle, int end)
    where T : IComparable
{
    // Create arrays to temporarily store
    // the values from the merge halfs.
    T[] left = collection.Slice(start, middle);
    T[] right = collection.Slice(middle + 1, end);
    // Keep track of location in each sorted
    // half as we combine them together.
    int leftI = 0, rightI = 0;
    // Step through the section of the array
    // to assemble, pulling values from either
    // of the halfs.
    int i = start;
    while (leftI < left.Length && rightI < right.Length)
    {
        // Insert the next least value into the array.
        if (left[leftI].CompareTo(right[rightI]) < 0)
        {
            collection[i] = left[leftI];
            leftI++;
        }
        else
        {
            collection[i] = right[rightI];
            rightI++;
        }
        i++;
    }
    // Pull any remaining elements from left
    // or right halfs (in the case of uneven halfs).
    while (leftI < left.Length)
    {
        collection[i] = left[leftI];
        leftI++; i++;
    }
    while (rightI < right.Length)
    {
        collection[i] = right[rightI];
        rightI++; i++;
    }
}
```
</details>
