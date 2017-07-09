#region Using Directives

using System.Collections.Generic;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public sealed class Update<T> : CommandTree<Update<T>> where T : DataRecord<T>, new()
    {
        private Update() : base(DataRecord<T>.Table) { }

        /// <summary>
        ///     Gets the number of column/value pairs added to this <see cref="Update" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The column/value pairs are added by calling <see cref="Set" /> with the
        ///         <see cref="IQueryableColumn" /> and value to be inserted.
        ///     </para>
        /// </remarks>
        public int ColumnCount => ColumnValues.Count;

        internal Dictionary<IQueryableColumn, object> ColumnValues { get; } = new Dictionary<IQueryableColumn, object>();

        public static Update<T> To()
        {
            Update<T> builder = new Update<T>();
            return builder;
        }

        public Update<T> Set(IQueryableColumn column, object value)
        {
            ColumnValues.Add(column, value);
            return this;
        }

        public Update<T> Where(ColumnPredicate predicate)
        {
            if (predicate != null)
                WherePredicates.Add(predicate);

            return this;
        }
    }

    /// <summary>
    ///     Represents a class for updating records in a datasource.
    /// </summary>
    public sealed class Update : CommandTree<Update>
    {
        private Update(ITable table) : base(table) { }

        /// <summary>
        ///     Gets the number of column/value pairs added to this <see cref="Update" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The column/value pairs are added by calling <see cref="Set" /> with the
        ///         <see cref="IQueryableColumn" /> and value to be inserted.
        ///     </para>
        /// </remarks>
        public int ColumnCount => ColumnValues.Count;

        internal Dictionary<IQueryableColumn, object> ColumnValues { get; } = new Dictionary<IQueryableColumn, object>();

        public static Update To(ITable table)
        {
            Update builder = new Update(table);
            return builder;
        }

        public Update Set(IQueryableColumn column, object value)
        {
            ColumnValues.Add(column, value);
            return this;
        }

        public Update Where(ColumnPredicate predicate)
        {
            if (predicate != null)
                WherePredicates.Add(predicate);

            return this;
        }
    }
}