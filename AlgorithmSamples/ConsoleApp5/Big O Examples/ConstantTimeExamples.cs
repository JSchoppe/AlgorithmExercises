using System;
using System.Collections.Generic;

namespace Samples.Big_O_Examples
{
    public static class ConstantTimeExamples
    {
        /*
            Precondition:
                a and b are arrays of equatable objects
                i is an integer value greater than or equal to 0
            Postcondition:
                must return false if either array does not contain index i
                must return false if values/references do not match at index i of a and index i of b
                must return true if values/references match at index i of a and index i of b
        */
        public static bool ArraysMatchAtIndex<T>(IEquatable<T>[] a, IEquatable<T>[] b, int i)
        {
            // Even though we have to do some input checking, Array length
            // is precalculated which adds no overhead beyond the initial operation.
            // This algorithm could be described as O(3) since it takes three constant
            // time operations to complete. This simplified to O(1).
            if(a.Length <= i || b.Length <= i){ return false; }
            return a[i] == b[i];
        }
    }
}
