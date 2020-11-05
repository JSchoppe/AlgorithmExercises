# Algorithms Library
Algorithms are most useful when generalized as much as possible. This increases the
degree to which they can be composed and reused. This is facilitated in C# land via
extension methods and interfaces.
```cs
public static void Swap<T>(this IList<T> collection, int indexA, int indexB)
{
    if (indexA != indexB)
    {
        T holder = collection[indexA];
        collection[indexA] = collection[indexB];
        collection[indexB] = holder;
    }
}
```
The algorithms in this project are exposed through a library that can be reused between projects.
## Fisher Yates Shuffle
This shuffle algorithm is a common goto for simplistic shuffling. Every shuffle outcome
is equally likely and it is simple to implement:
```cs
private static readonly Random PRNG = new Random();
...
public static void Shuffle<T>(this IList<T> collection)
{
    for (int i = collection.Count - 1; i > 0; i--)
        collection.Swap(i, PRNG.Next(i + 1));
}
```
The algorithm runs in O(n) linear time since each index requires one swap operation.
It produces every possible outcome equally because at each step it is choosing any of
the remaining elements with equal likelyhood. After each step it shrinks to focus on
a smaller segment of the collection, thus locking in the previous element. 
