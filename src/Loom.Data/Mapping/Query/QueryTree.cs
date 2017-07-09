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
using System.Collections.Specialized;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    public delegate object PrefetchHandler();

    /// <summary>
    /// Represents a command class for querying records in a datasource.
    /// </summary>
    public class QueryTree : CommandTree<QueryTree>
    {
        private readonly DataSession session;

        #region Type Fields

        private static readonly int SelectWithDistinctMask = BitVector32.CreateMask();
        private static readonly int SelectWithCountMask = BitVector32.CreateMask(SelectWithDistinctMask);
        private static readonly int SelectWithPagingMask = BitVector32.CreateMask(SelectWithCountMask);
        private static readonly int LocalizedMask = BitVector32.CreateMask(SelectWithPagingMask);

        #endregion

        #region Instance Fields

        private List<OrderBy> orderBys;
        private JoinPredicateCollection joins;
        private List<IQueryableColumn> queryColumns;
        private List<IQueryableColumn> groupByColumns;
        private ColumnPredicateCollection havingPredicates;

        // TODO: Custom serialize EntitySet class
        [NonSerialized] private BitVector32 flags = new BitVector32(0);

        #endregion

        #region Property Accessors

        public int ColumnCount
        {
            get { return Columns.Count; }
        }

        internal List<OrderBy> OrderBys
        {
            get { return orderBys ?? (orderBys = new List<OrderBy>()); }
        }

        internal List<IQueryableColumn> GroupBys
        {
            get { return groupByColumns ?? (groupByColumns = new List<IQueryableColumn>()); }
        }

        internal ColumnPredicateCollection HavingPredicates
        {
            get { return havingPredicates ?? (havingPredicates = new ColumnPredicateCollection(ParameterNameGenerator.GetShortName())); }
        }

        internal List<IQueryableColumn> Columns
        {
            get { return queryColumns ?? (queryColumns = new List<IQueryableColumn>()); }
        }

        internal JoinPredicateCollection Joins
        {
            get { return joins ?? (joins = new JoinPredicateCollection()); }
        }

        public bool SelectWithDistinct
        {
            get { return flags[SelectWithDistinctMask]; }
            set { flags[SelectWithDistinctMask] = value; }
        }

        internal bool SelectWithPaging
        {
            get { return flags[SelectWithPagingMask]; }
            set { flags[SelectWithPagingMask] = value; }
        }

        internal bool Localized
        {
            get { return flags[LocalizedMask]; }
            set { flags[LocalizedMask] = value; }
        }

        internal QueryTree Next { get; private set; }

        internal int StartIndex { get; set; }

        internal int PageSize { get; set; }

        internal Constraint ResultConstraint { get; set; }

        public DataSession Session
        {
            get { return session; }
        }

        #endregion

        #region .ctor

        public QueryTree(TableData table, DataSession session)
            : base(table)
        {
            this.session = session;
        }

        #endregion

        internal void AppendQuery(QueryTree value)
        {
            Argument.Assert.IsNotNull(value, Argument.Names.value);

            if (Next == null)
                Next = value;
            else
                Next.AppendQuery(value);
        }

        /// <summary>
        /// Gets a list of <see cref="IQueryableColumn"/> objects that represent the columns in the query.
        /// </summary>
        /// <returns></returns>
        public IQueryableColumn[] RetrieveQueryColumns()
        {
            return queryColumns == null 
                ? new IQueryableColumn[0] : queryColumns.ToArray();
        }

        /// <summary>
        /// Gets a list of <see cref="IQueryableColumn"/> objects that represent the group by columns in the query.
        /// </summary>
        /// <returns></returns>
        public IQueryableColumn[] RetrieveGroupByColumns()
        {
            return groupByColumns == null 
                ? new IQueryableColumn[0] : groupByColumns.ToArray();
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public OrderBy[] RetrieveOrderByColumns()
        {
            return orderBys == null 
                ? new OrderBy[0] : orderBys.ToArray();
        }
    }
}
