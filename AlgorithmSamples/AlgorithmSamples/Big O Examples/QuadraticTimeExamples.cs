using System;
using System.Collections.Generic;

namespace Samples.Big_O_Examples
{
    public static class QuadraticTimeExamples
    {
        /*
            Precondition:
                a is an array of natural numbers
            Postcondition:
                must return a two dimensional array where
                    [x, y] is equal to a[x] * a[y]
                    indices exist for every index of a on both x and y dimensions
        */
        public static double[,] GetProductsTable(double[] a)
        {
            double[,] table = new double[a.Length, a.Length];
            for (int x = 0; x < a.Length; x++)
                for (int y = 0; y < a.Length; y++)
                    table[x, y] = a[x] * a[y];
            return table;
        }
    }
}
