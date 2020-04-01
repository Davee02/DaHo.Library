using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DaHo.Library.Utilities
{
    public static class EnumerableUtilities
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> GetNth<T>(this IEnumerable<T> list, int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "The n must not be smaller than 1");

            var enumerable = list.ToList();

            for (var i = 0; i < enumerable.Count; i += n)
                yield return enumerable[i];
        }

        public static IEnumerable<T> GetNth<T>(this IEnumerable<T> list, int n, T filler)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "The n must not be smaller than 1");

            var enumerable = list.ToList();

            for (var i = 0; i < enumerable.Count; i++)
            {
                if (i % n == 0)
                    yield return enumerable[i];
                else
                    yield return filler;
            }
        }
    }
}
