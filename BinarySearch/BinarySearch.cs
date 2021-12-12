using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public static class BinarySearch
    {
        /// <summary>
        /// Searches element in source array and returns it's index or -1 if element was not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source array for searching in.</param>
        /// <param name="element">searching element.</param>
        /// <param name="comparer">Comparator for type.</param>
        /// <param name="index">index for using recursion.</param>
        /// <returns>index of element or -1 if element was not found</returns>
        public static int Search<T>(T[] source, T element, IComparer<T> comparer, int index = 0)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (source.Length < 1)
            {
                return -1;
            }

            int compareResult = comparer.Compare(source[source.Length / 2], element);

            if (compareResult == 0)
            {
                return source.Length / 2 + index;
            }

            if (compareResult > 0)
            {
                return Search(source[..(source.Length / 2)], element, comparer, index);
            }
            else
            {
                return Search(source[(source.Length / 2 + 1)..], element, comparer, source.Length / 2 + index + 1);
            }
        }
    }
}
