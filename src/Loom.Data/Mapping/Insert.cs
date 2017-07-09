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

using System;
using System.Collections.Generic;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public sealed class Insert
    {
        #region Member Fields

        private readonly Dictionary<IQueryableColumn, object> insertValues = new Dictionary<IQueryableColumn, object>();

        #endregion

        #region Internal Property Accessors

        internal Dictionary<IQueryableColumn, object> ColumnValues
        {
            get { return insertValues; }
        }

        #endregion

        #region Public Property Accessors

        public TableData Table { get; private set; }

        /// <summary>
        /// Gets the number of column/value pairs added to this <see cref="Insert"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The column/value pairs are added by calling <see cref="Set(IQueryableColumn,object)"/> with the 
        /// <see cref="IQueryableColumn"/> and value to be inserted.</para>
        /// </remarks>
        public int ColumnCount
        {
            get { return ColumnValues.Count; }
        }

        public long NewId { get; internal set; }

        #endregion

        #region .ctor

        private Insert(TableData table)
        {
            Table = table;
        }

        #endregion

        public static Insert Into(TableData table)
        {
            return new Insert(table);
        }

        public Insert Set(IQueryableColumn column, object value)
        {
            ColumnValues.Add(column, value);
            return this;
        }

        public Insert Set(ColumnPredicate columnPredicate)
        {
            if (columnPredicate.Comparison != Comparison.Equal)
                throw new NotSupportedException("Only Equal (==) comparisons are supported for this method.");

            if (columnPredicate.NextInGroup != null)
                throw new NotSupportedException("Multiple comparison operators are not supported for this method.");
            
            ColumnValues.Add(columnPredicate.Column, columnPredicate.Value);
            return this;
        }

        public bool IsSet(IQueryableColumn column)
        {
            return ColumnValues.ContainsKey(column);
        }
    }
}
