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
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Loom.Annotations;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public class EntitySet<T> : CommandBase<T> where T : DataRecord<T>, new()
    {
        #region .ctors

        internal EntitySet(DataSession session)
            : base(session, DataRecord<T>.Table)
        {}

        #endregion

        /// <summary>
        /// Appends a new <see cref="EntitySet{T}"/> to the end of this instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="EntitySet{T}"/> objects appended to this instance will be returned as additional results when
        /// using <see cref="CommandBase{T}.FetchReader"/> and additional <see cref="DataTable"/> objects in the 
        /// <see cref="DataSet"/> returned from <see cref="CommandBase{T}.FetchDataSet"/></para>
        /// <para>
        /// Use <see cref="IDataReader.NextResult"/> to access the result of the batched queries.</para>
        /// </remarks>
        /// <param name="entitySet">The <see cref="EntitySet{T}"/> to append to this instance.</param>
        public void Append<TNext>(EntitySet<TNext> entitySet) where TNext : DataRecord<TNext>, new()
        {
            QueryTree.AppendQuery(entitySet.QueryTree);
        }

        public EntitySet<T> Constrain(Constraint constraint)
        {
            QueryTree.ResultConstraint = constraint;
            return this;
        }

        public EntitySet<T> Distinct()
        {
            QueryTree.SelectWithDistinct = true;
            return this;
        }

        public EntitySet<T> Top(int count)
        {
            QueryTree.ResultConstraint = Constraint.TopCount(count);
            return this;
        }

        public EntitySet<T> TopPercent(int count)
        {
            QueryTree.ResultConstraint = Constraint.TopPercent(count);
            return this;
        }

        public T Add(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            Session.Delete(entity);
            return entity;
        }

        #region Select Implementation

        /// <summary>
        /// Adds an individual column to the select list.
        /// </summary>
        /// <param name="column">The <see cref="IQueryableColumn"/> to add to the <see cref="Query.QueryTree"/> query.</param>
        /// <returns>The current working <see cref="Query.QueryTree"/> object.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public EntitySet<T> Select(IQueryableColumn column)
        {
            Argument.Assert.IsNotNull(column, Argument.Names.column);
            QueryTree.Columns.Add(column);

            return this;
        }

        /// <summary>
        /// Adds columns to the select list.
        /// </summary>
        /// <param name="column1">The <see cref="IQueryableColumn"/> to add to the <see cref="Query.QueryTree"/> query.</param>
        /// <param name="column2">The <see cref="IQueryableColumn"/> to add to the <see cref="Query.QueryTree"/> query.</param>
        /// <returns>The current working <see cref="Query.QueryTree"/> object.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public EntitySet<T> Select(IQueryableColumn column1, IQueryableColumn column2)
        {
            Select(column1);
            Select(column2);

            return this;
        }

        /// <summary>
        /// Adds columns to the select list.
        /// </summary>
        /// <param name="column1">The <see cref="IQueryableColumn"/> to add to the <see cref="Query.QueryTree"/> query.</param>
        /// <param name="column2">The <see cref="IQueryableColumn"/> to add to the <see cref="Query.QueryTree"/> query.</param>
        /// <param name="column3">The <see cref="IQueryableColumn"/> to add to the <see cref="Query.QueryTree"/> query.</param>
        /// <returns>The current working <see cref="Query.QueryTree"/> object.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public EntitySet<T> Select(IQueryableColumn column1, IQueryableColumn column2, IQueryableColumn column3)
        {
            Select(column1);
            Select(column2);
            Select(column3);

            return this;
        }

        ///<summary>
        ///</summary>
        ///<param name="columns"></param>
        ///<returns></returns>
        public EntitySet<T> Select(params IQueryableColumn[] columns)
        {
            if (columns == null || columns.Length == 0)
            {
                foreach (QueryColumn column in DataRecord<T>.Table.Columns)
                    Select(column);
            }
            else
            {
                foreach (IQueryableColumn column in columns)
                    Select(column);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EntitySet<T> SelectAll()
        {
            Select();
            return this;
        }

        #endregion

        #region Join Implementation

        public EntitySet<T> Join<TDataRecord>(JoinPredicate predicate) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            QueryTree.Joins.Add(predicate);
            return this;
        }

        /// <exception cref="ArgumentException">The <see cref="Query.ColumnPredicate"/> can not be converted 
        /// to a <see cref="JoinPredicate"/>. The right side object must be an 
        /// <see cref="IQueryableColumn"/> instance.</exception>
        public EntitySet<T> Join<TDataRecord>(ColumnPredicate predicate) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            if (!predicate.IsJoinPredicate)
                throw new ArgumentException("The ColumnPredicate can not be converted to a JoinPredicate. The right side object must be an IQueryableColumn instance.");

            QueryTree.Joins.Add(predicate);
            return this;
        }

        public EntitySet<T> Join<TDataRecord>(QueryColumn fromColumn, QueryColumn toColumn) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Argument.Assert.IsNotNull(fromColumn, Argument.Names.fromColumn);
            Argument.Assert.IsNotNull(toColumn, Argument.Names.toColumn);

            JoinPredicate joinPredicate = new JoinPredicate(fromColumn, Comparison.Equal, toColumn);
            QueryTree.Joins.Add(joinPredicate);

            return this;
        }

        public EntitySet<T> Join<TDataRecord>() where TDataRecord : DataRecord<TDataRecord>, new()
        {
            AutoJoin(DataRecord<TDataRecord>.Table);
            return this;
        }

        /// <exception cref="InvalidOperationException">Unable to perform auto join.</exception>
        private void AutoJoin(TableData toTable)
        {
            JoinPredicate predicate = null;
            int matchCount = 0;

            foreach (IQueryableColumn column in toTable.Columns)
            {
                if (column.ForeignKeyColumn == null)
                    continue;

                IQueryableColumn foreignKeyColumn = column.ForeignKeyColumn;
                if (!SchemaCompare.ColumnsAreSame(foreignKeyColumn, QueryTree.Table.PrimaryKey))
                    continue;

                predicate = new JoinPredicate(foreignKeyColumn, Comparison.Equal, column);
                matchCount++;
            }

            foreach (IQueryableColumn column in QueryTree.Table.Columns)
            {
                if (column.ForeignKeyColumn == null)
                    continue;

                IQueryableColumn foreignKeyColumn = column.ForeignKeyColumn;
                if (!SchemaCompare.ColumnsAreSame(foreignKeyColumn, toTable.PrimaryKey))
                    continue;

                predicate = new JoinPredicate(column, Comparison.Equal, foreignKeyColumn);
                matchCount++;
            }

//            if (predicate == null)
//            {
//
//                foreach(IQueryableColumn column in toTable.Columns)
//                {
//                    if(column.ForeignKeyColumn == null)
//                        continue;
//
//                    IQueryableColumn foreignKeyColumn = column.ForeignKeyColumn;
//                    foreach (QueryColumn foreignKeyInnerColumn in foreignKeyColumn.Table.Columns)
//                    {
//                        if(!SchemaCompare.ColumnsAreSame(foreignKeyInnerColumn, QueryTree.Table.PrimaryKey))
//                            continue;
//
//                        predicate = new JoinPredicate(foreignKeyInnerColumn, Comparison.Equal, column);
//                        matchCount++;
//                    }
//                }
//
//                foreach(IQueryableColumn column in QueryTree.Table.Columns)
//                {
//                    if(column.ForeignKeyColumn == null)
//                        continue;
//
//                    IQueryableColumn foreignKeyColumn = column.ForeignKeyColumn;
//                    foreach (QueryColumn foreignKeyinnerColumn in foreignKeyColumn.Table.Columns)
//                    {
//                        if(!SchemaCompare.ColumnsAreSame(foreignKeyinnerColumn, toTable.PrimaryKey))
//                            continue;
//
//                        predicate = new JoinPredicate(column, Comparison.Equal, foreignKeyinnerColumn);
//                        matchCount++;
//                    }
//                }
//            }
                
//            if (predicate == null)
//                throw new InvalidOperationException(string.Format("Unable to map table {0}.{1} to table {2}.{3}.", QueryTree.Table.Owner, QueryTree.Table.Name, toTable.Owner, toTable.Name));

            if (matchCount > 1)
                throw new InvalidOperationException("Multiple potential joins were found. Unable to perform the auto-join operation.");

            if(predicate != null)
                QueryTree.Joins.Add(predicate);
        }

        #endregion

        #region Where Implementation

        public QueryCondition<T> Where(ColumnPredicate predicate)
        {
            if(predicate == null)
                return new QueryCondition<T>(this, QueryTree.WherePredicates);

            if (predicate.IsJoinPredicate)
            {
                Join<T>(predicate);
                return new QueryCondition<T>(this, QueryTree.WherePredicates);
            }

            TestForPredicateJoin(predicate);

            QueryTree.WherePredicates.Add(predicate);
            return new QueryCondition<T>(this, QueryTree.WherePredicates);
        }

        #endregion

        #region Having Implementation

        public QueryCondition<T> Having(ColumnPredicate predicate)
        {
            if (predicate == null)
                return new QueryCondition<T>(this, QueryTree.HavingPredicates);

            TestForPredicateJoin(predicate);

            QueryTree.HavingPredicates.Add(predicate);
            return new QueryCondition<T>(this, QueryTree.HavingPredicates);
        }

        #endregion

        #region OrderBy Implementation

        public EntitySet<T> OrderBy(IQueryableColumn column)
        {
            Argument.Assert.IsNotNull(column, Argument.Names.column);

            QueryTree.OrderBys.Add(new OrderBy(column, OrderByDirection.Asc));

            return this;
        }

        public EntitySet<T> OrderBy(IQueryableColumn column1, IQueryableColumn column2)
        {
            Argument.Assert.IsNotNull(column1, Argument.Names.column);
            Argument.Assert.IsNotNull(column2, Argument.Names.column);

            QueryTree.OrderBys.Add(new OrderBy(column1, OrderByDirection.Asc));
            QueryTree.OrderBys.Add(new OrderBy(column2, OrderByDirection.Asc));

            return this;
        }

        public EntitySet<T> OrderBy(IQueryableColumn column1, IQueryableColumn column2, IQueryableColumn column3)
        {
            Argument.Assert.IsNotNull(column1, Argument.Names.column);
            Argument.Assert.IsNotNull(column2, Argument.Names.column);
            Argument.Assert.IsNotNull(column3, Argument.Names.column);

            QueryTree.OrderBys.Add(new OrderBy(column1, OrderByDirection.Asc));
            QueryTree.OrderBys.Add(new OrderBy(column2, OrderByDirection.Asc));
            QueryTree.OrderBys.Add(new OrderBy(column3, OrderByDirection.Asc));

            return this;
        }

        public EntitySet<T> OrderBy(params IQueryableColumn[] columns)
        {
            foreach (var column in columns)
                QueryTree.OrderBys.Add(new OrderBy(column, OrderByDirection.Asc));

            return this;
        }

        public EntitySet<T> OrderBy(IEnumerable<IQueryableColumn> columns)
        {
            foreach (var column in columns)
                QueryTree.OrderBys.Add(new OrderBy(column, OrderByDirection.Asc));

            return this;
        }

        public EntitySet<T> OrderBy(IQueryableColumn column, OrderByDirection direction)
        {
            Argument.Assert.IsNotNull(column, Argument.Names.column);

            QueryTree.OrderBys.Add(new OrderBy(column, direction));
            return this;
        }

        #endregion

        #region GroupBy Implementation

        /// <summary>
        /// Adds a group by clause on the specified <paramref name="column"/>.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public EntitySet<T> GroupBy(IQueryableColumn column)
        {
            Argument.Assert.IsNotNull(column, Argument.Names.column);

            QueryTree.GroupBys.Add(column);
            return this;
        }

        /// <summary>
        /// Adds a group by clause on the specified columns.
        /// </summary>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <returns></returns>
        public EntitySet<T> GroupBy(IQueryableColumn column1, IQueryableColumn column2)
        {
            Argument.Assert.IsNotNull(column1, Argument.Names.column1);
            Argument.Assert.IsNotNull(column2, Argument.Names.column2);

            QueryTree.GroupBys.Add(column1);
            QueryTree.GroupBys.Add(column2);

            return this;
        }

        /// <summary>
        /// Adds a group by clause on the specified columns.
        /// </summary>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <param name="column3"></param>
        /// <returns></returns>
        public EntitySet<T> GroupBy(IQueryableColumn column1, IQueryableColumn column2, IQueryableColumn column3)
        {
            Argument.Assert.IsNotNull(column1, Argument.Names.column1);
            Argument.Assert.IsNotNull(column2, Argument.Names.column2);
            Argument.Assert.IsNotNull(column3, Argument.Names.column3);

            QueryTree.GroupBys.Add(column1);
            QueryTree.GroupBys.Add(column2);
            QueryTree.GroupBys.Add(column3);

            return this;
        }

        /// <summary>
        /// Adds a group by clause on the specified <paramref name="columns"/>.
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public EntitySet<T> GroupBy(IEnumerable<IQueryableColumn> columns)
        {
            if (columns == null)
                return this;

            QueryTree.GroupBys.AddRange(columns);
            return this;
        }

        /// <summary>
        /// Adds a group by clause on the specified <paramref name="columns"/>.
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public EntitySet<T> GroupBy(params IQueryableColumn[] columns)
        {
            if (columns == null || columns.Length == 0)
                return this;

            QueryTree.GroupBys.AddRange(columns);
            return this;
        }

        #endregion

        #region Paging Implementation

        /// <summary>
        /// Causes the query to return a subset of the data starting at the specified 
        /// <paramref name="startRowIndex"/> and returning up to the
        /// number of rows specified by <paramref name="maximumRows"/>.
        /// </summary>
        /// <param name="startRowIndex">The row on which to start the result set.</param>
        /// <param name="maximumRows">The number of rows after the <paramref name="startRowIndex"/>
        ///  to return.</param>
        /// <returns>An instance of the <see cref="EntitySet{T}"/> class used to refine and execute 
        /// the query.</returns>
        public EntitySet<T> Page(int startRowIndex, int maximumRows)
        {
            QueryTree.StartIndex = startRowIndex;
            QueryTree.PageSize = maximumRows;
            QueryTree.SelectWithPaging = true;

            return this;
        }

        #endregion

        #region Command Execution

        public DataSet FetchDataSet(ColumnPredicate wherePredicate)
        {
            return FetchDataSet();
        }

        public IDataReader FetchReader(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return FetchReader();
        }

        public object FetchScalar(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return FetchScalar();
        }
        
        public T FetchFirst(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return FetchFirst();
        }

        public T FetchFirst<TColumns>(Func<TColumns, ColumnPredicate> expression)
        {

            return FetchFirst();
        }

        public TResult FetchFirst<TResult>(ColumnPredicate wherePredicate, Expression<Func<T, TResult>> converter)
        {
            Where(wherePredicate);
            return FetchFirst(converter);
        }

        public List<T> ToList(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return ToList();
        }

        public List<TResult> ToList<TResult>(ColumnPredicate wherePredicate, Expression<Func<T, TResult>> converter)
        {
            Where(wherePredicate);
            return ToList(converter);
        }

        public Collection<T> ToCollection(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return ToCollection();
        }

        public Collection<TResult> ToCollection<TResult>(ColumnPredicate wherePredicate, Expression<Func<T, TResult>> converter)
        {
            Where(wherePredicate);
            return ToCollection(converter);
        }

        public Dictionary<TKey, T> ToDictionary<TKey>(IQueryableColumn keyColumn, ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return ToDictionary<TKey>(keyColumn);
        }

        [NotNull]
        public T[] ToArray(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return ToList().ToArray();
        }

        [NotNull]
        public TResult[] ToArray<TResult>(ColumnPredicate wherePredicate, Expression<Func<T, TResult>> converter)
        {
            Where(wherePredicate);
            return ToList(converter).ToArray();
        }

        public string ToXml(ColumnPredicate wherePredicate)
        {
            Where(wherePredicate);
            return ToXml();
        }

        #endregion

        #region Private Methods

        private void TestForPredicateJoin(ColumnPredicate predicate)
        {
            for (ColumnPredicate current = predicate; current != null; current = current.NextInGroup)
            {
                if (ShouldTryAutoJoin(current.Column, QueryTree))
                    AutoJoin(current.Column.Table);
            }
        }

        private static bool ShouldTryAutoJoin(IQueryableColumn current, QueryTree queryTree)
        {
            if (!SchemaCompare.TablesAreSame(current.Table, queryTree.Table))
            {
                foreach (JoinPredicate join in queryTree.Joins)
                    if (SchemaCompare.TablesAreSame(join.ToColumn.Table, current.Table))
                        return false;

                return true;
            }
            return false;
        }

        #endregion
    }
}
