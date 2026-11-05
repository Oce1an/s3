using System;
using System.Collections.Generic;
using Lab4.Entities;

namespace Lab4.Comparers
{
    public class MyCustomComparer<T> : IComparer<T> where T : Client
    {
        public int Compare(T? x, T? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}