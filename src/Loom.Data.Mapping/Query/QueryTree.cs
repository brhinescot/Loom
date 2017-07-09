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
    ///     Represents a command class for querying records in a datasource.
    /// </summary>
    public class QueryTree : CommandTree<QueryTree>
    {
        private static readonly int SelectWithDistinctMask = BitVector32.CreateMask();
        private static readonly int SelectWithCountMask = BitVector32.CreateMask(SelectWithDistinctMask);
        private static readonly int SelectWithPagingMask = BitVector32.CreateMask(SelectWithCountMask);
        private static readonly int LocalizedMask = BitVector32.CreateMask(SelectWithPagingMask);

        // TODO: Custom serialize EntitySet class
        [NonSerialized] private BitVector32 flags = new BitVector32(0);

        private List<IQueryableColumn> groupByColumns;
        private ColumnPredicateCollection havingPredicates;
        private JoinPredicateCollection joins;

        private List<OrderBy> orderBys;
        private List<IQueryableColumn> queryColumns;

        public QueryTree(ITable table, DataSession session)
            : base(table)
        {
            Session = session;
        }

        public int ColumnCount => Columns.Count;

        internal List<OrderBy> OrderBys => orderBys ?? (orderBys = new List<OrderBy>());

        internal List<IQueryableColumn> GroupBys => groupByColumns ?? (groupByColumns = new List<IQueryableColumn>());

        internal ColumnPredicateCollection HavingPredicates => havingPredicates ?? (havingPredicates = new ColumnPredicateCollection(ParameterNameGenerator.GetShortName()));

        internal List<IQueryableColumn> Columns => queryColumns ?? (queryColumns = new List<IQueryableColumn>());

        internal JoinPredicateCollection Joins => joins ?? (joins = new JoinPredicateCollection());

        public bool SelectWithDistinct
        {
            get => flags[SelectWithDistinctMask];
            set => flags[SelectWithDistinctMask] = value;
        }

        internal bool SelectWithPaging
        {
            get => flags[SelectWithPagingMask];
            set => flags[SelectWithPagingMask] = value;
        }

        internal bool Localized
        {
            get => flags[LocalizedMask];
            set => flags[LocalizedMask] = value;
        }

        internal QueryTree Next { get; private set; }

        internal int StartIndex { get; set; }

        internal int PageSize { get; set; }

        internal Constraint ResultConstraint { get; set; }

        public DataSession Session { get; }

        internal void AppendQuery(QueryTree value)
        {
            Argument.Assert.IsNotNull(value, nameof(value));

            if (Next == null)
                Next = value;
            else
                Next.AppendQuery(value);
        }

        /// <summary>
        ///     Gets a list of <see cref="IQueryableColumn" /> objects that represent the columns in the query.
        /// </summary>
        /// <returns></returns>
        public IQueryableColumn[] RetrieveQueryColumns()
        {
            return queryColumns == null
                ? new IQueryableColumn[0]
                : queryColumns.ToArray();
        }

        /// <summary>
        ///     Gets a list of <see cref="IQueryableColumn" /> objects that represent the group by columns in the query.
        /// </summary>
        /// <returns></returns>
        public IQueryableColumn[] RetrieveGroupByColumns()
        {
            return groupByColumns == null
                ? new IQueryableColumn[0]
                : groupByColumns.ToArray();
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public OrderBy[] RetrieveOrderByColumns()
        {
            return orderBys == null
                ? new OrderBy[0]
                : orderBys.ToArray();
        }
    }
}