#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    public static class Search
    {
        public static T BreadthFirst<T>(T parent, Predicate<T> filter, Func<T, IEnumerable> getChildren)
        {
            Argument.Assert.IsNotNull(parent, nameof(parent));
            Argument.Assert.IsNotNull(filter, nameof(filter));
            Argument.Assert.IsNotNull(getChildren, nameof(getChildren));

            return RunSearch(parent, filter, getChildren, new BreadthFirstSearch<T>());
        }

        public static T DepthFirst<T>(T parent, Predicate<T> filter, Func<T, IEnumerable> getChildren)
        {
            Argument.Assert.IsNotNull(parent, nameof(parent));
            Argument.Assert.IsNotNull(filter, nameof(filter));
            Argument.Assert.IsNotNull(getChildren, nameof(getChildren));

            return RunSearch(parent, filter, getChildren, new DepthFirstSearch<T>());
        }

        private static T RunSearch<T>(T parent, Predicate<T> filter, Func<T, IEnumerable> getChildren, ISearchable<T> search)
        {
            IEnumerable children = getChildren(parent);
            foreach (T child in children)
                search.Add(child);

            while (search.Continue)
            {
                T current = search.Next();

                if (filter(current))
                    return current;

                children = getChildren(current);
                foreach (T child in children)
                    search.Add(child);
            }

            return default(T);
        }

        #region Nested type: BreadthFirstSearch

        private class BreadthFirstSearch<T> : ISearchable<T>
        {
            private readonly Queue<T> queue = new Queue<T>();

            #region ISearchable<T> Members

            public T Next()
            {
                return queue.Dequeue();
            }

            public void Add(T item)
            {
                queue.Enqueue(item);
            }

            public bool Continue => queue.Count > 0;

            #endregion
        }

        #endregion

        #region Nested type: DepthFirstSearch

        private class DepthFirstSearch<T> : ISearchable<T>
        {
            private readonly Stack<T> stack = new Stack<T>();

            #region ISearchable<T> Members

            public T Next()
            {
                return stack.Pop();
            }

            public void Add(T item)
            {
                stack.Push(item);
            }

            public bool Continue => stack.Count > 0;

            #endregion
        }

        #endregion

        #region Nested type: ISearchable

        private interface ISearchable<T>
        {
            bool Continue { get; }

            void Add(T item);
            T Next();
        }

        #endregion
    }
}