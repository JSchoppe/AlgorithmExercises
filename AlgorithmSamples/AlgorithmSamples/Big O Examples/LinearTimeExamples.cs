using System;
using System.Collections.Generic;

namespace Samples.Big_O_Examples
{
    public static class LinearTimeExamples
    {
        /*
            Precondition:
                a is a list collection of numerical values
                a has a size of at least one element
            Postcondition:
                must return the big product evaluation of all terms in a
        */
        public static float MultiplyElements(IList<float> a)
        {
            // Each additional element in a results in one additional
            // multiplication-assignment operation. This algorithm
            // runs in O(n) linear time.
            float product = 1;
            for (int i = 0; i < a.Count; i++)
                product *= a[i];
            return product;
        }
    }
}
