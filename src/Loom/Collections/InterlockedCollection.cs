#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

namespace Loom.Collections
{
    public class InterlockedCollection<T> : IEnumerable<T> where T : class
    {
        private List<T> cache = new List<T>();

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return cache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) cache).GetEnumerator();
        }

        #endregion

        public bool Add(T item)
        {
            bool added = false;
            List<T> clone = new List<T>(cache);
            if (!clone.Contains(item))
            {
                clone.Add(item);
                added = true;
            }

            Interlocked.Exchange(ref cache, clone);
            return added;
        }

        public bool Insert(int index, T item)
        {
            bool inserted = false;
            List<T> clone = new List<T>(cache);
            if (!clone.Contains(item))
            {
                clone.Insert(index, item);
                inserted = true;
            }

            Interlocked.Exchange(ref cache, clone);
            return inserted;
        }

        public bool Remove(T item)
        {
            bool removed = false;
            List<T> clone = new List<T>(cache);
            if (clone.Contains(item))
            {
                clone.Remove(item);
                removed = true;
            }

            Interlocked.Exchange(ref cache, clone);
            return removed;
        }

        public IEnumerable<T> Remove(Func<T, bool> predicate)
        {
            IEnumerable<T> items = FindAll(predicate);
            IList<T> removeAll = items as IList<T> ?? items.ToList();
            foreach (T item in removeAll)
                Remove(item);
            return removeAll;
        }

        public void Clear()
        {
            List<T> clone = new List<T>();
            Interlocked.Exchange(ref cache, clone);
        }

        public int FindIndex(Predicate<T> predicate)
        {
            return cache.FindIndex(predicate);
        }

        public int FindLastIndex(Predicate<T> predicate)
        {
            return cache.FindLastIndex(predicate);
        }

        public T FindLast(Predicate<T> predicate)
        {
            return cache.FindLast(predicate);
        }

        public T Find(Func<T, bool> predicate)
        {
            return cache.AsParallel().FirstOrDefault(predicate);
        }

        public IEnumerable<T> FindAll(Func<T, bool> predicate)
        {
            return cache.AsParallel().Where(predicate).ToArray();
        }
    }
}