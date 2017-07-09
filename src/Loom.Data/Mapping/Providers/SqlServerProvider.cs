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
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using Colossus.Framework.Data;
using Loom.Collections;
using Loom.Data.Mapping.CodeGeneration;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;
using Loom.Data.SqlClient;

#endregion

namespace Loom.Data.Mapping.Providers
{
    internal sealed class SqlServerProvider : SqlDataSessionProvider, IActiveDataProvider
    {
        #region Type Fields

        [StructLayout(LayoutKind.Auto)]
        private struct SchemaValue
        {
            internal const string BaseTable = "BASE TABLE";
            internal const string Yes = "YES";
            internal const string ForeignKey = "FOREIGN KEY";
            internal const string PrimaryKey = "PRIMARY KEY";
            internal const string Unique = "UNIQUE";
        }

        private const string SqlSeperator = ";";
        private const string WildcardCharacter = "%";
        private const string ParameterPrefix = "@";
        private const string Comma = ",";

        #endregion

        #region IActiveDataProvider Implementation

        public DatabaseSchema FetchDatabaseSchema(string connectionString, ActiveMapCodeGenConfigurationSection configuration)
        {
            return new DatabaseSchema(FetchTableList(connectionString, configuration), FetchProcedureList(connectionString, configuration));
        }

        public DbCommand FetchCommand(CodeGenQuery codeGenQuery)
        {
            Argument.Assert.IsNotNull(codeGenQuery, Argument.Names.codeGenQuery);

            StringBuilder sqlBuilder = new StringBuilder(30);
            sqlBuilder.AppendFormat("SELECT * FROM {0}", codeGenQuery.Table.FullNameBracketed);
            
            return new SqlCommand(sqlBuilder.ToString()) { CommandType = CommandType.Text };
        }

        public DbCommand FetchCommand<T>(StoredProcedure<T> procedure) where T : StoredProcedure<T>, new()
        {
            Argument.Assert.IsNotNull(procedure, Argument.Names.procedure);

            ICallable callable = StoredProcedure<T>.Procedure;

            SqlCommand command = new SqlCommand(string.Format("[{0}].[{1}]", callable.Owner, callable.Name)) {CommandType = CommandType.StoredProcedure};
            
            foreach (ICallableParameter parameter in callable.Parameters)
            {
                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = parameter.Name, 
                    Value = procedure[parameter.Name] ?? DBNull.Value, 
                    DbType = parameter.DbType
                };

                if (parameter.ParameterType == ParameterType.In || parameter.ParameterType == ParameterType.Unknown)
                    sqlParameter.Direction = ParameterDirection.Input;
                else if (parameter.IsResult)
                    sqlParameter.Direction = ParameterDirection.ReturnValue;
                else switch (parameter.ParameterType)
                {
                    case ParameterType.Out:
                        sqlParameter.Direction = ParameterDirection.Output;
                        break;
                    case ParameterType.InOut:
                        sqlParameter.Direction = ParameterDirection.InputOutput;
                        break;
                }
                sqlParameter.Size = parameter.MaxLength;
                command.Parameters.Add(sqlParameter);
            }
            return command;
        }

        public DbCommand FetchCommand(QueryTree queryTree, ConstraintType constraintType)
        {
            Argument.Assert.IsNotNull(queryTree, Argument.Names.query);

            CommandBuilder builder = GetSelectBuilder(queryTree, constraintType);
            DbCommand cmd = builder.ToSelectCommand(constraintType);
            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        public DbCommand FetchCommand(Insert insert)
        {
            Argument.Assert.IsNotNull(insert, Argument.Names.insert);

            StringBuilder sqlBuilder = new StringBuilder(100);
            SqlCommand cmd = new SqlCommand {CommandType = CommandType.Text};

            const string s1 = "INSERT INTO [";
            const string s2 = "].[";
            const string s3 = "] (";
            const string s4 = ") VALUES (";
            const string s5 = ") SET @{0} = SCOPE_IDENTITY();";
            const string s6Replaced = ",)";
            const string s6 = ")";

            sqlBuilder.Append(s1 + insert.Table.Owner + s2 + insert.Table.Name + s3);
            Dictionary<IQueryableColumn, object> columnValues = insert.ColumnValues;

            foreach (var pair in columnValues.Where(p => !Equals(insert.Table.IdentityColumn, p.Key)))
                sqlBuilder.Append(pair.Key.Name + Comma);

            sqlBuilder.Append(s4);
            foreach (var pair in columnValues.Where(p => !Equals(insert.Table.IdentityColumn, p.Key)))
                sqlBuilder.Append(ParameterPrefix + pair.Key.Name + Comma);

            if (insert.Table.HasIdentityColumn)
                sqlBuilder.AppendFormat(s5, DataSessionBase<IDataSessionProvider>.NewIdInsertParameterName);
            else
                sqlBuilder.Append(s6);

            cmd.CommandText = sqlBuilder.ToString().Replace(s6Replaced, s6);

            foreach (var pair in columnValues.Where(p => !Equals(insert.Table.IdentityColumn, p.Key)))
                AddInsertParameter(cmd, pair);

            if (insert.Table.HasIdentityColumn)
                AddInsertIdentityParameter(cmd);

            return cmd;
        }

        private static void AddInsertParameter(SqlCommand cmd, KeyValuePair<IQueryableColumn, object> pair)
        {
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = pair.Key.Name, 
                Value = pair.Value ?? DBNull.Value, 
                Direction = ParameterDirection.Input
            };

            cmd.Parameters.Add(sqlParameter);
        }

        private static void AddInsertIdentityParameter(SqlCommand cmd)
        {
            SqlParameter newId = new SqlParameter
            {
                DbType = DbType.Int64, 
                Direction = ParameterDirection.Output, 
                ParameterName = DataSessionBase<IDataSessionProvider>.NewIdInsertParameterName, 
                Value = 0
            };

            cmd.Parameters.Add(newId);
        }

        public DbCommand FetchCommand(Update update)
        {
            Argument.Assert.IsNotNull(update, Argument.Names.update);

            StringBuilder sqlBuilder = new StringBuilder(100);
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text
            };

            sqlBuilder.Append("UPDATE [" + update.Table.Owner + "].[" + update.Table.Name + "] SET ");
            foreach (KeyValuePair<IQueryableColumn, object> pair in update.ColumnValues)
            {
                sqlBuilder.Append(pair.Key.Name + " = @Update" + pair.Key.Name + ", ");

                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = "Update" + pair.Key.Name, 
                    Value = pair.Value ?? DBNull.Value, 
                    Direction = ParameterDirection.Input
                };

                cmd.Parameters.Add(sqlParameter);
            }
            sqlBuilder.Remove(sqlBuilder.Length - 2, 2);

            if (update.WherePredicates.Count > 0)
            {
                sqlBuilder.Append(" WHERE ");
                AddColumnPredicates(update.WherePredicates, sqlBuilder, cmd);
            }

            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }

        public DbCommand FetchCommand(Delete delete, bool obliterate)
        {
            Argument.Assert.IsNotNull(delete, Argument.Names.delete);
            if (!obliterate)
                return FetchCommand(delete);

            TableData table = delete.Table;

            if (delete.WherePredicates.Count == 0)
                return BuildTruncateCommand(table.Owner, table.Name);

            StringBuilder sqlBuilder = new StringBuilder(50);
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text
            };

            sqlBuilder.AppendFormat("DELETE [{0}].[{1}]", table.Owner, table.Name);

            if (delete.WherePredicates.Count > 0)
            {
                sqlBuilder.Append(" WHERE ");
                AddColumnPredicates(delete.WherePredicates, sqlBuilder, cmd);
            }

            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }

        private DbCommand FetchCommand(Delete delete)
        {
            Argument.Assert.IsNotNull(delete, Argument.Names.delete);

            if (!delete.Table.HasDeletedColumn)
                return FetchCommand(delete, true);

            Update update = Update.To(delete.Table).Set(delete.Table.DeletedColumn, true);

            foreach (var predicate in delete.WherePredicates)
                update.WherePredicates.Add(predicate);

            return FetchCommand(update);
        }

        #endregion

        #region Schema Helper Methods

        [SuppressMessage("Microsoft.Security", "CA2100:ReviewSqlQueriesForSecurityVulnerabilities")]
        private TableDefinitionCollection FetchTableList(string connectionString, ActiveMapCodeGenConfigurationSection configuration)
        {
            TableDefinitionCollection tableList = new TableDefinitionCollection(configuration);
            string sql = 
                SqlScript.SqlTableColumns + SqlSeperator + Environment.NewLine + 
                SqlScript.SqlConstraints + SqlSeperator + Environment.NewLine + 
                SqlScript.SqlForeignKeys;

            using (SqlConnection connection = (SqlConnection) FetchConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                using (IDataReader reader = command.ExecuteReader())
                {
                    PopulateTableInfo(tableList, reader, configuration);
                    reader.NextResult();
                    UpdateColumnConstraints(tableList, reader);
                    reader.NextResult();
                    UpdateForeignKeyColumns(tableList, reader);
                    return tableList;
                }
            }
        }

        private ProcedureDefinitionCollection FetchProcedureList(string connectionString, ActiveMapCodeGenConfigurationSection configuration)
        {
            ProcedureDefinitionCollection procedureDefinitionCollection = new ProcedureDefinitionCollection(configuration);
            string sql = SqlScript.SqlSPParameters;

            using (SqlConnection connection = (SqlConnection)FetchConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                using (IDataReader reader = command.ExecuteReader())
                {
                    PopulateProcedureInfo(procedureDefinitionCollection, reader, configuration);
                    return procedureDefinitionCollection;
                }
            }
        }

        #endregion

        #region Private Static Helper Methods

        private static void AddCommandParameter(SqlCommand cmd, ColumnPredicate predicate, string param1Name, string param2Name) 
        {
            if ((predicate.Value == null || predicate.Value == DBNull.Value)
                && (predicate.Comparison == Comparison.NotEqual || predicate.Comparison == Comparison.Equal))
                    return;

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

                if (!cmd.Parameters.Contains("@" + param1Name))
                    cmd.Parameters.AddWithValue("@" + param1Name, value1);
                if (predicate.Comparison == Comparison.Between && !cmd.Parameters.Contains("@" + param2Name))
                    cmd.Parameters.AddWithValue("@" + param2Name, predicate.Value2);
        }

        private static void PopulateTableInfo(IWriteEnumerable<TableDefinition> tableList, IDataReader reader, ActiveMapCodeGenConfigurationSection configuration)
        {
            TableDefinition tableInfo = null;
            string lastOwner = null;
            string lastTable = null;

            while (reader.Read())
            {
                string newOwner = reader.GetString(0);
                string newTable = reader.GetString(1);

                if (newTable.StartsWith("sys", StringComparison.OrdinalIgnoreCase) ||
                    newTable.StartsWith("dt", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (tableInfo == null || (newOwner != lastOwner || newTable != lastTable))
                {
                    // TODO: Implement data source name usage
                    // TODO: Separate Views from Tables
                    tableInfo = new TableDefinition(null, newTable, newOwner, configuration)
                    {
                        IsReadOnly = reader.GetString(2) != SchemaValue.BaseTable
                    };

                    tableList.Add(tableInfo);
                }

                TableColumnDefinition columnInfo = new TableColumnDefinition(configuration)
                {
                    Name = reader.GetString(3), 
                    IsNullable = reader.GetString(4) == SchemaValue.Yes, 
                    MaxLength = reader.IsDBNull(6) ? 0 : reader.GetInt32(6), 
                    IsComputed = reader.GetInt32(7) == 1, 
                    IsIdentity = reader.GetInt32(8) == 1, 
                    Ordinal = Convert.ToInt32(reader[9]), 
                    Description = Convert.ToString(reader[10]), 
                    DefaultValue = Convert.ToString(reader[11])
                };

                columnInfo.DbType = GetDbType(reader.GetString(5), columnInfo.MaxLength);
                tableInfo.Columns.Add(columnInfo);

                lastOwner = tableInfo.Owner;
                lastTable = tableInfo.Name;
            }
        }

        private static void UpdateColumnConstraints(TableDefinitionCollection tableList, IDataReader reader)
        {
            while (reader.Read())
            {
                TableColumnDefinition columnInfo = tableList.FindColumn(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                if (columnInfo == null)
                    continue;

                switch (reader.GetString(3))
                {
                    case SchemaValue.ForeignKey:
                        columnInfo.IsForeignKey = true;
                        break;
                    case SchemaValue.PrimaryKey:
                        columnInfo.IsPrimaryKey = true;
                        columnInfo.ParentTable.PrimaryKey = columnInfo;
                        break;
                    case SchemaValue.Unique:
                        columnInfo.IsUnique = true;
                        break;
                }
            }
        }

        private static void UpdateForeignKeyColumns(TableDefinitionCollection tableList, IDataReader reader)
        {
            while (reader.Read())
            {
                TableColumnDefinition columnInfo = tableList.FindColumn(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                if (columnInfo != null && columnInfo.IsForeignKey)
                    columnInfo.ForeignKeyColumn = tableList.FindColumn(reader.GetString(3), reader.GetString(4), reader.GetString(5));
            }
        }

        private static void PopulateProcedureInfo(IWriteEnumerable<ProcedureDefinition> procedureList, IDataReader reader, ActiveMapCodeGenConfigurationSection configuration)
        {
            ProcedureDefinition procedureDefinition = null;
            string lastOwner = null;
            string lastTable = null;

            while (reader.Read())
            {
                string newOwner = reader.GetString(0);
                string newProcedure = reader.GetString(1);
                if (procedureDefinition == null || (newOwner != lastOwner || newProcedure != lastTable))
                {
                    procedureDefinition = new ProcedureDefinition(newProcedure, newOwner);
                    procedureList.Add(procedureDefinition);
                }

                ProcedureParameterDefinition parameterDefinition = new ProcedureParameterDefinition(configuration)
                {
                    Name = reader.GetString(2), 
                    Position = Convert.ToInt32(reader[3]), 
                    ParameterType = GetParameterType(reader.GetString(4)), 
                    IsResult = reader.GetString(5) == SchemaValue.Yes, 
                    MaxLength = reader.IsDBNull(7) ? 0 : reader.GetInt32(7)
                };

                parameterDefinition.DbType = GetDbType(reader.GetString(6), parameterDefinition.MaxLength);
                procedureDefinition.Parameters.Add(parameterDefinition);

                lastOwner = procedureDefinition.Owner;
                lastTable = procedureDefinition.Name;
            }
        }

        [SuppressMessage("Microsoft.Security", "CA2100:ReviewSqlQueriesForSecurityVulnerabilities")]
        private static DbCommand BuildTruncateCommand(string tableOwner, string tableName)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = string.Format("DELETE FROM [{0}].[{1}]", tableOwner, tableName), 
                CommandType = CommandType.Text
            };

            return cmd;
        }

        private static CommandBuilder GetSelectBuilder(QueryTree queryTree, ConstraintType constraintType)
        {
            SqlServerCommandBuilder commandBuilder = new SqlServerCommandBuilder(queryTree) {Distinct = queryTree.SelectWithDistinct};
            if(queryTree.Localized)
                commandBuilder.JoinInWhereClause = false;

            commandBuilder.Constrain(queryTree.SelectWithPaging ? Constraint.Page(queryTree.StartIndex, queryTree.PageSize) : queryTree.ResultConstraint);

            for (QueryTree current = queryTree; current != null; current = current.Next)
            {
                if (constraintType != ConstraintType.Count)
                    commandBuilder.AppendSelect(current.RetrieveQueryColumns());

                commandBuilder.AppendFrom(current.Table);
                foreach (JoinPredicate joinPredicate in current.Joins)
                    commandBuilder.AppendWhere(joinPredicate);
                foreach (ColumnPredicate columnPredicate in current.WherePredicates)
                    commandBuilder.AppendWhere(columnPredicate);
                foreach (ColumnPredicate columnPredicate in current.HavingPredicates)
                    commandBuilder.AppendHaving((columnPredicate));

                commandBuilder.AppendGroupBy(current.RetrieveGroupByColumns());
                commandBuilder.AppendOrderBy(current.RetrieveOrderByColumns());
            }
            return commandBuilder;
        }

        private static void AddColumnPredicates(ColumnPredicateCollection predicateCollection, StringBuilder sqlBuilder, SqlCommand command)
        {
            for (int i = 0; i < predicateCollection.Count; i++)
            {
                ColumnPredicate predicate = predicateCollection[i];

                if (predicate.OrToPreviousGroup)
                    sqlBuilder.Append(" OR ");
                else if (i >= 1)
                    sqlBuilder.Append(" AND ");

                if (predicate.IsGroup)
                {
                    sqlBuilder.Append("(");

                    for (ColumnPredicate current = predicate; current != null; current = current.NextInGroup)
                    {
                        IQueryableColumn column = current.Column;
                        string columText = string.Format("[{0}].[{1}].[{2}]", column.Table.Owner, column.Table.Name, column.Name);
                        
                        if (column.ColumnFormat != null)
                            columText = string.Format(column.ColumnFormat, columText);

                        if (current.Value == DBNull.Value || current.Value == null)
                            sqlBuilder.Append(columText + " " + GetComparisonOperator(current.Comparison, current.Value) + " NULL");
                        else if (current.Comparison == Comparison.Between)
                        {
                            string param1Name = predicateCollection.GetUniqueName(current);
                            string param2Name = predicateCollection.GetUniqueName(current);
                            sqlBuilder.AppendFormat("{0} {1} @{2} AND @{3}", columText, GetComparisonOperator(current.Comparison, current.Value), param1Name, param2Name);
                            AddCommandParameter(command, current, param1Name, param2Name);
                        }
                        else
                        {
                            string param1Name = predicateCollection.GetUniqueName(current);
                            sqlBuilder.AppendFormat("{0} {1} @{2}", columText, GetComparisonOperator(current.Comparison, current.Value), param1Name);
                            AddCommandParameter(command, current, param1Name, param1Name);
                        }

                        if (current.NextInGroup == null)
                            sqlBuilder.Append(")");

                        if (current.NextInGroup != null && current.OrNextPredicate)
                            sqlBuilder.Append(" OR ");
                        else if (current.NextInGroup != null)
                            sqlBuilder.Append(" AND ");
                    }
                }
                else
                {
                    IQueryableColumn column = predicate.Column;
                    string columText = string.Format("[{0}].[{1}].[{2}]", column.Table.Owner, column.Table.Name, column.Name);
                    
                    if (column.ColumnFormat != null)
                        columText = string.Format(column.ColumnFormat, columText);

                    if (predicate.Value == DBNull.Value || predicate.Value == null)
                        sqlBuilder.Append(columText + " " + GetComparisonOperator(predicate.Comparison, predicate.Value) + " NULL");
                    else if (predicate.Comparison == Comparison.Between)
                    {
                        string param1Name = predicateCollection.GetUniqueName(predicate);
                        string param2Name = predicateCollection.GetUniqueName(predicate);
                        sqlBuilder.AppendFormat("{0} {1} @{2} AND @{3}", columText, GetComparisonOperator(predicate.Comparison, predicate.Value), param1Name, param2Name);
                        AddCommandParameter(command, predicate, param1Name, param2Name);
                    }
                    else
                    {
                        string param1Name = predicateCollection.GetUniqueName(predicate);
                        sqlBuilder.AppendFormat("{0} {1} @{2}", columText, GetComparisonOperator(predicate.Comparison, predicate.Value), param1Name);
                        AddCommandParameter(command, predicate, param1Name, param1Name);
                    }

                    if (predicate.NextInGroup != null && predicate.OrNextPredicate)
                        sqlBuilder.Append(" OR ");
                    else if (predicate.NextInGroup != null)
                        sqlBuilder.Append(" AND ");
                }
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static DbType GetDbType(string dataType, int length)
        {
            Argument.Assert.IsNotNull(dataType, Argument.Names.dataType);

            switch (dataType)
            {
                case "int":
                    return DbType.Int32;
                case "bigint":
                    return DbType.Int64;
                case "smallint":
                case "tinyint":
                    return DbType.Int16;
                case "varchar":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "sql_variant":
                case "sysname":
                case "text":
                    return DbType.String;
                case "bit":
                    return DbType.Boolean;
                case "char":
                    return length == 1 ? DbType.AnsiStringFixedLength : DbType.String;
                case "datetime":
                case "smalldatetime":
                    return DbType.DateTime;
                case "decimal":
                case "float":
                case "numeric":
                case "real":
                    return DbType.Decimal;
                case "image":
                case "binary":
                case "timestamp":
                case "varbinary":
                    return DbType.Binary;
                case "money":
                case "smallmoney":
                    return DbType.Currency;
                case "uniqueidentifier":
                    return DbType.Guid;
                default:
                    return DbType.String;
            }
        }

        private static ParameterType GetParameterType(string parameterType)
        {
            switch (parameterType)
            {
                case "IN":
                    return ParameterType.In;
                case "OUT":
                    return ParameterType.Out;
                case "INOUT":
                    return ParameterType.InOut;
                default:
                    return ParameterType.Unknown;
            }
        }

        internal static string GetComparisonOperator(Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.Equal:
                    return "=";
                case Comparison.NotEqual:
                    return "<>";
                case Comparison.Greater:
                    return ">";
                case Comparison.GreaterOrEqual:
                    return ">=";
                case Comparison.Less:
                    return "<";
                case Comparison.LessOrEqual:
                    return "<=";
                case Comparison.StartsWith:
                case Comparison.EndsWith:
                case Comparison.Contains:
                    return "LIKE";
                case Comparison.DoesNotContain:
                case Comparison.DoesNotEndWith:
                case Comparison.DoesNotStartWith:
                    return "NOT LIKE";
                case Comparison.Between:
                    return "BETWEEN";
                default:
                    throw new ArgumentException("Unrecognized comparison.", "comparison");
            }
        }

        internal static string GetComparisonOperator(Comparison comparison, object value)
        {
            switch (comparison)
            {
                case Comparison.Equal:
                    if(value == null || value == DBNull.Value)
                        return "IS";
                    return "=";
                case Comparison.NotEqual:
                    if (value == null || value == DBNull.Value)
                        return "IS NOT";
                    return "<>";
                case Comparison.Greater:
                    return ">";
                case Comparison.GreaterOrEqual:
                    return ">=";
                case Comparison.Less:
                    return "<";
                case Comparison.LessOrEqual:
                    return "<=";
                case Comparison.StartsWith:
                case Comparison.EndsWith:
                case Comparison.Contains:
                    return "LIKE";
                case Comparison.DoesNotContain:
                case Comparison.DoesNotEndWith:
                case Comparison.DoesNotStartWith:
                    return "NOT LIKE";
                case Comparison.Between:
                    return "BETWEEN";
                default:
                    throw new ArgumentException("Unrecognized comparison.", "comparison");
            }
        }

        #endregion
    }
}
