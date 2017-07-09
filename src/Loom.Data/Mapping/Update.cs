#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System.Collections.Generic;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public sealed class Update<T> : CommandTree<Update<T>> where T : DataRecord<T>, new()
    {
        #region Member Fields

        private readonly Dictionary<IQueryableColumn, object> updateValues = new Dictionary<IQueryableColumn, object>();

        #endregion

        /// <summary>
        /// Gets the number of column/value pairs added to this <see cref="Update"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The column/value pairs are added by calling <see cref="Set"/> with the 
        /// <see cref="IQueryableColumn"/> and value to be inserted.</para>
        /// </remarks>
        public int ColumnCount
        {
            get { return ColumnValues.Count; }
        }

        #region Internal Property Accessors

        internal Dictionary<IQueryableColumn, object> ColumnValues
        {
            get { return updateValues; }
        }

        #endregion

        #region .ctor

        private Update() : base(DataRecord<T>.Table) { }

        #endregion

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
    /// Represents a class for updating records in a datasource.
    /// </summary>
    public sealed class Update : CommandTree<Update>
    {
        #region Member Fields

        private readonly Dictionary<IQueryableColumn, object> updateValues = new Dictionary<IQueryableColumn, object>();

        #endregion

        /// <summary>
        /// Gets the number of column/value pairs added to this <see cref="Update"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The column/value pairs are added by calling <see cref="Set"/> with the 
        /// <see cref="IQueryableColumn"/> and value to be inserted.</para>
        /// </remarks>
        public int ColumnCount
        {
            get { return ColumnValues.Count; }
        }

        #region Internal Property Accessors

        internal Dictionary<IQueryableColumn, object> ColumnValues
        {
            get { return updateValues; }
        }

        #endregion

        #region .ctor

        private Update(TableData table) : base(table) { }

        #endregion

        public static Update To(TableData table)
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
            if(predicate != null)
                WherePredicates.Add(predicate);

            return this;
        }
    }
}
