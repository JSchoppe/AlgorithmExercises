# Searching Algorithms
Searching algorithms are more straightforward then sorting algorithms. When nothing
is known about the incoming data; typically you must use linear search. If you can
be certain about the order or distribution of the given data there are a few shortcuts
that you can take (seen here in binary search and interpolation search).

To see these algorithms in action; run the console application under this project
which will use each method described below to search 70,000+ objects on your machine.
The results on my machine were (milliseconds):

Linear Search: 1

Binary Search: 0

Interpolation Search: 115

So, what's up with interpolation search? Why did it perform so poorly when it
is supposed to be faster? This is an example where our results have been distorted
by the insignificant performance factors. Namely the floating point math required
to calculate the interpolant in interpolation search adds a lot of overhead compared
to the very simple integer operations used in linear and binary search. We would
suspect to see better results from interpolation in a massive collection or with
data types where value computation is an expensive operation.

# Algorithm Specifics
<details>
<summary>Linear Search</summary>

## Linear Search
Linear search is the most obvious searching algorithm. Simply checking element by element
using an equality comparison until the value is found or until the array end is reached.
Assuming there is only one instance of the target element, linear search runs in O(n)
time. Given the randomness of the target location the search time follows a normal distribution
of likely exit times between 0n and 1n times, with the average performance being O(n/2).

Here is the pseudocode from [TutorialsPoint](https://www.tutorialspoint.com/data_structures_algorithms/linear_search_algorithm.htm):
```
procedure linear_search (list, value)
   for each item in the list
      if match item == value
         return the item's location
      end if
   end for
end procedure
```

### My Implementation

```cs
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
```
</details>
<details>
<summary>Binary Search</summary>

## Binary Search
Binary search is much faster than linear search but has the additional stipulation that the
data must be sorted in non-descending order. This is an easy choice if you know the data has
been sorted elsewhere. If not then whether you should sort to do a binary search is a more
complicated choice that depends on how many times you will access the array and how often
the data will change (require resorting). Binary sort is NOT faster than linear sort if
it is a single case basis. The numerous comparisons required to sort will always outweigh
the single comparison per item required to perform linear search. That being said, if the data
is sorted than you can use binary search to speed up your search from O(n) time to O(logn) times.
Binary search works by taking advantage of the sorted order. It plays an optimized high-low game
where each comparison it makes cuts the search size in half.

Here is the pseudocode from [TutorialsPoint](https://www.tutorialspoint.com/data_structures_algorithms/binary_search_algorithm.htm):
```
procedure binary_search
   A ← sorted array
   n ← size of array
   x ← value to be searched

   Set lowerBound = 1
   Set upperBound = n
   while x not found
      if upperBound < lowerBound 
         EXIT: x does not exists.
      set midPoint = lowerBound + ( upperBound - lowerBound ) / 2
      if A[midPoint] < x
         set lowerBound = midPoint + 1
      if A[midPoint] > x
         set upperBound = midPoint - 1 
      if A[midPoint] = x 
         EXIT: x found at location midPoint
   end while
end procedure
```

### My Implementation

```cs
// This approach assumes the collection is sorted ahead of time.
private static int BinarySearch<T>(IList<T> collection, T value)
    where T : IComparable
{
    // Check the input collection to avoid
    // out of range exceptions.
    if (collection.Count == 0)
        return -1;
    // Place search brackets at each end of the collection.
    int leftIndex = 0;
    int rightIndex = collection.Count - 1;
    // Split the bracket range in half repeatedly
    // to find the value using an optimized high-low game.
    int middleIndex, comparisonValue;
    while (leftIndex != rightIndex)
    {
        middleIndex = (leftIndex + rightIndex) / 2;
        // Check to see if we found the value,
        // otherwise we move one of the brackets in.
        comparisonValue = collection[middleIndex].CompareTo(value);
        if (comparisonValue > 0)
            rightIndex = middleIndex;
        else if (comparisonValue < 0)
            leftIndex = middleIndex;
        else
            return middleIndex;
    }
    // If we didn't find the value, return a sentinel.
    // This search may fail if the collection isn't sorted.
    return -1;
}
```
</details>
<details>
<summary>Interpolation Search</summary>

## Interpolation Search
Interpolation search has even more specific requirements than binary search. Not only must the data be sorted
but it must also follow an even value distribution. While it is rare you will come across this type of data,
if you can notice its existence and implement interpolation search you can improve from O(logn) to O(loglogn) times.
Only use this algorithm if you are certain that the conditions of your data ensures that it is evenly distributed.
If applied without even distribution this search has a chance to fail by stepping over the target value. This
can be seen in the included program (the data set is not perfectly evenly distributed), so if you play around
with the number of elements being searched through you will find scenarios in which this search fails entirely.
Interpolation search works by looking at two values in an array and based on their relative location to the target
value, the algorithm will attempt to slice the midpoint (from binary search) right on top of where it suspects
the target value to be.

Here is the pseudocode from [TutorialsPoint](https://www.tutorialspoint.com/data_structures_algorithms/interpolation_search_algorithm.htm):
```
A → Array list
N → Size of A
X → Target Value

Procedure Interpolation_Search()
   Set Lo  →  0
   Set Mid → -1
   Set Hi  →  N-1
   While X does not match
      if Lo equals to Hi OR A[Lo] equals to A[Hi]
         EXIT: Failure, Target not found
      end if
      Set Mid = Lo + ((Hi - Lo) / (A[Hi] - A[Lo])) * (X - A[Lo]) 
      if A[Mid] = X
         EXIT: Success, Target found at Mid
      else 
         if A[Mid] < X
            Set Lo to Mid+1
         else if A[Mid] > X
            Set Hi to Mid-1
         end if
      end if
   End While
End Procedure
```

### My Implementation

```cs
// This approach assumes the collection is sorted ahead of time,
// and that the distribution of values is even.
private static int InterpolationSearch(IList<int> collection, int value)
{
    // Check the input collection to avoid
    // out of range exceptions.
    if (collection.Count == 0)
        return -1;
    // Place search brackets at each end of the collection.
    int leftIndex = 0;
    int rightIndex = collection.Count - 1;
    // Use interpolation to rapidly close in the brackets
    // onto the target value.
    int middleIndex, comparisonValue;
    while (leftIndex != rightIndex)
    {
        // Check to see if the interpolation has stepped
        // over the range we would suspect the value to be in.
        if (collection[leftIndex] > value
            || collection[rightIndex] < value)
        {
            return -1;
        }
        // Use interpolation to determine a new middle index target.
        // The first two lines figure out the interpolant between
        // the values at the brackets. The final line applies this
        // back into the indices.
        middleIndex = (int)((float)
            (value - collection[leftIndex]) /
            (collection[rightIndex] - collection[leftIndex])
            * (rightIndex - leftIndex) + leftIndex);
        // Check to see if we found the value,
        // otherwise we move one of the brackets in.
        comparisonValue = collection[middleIndex] - value;
        if (comparisonValue > 0)
            rightIndex = middleIndex;
        else if (comparisonValue < 0)
            leftIndex = middleIndex;
        else
            return middleIndex;
    }
    // If we didn't find the value, return a sentinel.
    // This search may fail if the collection isn't sorted.
    return -1;
}
```
</details>
