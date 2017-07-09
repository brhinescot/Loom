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

using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    /// <summary>
    /// Represents a dynamic SQL statement to execute against a data source. Provides a base 
    /// class for database-specific classes that represent queries.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommandTree<T> where T : CommandTree<T>
    {
        #region Instance Fields

        private readonly ColumnPredicateCollection wherePredicates = new ColumnPredicateCollection(ParameterNameGenerator.GetShortName());

        #endregion

        #region Property Accessors

        internal ColumnPredicateCollection WherePredicates
        {
            get { return wherePredicates; }
        }

        internal bool HasWhereClause
        {
            get { return !Compare.IsNullOrEmpty(wherePredicates); }
        }

        /// <summary>
        /// Gets the <see cref="ISchema"/> instance representing a table in a data source to be queried.
        /// </summary>
        public TableData Table { get; set; }

        #endregion

        #region .ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTree{T}"/> class.
        /// </summary>
        /// <param name="table">The table to query.</param>
        protected CommandTree(TableData table)
        {
            Argument.Assert.IsNotNull(table, Argument.Names.table);

            Table = table;
        }

        #endregion
    }
}
