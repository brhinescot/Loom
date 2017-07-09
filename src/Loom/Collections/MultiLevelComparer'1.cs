#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using Loom.Dynamic;

#endregion

namespace Loom.Collections
{
    /// <summary>
    ///     Represents an <see cref="IComparer{T}" /> class used to sort collections.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The <see cref="MultiLevelComparer{T}" /> can operate on public properties or fields.
    ///     </para>
    /// </remarks>
    /// <example>
    ///     The following example demonstrates how to use the <see cref="MultiLevelComparer{T}" />.
    ///     The example assumes a class named User with at least a Name and Level property and a constructor
    ///     that initializes them.
    ///     <code>
    ///  private static void Main()
    ///  {
    ///      List&lt;User&gt; users = new List&lt;User&gt;();
    ///      users.Add(new User("Sue", 1);
    ///      users.Add(new User("Bob", 1);
    ///      users.Add(new User("Dave", 2);
    ///      users.Add(new User("Ed", 2);
    ///  
    ///      SortUsers(users);
    ///  }
    ///  
    ///  private static void SortUsers(IEnumerable&lt;User&gt; users)
    ///  {
    /// 	    MultiLevelComparer&lt;User&gt; userSorter = new MultiLevelComparer&lt;User&gt;();
    /// 	    userSorter.AddColumn("Level", SortDirection.Descending);
    /// 	    userSorter.AddColumn("Name", SortDirection.Ascending);
    /// 	    users.Sort(userSorter);
    ///  
    ///      foreach(User user in users)
    ///      {
    ///          Console.WriteLine(user.Level + " " + user.Name);
    ///      }
    ///      
    ///      /* Output:
    ///       * 
    ///       * 2 Dave
    ///       * 2 Ed
    ///       * 1 Bob
    ///       * 1 Sue
    ///       */
    /// 	}
    ///  </code>
    /// </example>
    public sealed class MultiLevelComparer<T> : IComparer<T>, IComparer
    {
        private readonly List<SortColumn> sortColumns = new List<SortColumn>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiLevelComparer{T}" /> class.
        /// </summary>
        public MultiLevelComparer() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiLevelComparer{T}" /> class.
        /// </summary>
        /// <param name="propertyName1">The sort column representing a public property.</param>
        /// <param name="propertyName2">The sort column representing a public property.</param>
        public MultiLevelComparer(string propertyName1, string propertyName2)
        {
            AddColumn(propertyName1);
            AddColumn(propertyName2);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiLevelComparer{T}" /> class.
        /// </summary>
        /// <param name="propertyName1">The sort column representing a public property.</param>
        /// <param name="propertyName2">The sort column representing a public property.</param>
        /// <param name="propertyName3">The sort column representing a public property.</param>
        public MultiLevelComparer(string propertyName1, string propertyName2, string propertyName3)
        {
            AddColumn(propertyName1);
            AddColumn(propertyName2);
            AddColumn(propertyName3);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiLevelComparer{T}" /> class.
        /// </summary>
        /// <param name="propertyNames">The sort columns representing public properties.</param>
        public MultiLevelComparer(params string[] propertyNames)
        {
            for (int i = 0; i < propertyNames.Length; i++)
                AddColumn(propertyNames[i]);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiLevelComparer{T}" /> class.
        /// </summary>
        /// <param name="propertyName">The sort column representing a public property.</param>
        /// <param name="direction">The direction of the sort.</param>
        public MultiLevelComparer(string propertyName, SortDirection direction = SortDirection.Ascending)
        {
            AddColumn(propertyName, direction);
        }

        public bool TopLevelOnly { get; set; }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return -1;

            return Compare((T) x, (T) y);
        }

        #endregion

        #region IComparer<T> Members

        /// <summary>
        ///     Compares two objects and returns a value indicating whether one
        ///     is less than, equal to, or greater than the other.
        /// </summary>
        /// <returns>
        ///     A value less than zero indicates x is less than y. Zero equals y.
        ///     Greater than zero is greater than y.
        /// </returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public int Compare(T x, T y)
        {
            return sortColumns.Count == 0 ? 0 : Compare(0, x, y);
        }

        #endregion

        private int Compare(int sortLevel, T x, T y)
        {
            if (sortColumns.Count - 1 < sortLevel)
                return 0;

            int result = 0;

            SortColumn column = sortColumns[sortLevel];
            switch (column.Direction)
            {
                case SortDirection.Ascending:
                    result = column.Comparer(x, y);
                    break;
                case SortDirection.Descending:
                    result = column.Comparer(y, x);
                    break;
            }

            if (result == 0 && !TopLevelOnly)
                result = Compare(sortLevel + 1, x, y);

            return result;
        }

        /// <summary>
        ///     Adds a new column to the sort.
        /// </summary>
        /// <param name="propertyName1">A sort column representing a public property.</param>
        /// <param name="propertyName2">A sort column representing a public property.</param>
        public void AddColumn(string propertyName1, string propertyName2)
        {
            AddColumn(propertyName1);
            AddColumn(propertyName2);
        }

        /// <summary>
        ///     Adds a new column to the sort.
        /// </summary>
        /// <param name="propertyName1">A sort column representing a public property.</param>
        /// <param name="propertyName2">A sort column representing a public property.</param>
        /// <param name="propertyName3">A sort column representing a public property.</param>
        public void AddColumn(string propertyName1, string propertyName2, string propertyName3)
        {
            AddColumn(propertyName1);
            AddColumn(propertyName2);
            AddColumn(propertyName3);
        }

        /// <summary>
        ///     Adds a new column to the sort.
        /// </summary>
        /// <param name="propertyNames">The sort columns representing public properties.</param>
        public void AddColumn(params string[] propertyNames)
        {
            for (int i = 0; i < propertyNames.Length; i++)
                AddColumn(propertyNames[i]);
        }

        /// <summary>
        ///     Adds a new column to the sort using the specified sort direction.
        /// </summary>
        /// <param name="propertyName">The sort column representing a public property.</param>
        /// <param name="direction">The direction of the sort.</param>
        public void AddColumn(string propertyName, SortDirection direction = SortDirection.Ascending)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));
            sortColumns.Add(new SortColumn(DynamicType<T>.CreateComparison(propertyName), direction));
        }

        /// <summary>
        ///     Removes all columns from the sort.
        /// </summary>
        public void ClearColumns()
        {
            sortColumns.Clear();
        }

        #region Nested type: SortColumn

        /// <summary>
        ///     Represents a sort column and the direction to sort.
        /// </summary>
        private class SortColumn
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="SortColumn" /> class.
            /// </summary>
            /// <param name="direction">The direction to sort.</param>
            /// <param name="comparer"></param>
            public SortColumn(Comparison<T> comparer, SortDirection direction)
            {
                Direction = direction;
                Comparer = comparer;
            }

            public Comparison<T> Comparer { get; }

            /// <summary>
            ///     The direction to sort.
            /// </summary>
            public SortDirection Direction { get; }
        }

        #endregion
    }
}