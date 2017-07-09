#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Collections;
using Loom.Dynamic;

#endregion

namespace Loom
{
    public static class ListExtensions
    {
        public static List<T> GetRandom<T>(this List<T> items, int maximumItems, Predicate<T> filter = null)
        {
            return GetRandomPrivate(items, maximumItems, filter, null);
        }

        public static List<T> GetRandomDistinct<T>(this List<T> items, int maximumItems, string distinctProperty)
        {
            return GetRandomPrivate(items, maximumItems, null, distinctProperty);
        }

        public static List<T> GetRandomDistinct<T>(this List<T> items, int maximumItems, string distinctProperty, Predicate<T> predicate)
        {
            return GetRandomPrivate(items, maximumItems, predicate, distinctProperty);
        }

        /// <summary>
        ///     Sorts the items in the <see cref="List{T}" /> by the specified
        ///     <paramref name="propertyName" /> and <paramref name="direction" />.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="items">The list to sort.</param>
        /// <param name="propertyName">
        ///     A <see cref="string" /> representing the property by which to
        ///     sort.
        /// </param>
        /// <param name="direction">
        ///     A <see cref="SortDirection" /> representing the direction of the sort.
        ///     The default is <see cref="SortDirection.Ascending" />
        /// </param>
        /// <returns>
        ///     The <see cref="MultiLevelComparer{T}" /> used to perform the sort.
        /// </returns>
        public static MultiLevelComparer<T> SortBy<T>(this List<T> items, string propertyName, SortDirection direction = SortDirection.Ascending)
        {
            Argument.Assert.IsNotNull(items, nameof(items));
            Argument.Assert.IsNotNullOrEmpty(propertyName, nameof(propertyName));

            return SortByPrivate(items, propertyName, direction);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="propertyName1"></param>
        /// <param name="propertyName2"></param>
        /// <returns>
        ///     The <see cref="MultiLevelComparer{T}" /> used to perform the sort.
        /// </returns>
        public static MultiLevelComparer<T> SortBy<T>(this List<T> items, string propertyName1, string propertyName2)
        {
            Argument.Assert.IsNotNull(items, nameof(items));
            Argument.Assert.IsNotNullOrEmpty(propertyName1, nameof(propertyName1));
            Argument.Assert.IsNotNullOrEmpty(propertyName2, nameof(propertyName2));

            return SortByPrivate(items, propertyName1, propertyName2);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="propertyName1"></param>
        /// <param name="propertyName2"></param>
        /// <param name="propertyName3"></param>
        /// <returns>
        ///     The <see cref="MultiLevelComparer{T}" /> used to perform the sort.
        /// </returns>
        public static MultiLevelComparer<T> SortBy<T>(this List<T> items, string propertyName1, string propertyName2, string propertyName3)
        {
            Argument.Assert.IsNotNull(items, nameof(items));
            Argument.Assert.IsNotNullOrEmpty(propertyName1, nameof(propertyName1));
            Argument.Assert.IsNotNullOrEmpty(propertyName2, nameof(propertyName2));
            Argument.Assert.IsNotNullOrEmpty(propertyName3, nameof(propertyName3));

            return SortByPrivate(items, propertyName1, propertyName2, propertyName3);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="propertyNames"></param>
        /// <returns>
        ///     The <see cref="MultiLevelComparer{T}" /> used to perform the sort.
        /// </returns>
        public static MultiLevelComparer<T> SortBy<T>(this List<T> items, params string[] propertyNames)
        {
            Argument.Assert.IsNotNull(items, nameof(items));
            Argument.Assert.IsNotNullOrEmpty(propertyNames, nameof(propertyNames));

            return SortByPrivate(items, propertyNames);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> CirclePaginate<T>(this IList<T> list, int startIndex, int count, Func<T, bool> predicate = null)
        {
            if (startIndex < 0)
            {
                int rem;
                Math.DivRem(startIndex, list.Count, out rem);
                startIndex = list.Count - Math.Abs(rem);
            }

            return count < 0 ? EnumerateBackwardFromElement(list, startIndex, count, predicate) : EnumerateForwardFromElement(list, startIndex, count, predicate);
        }

        private static IEnumerable<T> EnumerateBackwardFromElement<T>(this IList<T> list, int index, int count, Func<T, bool> predicate = null)
        {
            count = Math.Abs(count);

            for (int i = index; i >= 0; --i)
            {
                T element = list[i];
                if (predicate != null && !predicate(element))
                    continue;

                yield return list[i];

                count--;
                if (count == 0)
                    break;
            }

            while (count > 0)
                for (int i = list.Count - 1; i >= 0; --i)
                {
                    T element = list[i];
                    if (predicate != null && !predicate(element))
                        continue;

                    yield return list[i];

                    count--;
                    if (count == 0)
                        break;
                }
        }

        private static IEnumerable<T> EnumerateForwardFromElement<T>(this IList<T> list, int index, int count, Func<T, bool> predicate = null)
        {
            for (int i = index; i < list.Count; ++i)
            {
                T element = list[i];
                if (predicate != null && !predicate(element))
                    continue;

                yield return element;

                count--;
                if (count == 0)
                    break;
            }

            while (count > 0)
                for (int i = 0; i < list.Count; ++i)
                {
                    T element = list[i];
                    if (predicate != null && !predicate(element))
                        continue;

                    yield return element;

                    count--;
                    if (count == 0)
                        break;
                }
        }

        private static List<T> GetRandomPrivate<T>(this List<T> items, int maximumItems, Predicate<T> predicate, string distinctProperty)
        {
            Argument.Assert.IsNotNull(items, nameof(items));

            if (maximumItems <= 0)
                throw new ArgumentOutOfRangeException(nameof(maximumItems), maximumItems, "The count must be greater than zero and less than or equal to the number of items in the list that match the predicate.");

            List<T> source = new List<T>(predicate != null ? items.FindAll(predicate) : items);

            if (source.Count <= maximumItems)
                maximumItems = source.Count;

            List<T> selected = new List<T>();

            Comparison<T> comparison = null;
            if (!Compare.IsNullOrEmpty(distinctProperty))
                comparison = DynamicType<T>.CreateComparison(distinctProperty);

            int seed = (int) (DateTime.Now.Ticks / maximumItems) * 9;
            Random random = new Random(seed);
            for (int i = 0; i < maximumItems; i++)
            {
                int nextIndex = random.Next(0, source.Count);

                T nextItem = source[nextIndex];

                if (comparison != null)
                {
                    bool dupe = false;
                    for (int j = 0; j < selected.Count; j++)
                    {
                        if (comparison(nextItem, selected[j]) != 0)
                            continue;

                        dupe = true;
                        break;
                    }
                    if (dupe)
                    {
                        i--;
                        continue;
                    }
                }

                selected.Add(nextItem);
                source.RemoveAt(nextIndex);
            }

            return selected;
        }

        private static MultiLevelComparer<T> SortByPrivate<T>(List<T> items, string propertyName, SortDirection direction)
        {
            MultiLevelComparer<T> comparer = new MultiLevelComparer<T>(propertyName, direction);
            items.Sort(comparer);
            return comparer;
        }

        private static MultiLevelComparer<T> SortByPrivate<T>(List<T> items, string propertyName1, string propertyName2)
        {
            MultiLevelComparer<T> comparer = new MultiLevelComparer<T>(propertyName1, propertyName2);
            items.Sort(comparer);
            return comparer;
        }

        private static MultiLevelComparer<T> SortByPrivate<T>(List<T> items, string propertyName1, string propertyName2, string propertyName3)
        {
            MultiLevelComparer<T> comparer = new MultiLevelComparer<T>(propertyName1, propertyName2, propertyName3);
            items.Sort(comparer);
            return comparer;
        }

        private static MultiLevelComparer<T> SortByPrivate<T>(List<T> items, string[] propertyNames)
        {
            MultiLevelComparer<T> comparer = new MultiLevelComparer<T>(propertyNames);
            items.Sort(comparer);
            return comparer;
        }
    }
}