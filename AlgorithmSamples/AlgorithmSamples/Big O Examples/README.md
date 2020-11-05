# Examples of Big-O
Big-O notation describes the time or storage consumption of an algorithm or segment of code.
Notably, it is decoupled from the limitations of hardware and is strictly a mathematical
expression of the maximum amount of storage or time a function will take relative to
its inputs.
## Constant Time | O(1)
Constant time describes most simple functions that do not extensively use collections
or looping. A very basic example would be something like the following:
```cs
public bool ArraysMatchAtIndex<T>(IEquatable<T>[] a, IEquatable<T>[] b, int i)
{
    if(a.Length <= i || b.Length <= i){ return false; }
    return a[i] == b[i];
}
```
Since arrays have linear access time, and length is precalculated, we can always
be sure that this code will run in about the same time.
## Linear Time | O(n)
Linear time is most commonly found when iterating over a collection item by item.
The time taken is directly and linearly proportional to the size of the input. This
is shown in the following function:
```cs
public float MultiplyElements(IList<float> a)
{
    float product = 1;
    for (int i = 0; i < a.Count; i++)
        product *= a[i];
    return product;
}
```
One thing that is important to keep in mind in this example, is that the use of IList interface
instead of List or Array type can be potentially misleading to the big-o notation of this
function. IList can be implemented in such a way that access time is not constant O(1), it
is important to keep this in mind when generalizing functions.
## Quadratic Time | O(n^2)
Quadratic Time and all the other n^constant variants are heavily associated with n-dimensional
arrays or n-dimensional operations. Consider the following function, where we want to generate
a table of every product of the input set (in this case for simplicity we ignore the repeats
over the diagonal). Each number has to be multiplied by every other number which yields the
nested for loop and the exponential relationship of the function time to the input length.
```cs
public static double[,] GetProductsTable(double[] a)
{
    double[,] table = new double[a.Length, a.Length];
    for (int x = 0; x < a.Length; x++)
        for (int y = 0; y < a.Length; y++)
            table[x, y] = a[x] * a[y];
    return table;
}
```
