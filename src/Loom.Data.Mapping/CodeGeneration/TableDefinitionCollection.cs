#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable]
    public sealed class TableDefinitionCollection : SchemaDefinitionCollection<TableDefinition>
    {
        public TableDefinitionCollection(ActiveMapCodeGenConfigurationSection configuration) : base(configuration) { }

        protected override bool ExplicitInclude => Configuration.Tables.ExplicitInclude;

        public void EnsureLocalizableColumns()
        {
            foreach (TableDefinition item in this)
            foreach (TablesElement table in Configuration.Tables)
            {
                if (table.Owner != item.Owner || table.Name != item.Name)
                    continue;

                if (Compare.IsNullOrEmpty(table.LocalizationTableName))
                    continue;
                if (Compare.IsNullOrEmpty(table.LocalizableColumns))
                    continue;

                foreach (string columnName in table.LocalizableColumns.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    TableColumnDefinition column = item.Columns.FindColumn(columnName);
                    if (column == null)
                        continue;

                    TableColumnDefinition translatedColumn = column.LocalizationColumn = FindColumn(table.LocalizationTableSchema, table.LocalizationTableName, columnName);
                    if (translatedColumn == null)
                        continue;

                    column.IsLocalizable = true;
                    column.LocalizationColumn = translatedColumn;
                }
            }
        }

        protected override IEnumerable<ISchema> GetExcludedItems()
        {
            foreach (TablesElement table in Configuration.Tables)
                if (table.Exclude)
                    yield return new Schema(table.Owner, table.Name);
        }

        protected override IEnumerable<string> GetSchemaExcludes()
        {
            if (Configuration.CodeGen.SchemaExcludes != null)
                foreach (string s in Configuration.CodeGen.SchemaExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;

            if (Configuration.Tables.SchemaExcludes != null)
                foreach (string s in Configuration.Tables.SchemaExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetPrefixExcludes()
        {
            if (Configuration.CodeGen.PrefixExcludes != null)
                foreach (string s in Configuration.CodeGen.PrefixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;

            if (Configuration.Tables.PrefixExcludes != null)
                foreach (string s in Configuration.Tables.PrefixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetSuffixExcludes()
        {
            if (Configuration.CodeGen.SuffixExcludes != null)
                foreach (string s in Configuration.CodeGen.SuffixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;

            if (Configuration.Tables.SuffixExcludes != null)
                foreach (string s in Configuration.Tables.SuffixExcludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetSchemaIncludes()
        {
            if (Configuration.Tables.SchemaIncludes != null)
                foreach (string s in Configuration.Tables.SchemaIncludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetPrefixIncludes()
        {
            if (Configuration.Tables.PrefixIncludes != null)
                foreach (string s in Configuration.Tables.PrefixIncludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        protected override IEnumerable<string> GetSuffixIncludes()
        {
            if (Configuration.Tables.SuffixIncludes != null)
                foreach (string s in Configuration.Tables.SuffixIncludes.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                    yield return s;
        }

        public TableColumnDefinition FindColumn(string schemaName, string tableName, string columnName)
        {
            TableDefinition info = FindTable(schemaName, tableName);
            return info == null ? null : info.Columns.FindColumn(columnName);
        }

        public TableDefinition FindTable(string schemaName, string tableName)
        {
            string key = schemaName + tableName;
            return !InnerList.ContainsKey(key) ? null : InnerList[key];
        }
    }
}