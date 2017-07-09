#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    [DebuggerDisplay("CommandBuilder")]
    public abstract class CommandBuilder : ICommandBuilder
    {
        #region Abstract Members

        protected abstract string ParameterPrefix{ get; }
        protected abstract string WildcardCharacter { get; }
        protected abstract string GetComparisonOperator(Comparison comparison, object value);
        public abstract DbCommand ToSelectCommand();
        public abstract DbCommand ToSelectCommand(ConstraintType constraintType);

        #endregion

        #region Instance Fields

        private int groupCount;
        private Dictionary<string, object> parameters;
        private ParameterNameGeneratorHandler nameGenerator;
        private JoinPredicateCollection joinPredicates;
        private JoinPredicateCollection localizedJoinPredicates;
        private ColumnPredicateCollection wherePredicates;
        private ColumnPredicateCollection havingPredicates;
        private Collection<OrderBy> orderBys;
        private Collection<IQueryableColumn> groupBys;
        private Collection<IQueryableColumn> columns;
        private readonly Dictionary<string, TableData> tables = new Dictionary<string, TableData>();
        private readonly List<TableData> selfJoinTables = new List<TableData>();
        private readonly DataSession session;

        #endregion

        #region Property Accessors

        public bool JoinInWhereClause { get; set; }
        public bool Distinct { get; set; }

        public Constraint Constraint { get; private set; }

        protected virtual string UniquePrefix
        {
            get { return "_"; }
        }

        protected Dictionary<string, object> Parameters
        {
            get { return parameters ?? (parameters = new Dictionary<string, object>()); }
        }

        private ParameterNameGeneratorHandler NameGenerator
        {
            get { return nameGenerator ?? (nameGenerator = ParameterNameGenerator.GetShortName(UniquePrefix)); }
        }

        protected Collection<IQueryableColumn> Columns
        {
            get { return columns ?? (columns = new Collection<IQueryableColumn>()); }
        }

        protected Collection<OrderBy> OrderBys
        {
            get { return orderBys ?? (orderBys = new Collection<OrderBy>()); }
        }

        protected Collection<IQueryableColumn> GroupBys
        {
            get { return groupBys ?? (groupBys = new Collection<IQueryableColumn>()); }
        }

        protected Dictionary<string, TableData> Tables
        {
            get { return tables; }
        }

        protected TableData SelectTable { get; set; }

        #endregion

        #region .ctor

        protected CommandBuilder(DataSession session)
        {
            this.session = session;
            JoinInWhereClause = true;
        }

        protected CommandBuilder(QueryTree query)
        {
            session = query.Session;
            AppendFrom(query.Table);
            JoinInWhereClause = true;
        }

        #endregion

        #region ICommandBuilder Members

        JoinPredicateCollection ICommandBuilder.JoinPredicates
        {
            get { return joinPredicates ?? (joinPredicates = new JoinPredicateCollection()); }
        }
        
        private JoinPredicateCollection LocalizedJoinPredicates
        {
            get { return localizedJoinPredicates ?? (localizedJoinPredicates = new JoinPredicateCollection()); }
        }

        ColumnPredicateCollection ICommandBuilder.WherePredicates
        {
            get { return wherePredicates ?? (wherePredicates = new ColumnPredicateCollection(NameGenerator)); }
        }

        ColumnPredicateCollection ICommandBuilder.HavingPredicates
        {
            get { return havingPredicates ?? (havingPredicates = new ColumnPredicateCollection(NameGenerator)); }
        }

        #endregion

        #region Public Entrypoints

        #region Constraints

        public void Constrain(Constraint constraint)
        {
            Constraint = constraint;
        }

        #endregion

        #region SELECT

        public void AppendSelect(IQueryableColumn selectColumn)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (!session.UseDefaultLocale && session.DefaultLocale != currentCulture && session.IsLanguageSupported(currentCulture))
            {
                IQueryableColumn localizedColumn = selectColumn.LocalizedColumn;
                if (localizedColumn != null)
                {
                    localizedColumn = localizedColumn.As(selectColumn.Alias);
                    ColumnPredicate localizePredicate = localizedColumn.Table.FindColumn("Locale") == currentCulture | localizedColumn.Table.FindColumn("Locale") == null;
                    if (!((ICommandBuilder) this).WherePredicates.Contains(localizePredicate))
                        ((ICommandBuilder) this).WherePredicates.Add(localizePredicate);
                    localizedColumn.LocalizeFallbackColumn = selectColumn;
                    Columns.Add(localizedColumn);

                    foreach (QueryColumn primaryKey in selectColumn.Table.PrimaryKey)
                    {
                        JoinPredicate joinPredicate = (primaryKey == localizedColumn.Table.FindColumn(primaryKey.Name));

                        if (!LocalizedJoinPredicates.Contains(joinPredicate))
                            LocalizedJoinPredicates.Add(joinPredicate);
                    }

                    JoinInWhereClause = false;
                    return;
                }
            }

            AppendFrom(selectColumn.Table);
            Columns.Add(selectColumn);
        }

        public void AppendSelect(IQueryableColumn selectColumn1, IQueryableColumn selectColumn2)
        {
            AppendSelect(selectColumn1);
            AppendSelect(selectColumn2);
        }

        public void AppendSelect(IQueryableColumn selectColumn1, IQueryableColumn selectColumn2, IQueryableColumn selectColumn3)
        {
            AppendSelect(selectColumn1);
            AppendSelect(selectColumn2);
            AppendSelect(selectColumn3);
        }

        public void AppendSelect(params IQueryableColumn[] selectColumns)
        {
            foreach (IQueryableColumn column in selectColumns)
                AppendSelect(column);
        }

        #endregion

        #region FROM

        public void AppendFrom(TableData table)
        {
            if (Tables.Count == 0)
                SelectTable = table;

            string key = table.Datasource + table.Owner + table.Name;
            if(Tables.Count > 1 && !JoinInWhereClause)
            {
                Tables.Clear();
                Tables.Add(key, SelectTable);
            }

            if(Tables.Count > 0 && !JoinInWhereClause)
                return;

            if (!Tables.ContainsKey(key))
            {
                Tables.Add(key, table);
            }
        }

        public void AppendFrom(TableData selectTable1, TableData selectTable2)
        {
            AppendFrom(selectTable1);
            AppendFrom(selectTable2);
        }

        public void AppendFrom(TableData selectTable1, TableData selectTable2, TableData selectTable3)
        {
            AppendFrom(selectTable1);
            AppendFrom(selectTable2);
            AppendFrom(selectTable3);
        }

        public void AppendFrom(params TableData[] selectTables) 
        {
            foreach (TableData table in selectTables)
                AppendFrom(table);
        }

        #endregion

        #region 

        public void AppendJoin(JoinPredicate predicate)
        {
            if(!((ICommandBuilder)this).JoinPredicates.Contains(predicate))
                ((ICommandBuilder)this).JoinPredicates.Add(predicate);
        }

        #endregion

        #region WHERE

        public void AppendWhere(IBindablePredicate predicate)
        {
            Argument.Assert.IsNotNull(predicate, "predicate");

            predicate.BindWhere(this);
        }

        public void AppendWhere(IBindablePredicate predicate1, IBindablePredicate predicate2)
        {
            AppendWhere(predicate1);
            AppendWhere(predicate2);
        }

        public void AppendWhere(IBindablePredicate predicate1, IBindablePredicate predicate2, IBindablePredicate predicate3)
        {
            AppendWhere(predicate1);
            AppendWhere(predicate2);
            AppendWhere(predicate3);
        }

        public void AppendWhere(params IBindablePredicate[] predicates)
        {
            Argument.Assert.IsNotNull(predicates, "predicates");

            foreach (ColumnPredicate predicate in predicates)
                AppendWhere(predicate);
        }

        #endregion

        #region HAVING

        public void AppendHaving(IBindablePredicate predicate)
        {
            Argument.Assert.IsNotNull(predicate, "predicate");

            predicate.BindHaving(this);
        }

        public void AppendHaving(IBindablePredicate predicate1, IBindablePredicate predicate2)
        {
            AppendHaving(predicate1);
            AppendHaving(predicate2);
        }

        public void AppendHaving(IBindablePredicate predicate1, IBindablePredicate predicate2, IBindablePredicate predicate3)
        {
            AppendHaving(predicate1);
            AppendHaving(predicate2);
            AppendHaving(predicate3);
        }

        public void AppendHaving(params IBindablePredicate[] predicates)
        {
            Argument.Assert.IsNotNull(predicates, "predicates");

            foreach (IBindablePredicate predicate in predicates)
                AppendHaving(predicate);
        }

        public void AppendHaving(IEnumerable<IBindablePredicate> predicates)
        {
            Argument.Assert.IsNotNull(predicates, "predicates");

            foreach (IBindablePredicate predicate in predicates)
                AppendHaving(predicate);
        }

        #endregion

        #region ORDER BY


        public void AppendOrderBy(IQueryableColumn column, OrderByDirection direction)
        {
            OrderBys.Add(new OrderBy(column, direction));
        }

        public void AppendOrderBy(OrderBy orderBy)
        {
            OrderBys.Add(orderBy);
        }

        public void AppendOrderBy(OrderBy orderBy1, OrderBy orderBy2)
        {
            AppendOrderBy(orderBy1);
            AppendOrderBy(orderBy2);
        }

        public void AppendOrderBy(OrderBy orderBy1, OrderBy orderBy2, OrderBy orderBy3)
        {
            AppendOrderBy(orderBy1);
            AppendOrderBy(orderBy2);
            AppendOrderBy(orderBy3);
        }

        public void AppendOrderBy(params OrderBy[] orderByList)
        {
            Argument.Assert.IsNotNull(orderByList, "groupByColumns");

            foreach (OrderBy orderBy in orderByList)
                AppendOrderBy(orderBy);
        }

        #endregion

        #region GROUP BY

        public void AppendGroupBy(IQueryableColumn groupByColumn)
        {
            GroupBys.Add(groupByColumn);
        }

        public void AppendGroupBy(IQueryableColumn groupByColumn1, IQueryableColumn groupByColumn2)
        {
            AppendGroupBy(groupByColumn1);
            AppendGroupBy(groupByColumn2);
        }

        public void AppendGroupBy(IQueryableColumn groupByColumn1, IQueryableColumn groupByColumn2, IQueryableColumn groupByColumn3)
        {
            AppendGroupBy(groupByColumn1);
            AppendGroupBy(groupByColumn2);
            AppendGroupBy(groupByColumn3);
        }

        public void AppendGroupBy(params IQueryableColumn[] groupByColumns)
        {
            Argument.Assert.IsNotNull(groupByColumns, "groupByColumns");

            foreach (IQueryableColumn groupByColumn in groupByColumns)
                AppendGroupBy(groupByColumn);
        }

        #endregion

        #endregion

        #region Virtual Members

        protected virtual void OnWriteCount(DbTextWriter writer)
        {
            writer.WriteCount();
        }

        protected virtual void OnWriteSelect(DbTextWriter writer)
        {
            writer.WriteSelect(Columns, Constraint, Distinct);
        }

        protected virtual void OnWriteFrom(DbTextWriter writer)
        {
            writer.WriteFrom(Tables.Values, NeedsAlias());
            writer.WriteFrom(selfJoinTables, NeedsAlias());
        }

        protected virtual void OnWriteJoin(DbTextWriter writer)
        {
            if(!writer.IsSelectionSet)
                throw new InvalidOperationException("Select columns have not been set.");

            if (!JoinInWhereClause && joinPredicates != null && joinPredicates.Count > 0)
                WriteJoinPredicates(writer, joinPredicates);
            if (!JoinInWhereClause && localizedJoinPredicates != null && localizedJoinPredicates.Count > 0)
                WriteJoinPredicates(writer, localizedJoinPredicates);
        }

        protected virtual void OnWriteWhere(DbTextWriter writer)
        {
            if (!writer.IsSelectionSet)
                throw new InvalidOperationException("Select columns have not been set.");

            if (JoinInWhereClause && joinPredicates != null && joinPredicates.Count > 0)
            {
                writer.WriteWhere();
                WriteWhereJoinPredicates(writer, joinPredicates);
            }

            if (wherePredicates != null && wherePredicates.Count > 0)
            {
                writer.WriteWhere();
                WriteColumnPredicates(writer, wherePredicates);
            }
        }

        protected virtual void OnWriteHaving(DbTextWriter writer)
        {
            if (havingPredicates == null || havingPredicates.Count == 0)
                return;

            if (!writer.IsSelectionSet)
                throw new InvalidOperationException("Select columns have not been set.");

            writer.WriteHaving();
            WriteColumnPredicates(writer, havingPredicates);
        }

        protected virtual void OnWriteOrderBys(DbTextWriter writer)
        {
            writer.WriteOrderBy(OrderBys, Constraint);
        }

        protected virtual void OnWriteGroupBys(DbTextWriter writer)
        {
            if (groupBys == null || groupBys.Count == 0)
                return;

            writer.WriteGroupBy(GroupBys);
        }

        protected virtual void OnWriteBeginGroup(DbTextWriter writer)
        {
            writer.Write("(");
        }

        protected virtual void OnWriteEndGroup(DbTextWriter writer)
        {
            writer.Write(")");
            groupCount--;
        }

        protected virtual void OnWriteOr(DbTextWriter writer)
        {
            writer.Write(" OR ");
        }

        protected virtual void OnWriteAnd(DbTextWriter writer)
        {
            writer.Write(" AND ");
        }

        protected virtual string FormatPagingSql(DbTextWriter textWriter, Constraint constraint)
        {
            WriteBasicSelect(textWriter);
            return textWriter.ToString();
        }

        #endregion

        #region Private Helpers

        private bool NeedsAlias()
        {
            return (columns != null && columns.Count > 0) ||
                   (wherePredicates != null && wherePredicates.Count > 0) ||
                   (orderBys != null && orderBys.Count > 0) ||
                   (groupBys != null && groupBys.Count > 0) ||
                   (havingPredicates != null && havingPredicates.Count > 0) ||
                   (joinPredicates != null && joinPredicates.Count > 0) ||
                   Constraint.ConstraintType == ConstraintType.Page;
        }

        private void WriteJoinPredicates(DbTextWriter writer, IEnumerable<JoinPredicate> predicates)
        {
            foreach(JoinPredicate predicate in predicates)
            {
                writer.Write(" LEFT JOIN {0} ON ", "[" + predicate.ToColumn.Table.Owner + "].[" + predicate.ToColumn.Table.Name + "] " + writer.GetTableAlias(predicate.ToColumn.Table));

                if(predicate.IsGroup)
                {
                    WriteBeginGroup(writer);
                    for(JoinPredicate current = predicate; current != null; current = current.NextInGroup)
                        WriteJoinPredicate(writer, current);
                }
                else
                {
                    WriteJoinPredicate(writer, predicate);
                }
            }
        }

        private void WriteWhereJoinPredicates(DbTextWriter writer, IEnumerable<JoinPredicate> predicates)
        {
            int i = 0;
            foreach (JoinPredicate predicate in predicates)
            {
                if (predicate.OrToPreviousGroup)
                    OnWriteOr(writer);
                else if (i++ >= 1)
                    OnWriteAnd(writer);

                if (predicate.IsGroup)
                {
                    WriteBeginGroup(writer);
                    for (JoinPredicate current = predicate; current != null; current = current.NextInGroup)
                        WriteJoinPredicate(writer, current);
                }
                else
                {
                    WriteJoinPredicate(writer, predicate);
                }
            }
        }

        private void WriteJoinPredicate(DbTextWriter writer, JoinPredicate current)
        {
            writer.WritePredicate(current, GetComparisonOperator(current.Comparison, current.ToColumn));

            if (current.NextInGroup == null)
                WriteEndGroup(writer);

            if (current.NextInGroup != null && current.OrNextPredicate)
                OnWriteOr(writer);
            else if (current.NextInGroup != null)
                OnWriteAnd(writer);
        }

        private void WriteColumnPredicates(DbTextWriter writer, IEnumerable<ColumnPredicate> predicates)
        {
            int i = 0;
            foreach (ColumnPredicate predicate in predicates)
            {
                if (predicate.OrToPreviousGroup)
                    OnWriteOr(writer);
                else if (i++ >= 1)
                    OnWriteAnd(writer);

                if (predicate.IsGroup)
                {
                    WriteBeginGroup(writer);
                    for (ColumnPredicate current = predicate; current != null; current = current.NextInGroup)
                        WriteColumnPredicate(writer, current);
                }
                else
                {
                    WriteColumnPredicate(writer, predicate);
                }
            }
        }

        private void WriteColumnPredicate(DbTextWriter writer, ColumnPredicate current)
        {
            if(current.IsJoinPredicate)
            {
                WriteJoinPredicate(writer, current);
                return;
            }

            writer.WritePredicate(current.Column, GetComparisonOperator(current.Comparison, current.Value), AddParameter(current));

            if (current.NextInGroup == null)
                WriteEndGroup(writer);

            if (current.NextInGroup != null && current.OrNextPredicate)
                OnWriteOr(writer);
            else if (current.NextInGroup != null)
                OnWriteAnd(writer);
        }

        private void WriteBeginGroup(DbTextWriter writer)
        {
            OnWriteBeginGroup(writer);
            groupCount++;
        }

        private void WriteEndGroup(DbTextWriter writer)
        {
            if (groupCount == 0)
                return;

            OnWriteEndGroup(writer);
        }

        private ParameterNames AddParameter(ColumnPredicate predicate)
        {
            if ((predicate.Value == null || predicate.Value == DBNull.Value)
                && (predicate.Comparison == Comparison.NotEqual || predicate.Comparison == Comparison.Equal))
                return ParameterNames.Empty;

            object value1;
            switch (predicate.Comparison)
            {
                case Comparison.DoesNotStartWith:
                case Comparison.StartsWith:
                    value1 = predicate.Value + WildcardCharacter;
                    break;
                case Comparison.DoesNotEndWith:
                case Comparison.EndsWith:
                    value1 = WildcardCharacter + predicate.Value;
                    break;
                case Comparison.DoesNotContain:
                case Comparison.Contains:
                    value1 = WildcardCharacter + predicate.Value + WildcardCharacter;
                    break;
                default:
                    value1 = predicate.Value;
                    break;
            }

            string param1Name = ParameterPrefix + NameGenerator(predicate.Column.Name);
            string param2Name = null;

            if (!Parameters.ContainsKey(param1Name))
                Parameters.Add(param1Name, value1);

            if (predicate.Comparison == Comparison.Between)
            {
                param2Name = ParameterPrefix + NameGenerator(predicate.Column.Name);
                if(!Parameters.ContainsKey(param2Name))
                    Parameters.Add(param2Name, predicate.Value2);
            }

            return new ParameterNames(param1Name, param2Name);
        }

        #endregion

        public string ToSelectText()
        {
            using (DbTextWriter writer = new DbTextWriter())
                return ToSelectText(writer, ConstraintType.None);
        }

        public string ToSelectText(ConstraintType constraintType)
        {
            using (DbTextWriter writer = new DbTextWriter())
                return ToSelectText(writer, constraintType);
        }

        public string ToSelectText(DbTextWriter textWriter)
        {
            return ToSelectText(textWriter, ConstraintType.None);
        }

        public string ToSelectText(DbTextWriter textWriter, ConstraintType constraintType)
        {
            Parameters.Clear();

            if (Constraint.ConstraintType == ConstraintType.Page)
                return FormatPagingSql(textWriter, Constraint);

            if (constraintType == ConstraintType.Count)
                WriteCountSelect(textWriter);
            else
                WriteBasicSelect(textWriter);
            return textWriter.ToString();
        }

        private void WriteBasicSelect(DbTextWriter textWriter)
        {
            OnWriteSelect(textWriter);
            OnWriteFrom(textWriter);
            OnWriteJoin(textWriter);
            OnWriteWhere(textWriter);
            OnWriteGroupBys(textWriter);
            OnWriteHaving(textWriter);
            OnWriteOrderBys(textWriter);
        }

        private void WriteCountSelect(DbTextWriter textWriter)
        {
            OnWriteCount(textWriter);
            OnWriteFrom(textWriter);
            OnWriteJoin(textWriter);
            OnWriteWhere(textWriter);
            OnWriteGroupBys(textWriter);
            OnWriteHaving(textWriter);
            OnWriteOrderBys(textWriter);
        }
    }
}
