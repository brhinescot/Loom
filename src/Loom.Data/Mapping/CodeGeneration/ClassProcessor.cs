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
using System.Text;
using Humanizer;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    internal class ClassProcessor
    {
        #region Instance Fields

        private readonly CodeGenSession session;

//        private readonly ReadOnlyProcessorState readOnlyProcessorState = new ReadOnlyProcessorState();
        private readonly ReadWriteProcessorState readWriteProcessorState = new ReadWriteProcessorState();
        private IClassProcessorState processorState;
        private readonly object lockObject = new object();

        private readonly string deletedColumn;
        private readonly string modifiedOnColumn;
        private readonly string modifiedByColumn;
        private readonly string createdOnColumn;
        private readonly string createdbyColumn;
        private readonly string serializationNamespace;
        private readonly string allowNillable;

        #endregion

        public ClassProcessor(CodeGenSession dataSession)
        {
            session = dataSession;

            deletedColumn = session.Configuration.AuditMapping.DeletedColumn;
            modifiedOnColumn = session.Configuration.AuditMapping.ModifiedOnColumn;
            modifiedByColumn = session.Configuration.AuditMapping.ModifiedByColumn;
            createdOnColumn = session.Configuration.AuditMapping.CreatedOnColumn;
            createdbyColumn = session.Configuration.AuditMapping.CreatedbyColumn;
            serializationNamespace = session.Configuration.CodeGen.SerializationNamespace;
            allowNillable = session.Configuration.CodeGen.UseNullableTypes.ToString().ToLower();

        }

        public string GenerateClass(TableDefinition table)
        {
            lock (lockObject)
            {
                processorState = readWriteProcessorState;
                return GenerateClassInternal(table);
            }
        }

        private string GenerateClassInternal(TableDefinition table)
        {
            StringBuilder columnBuilder = new StringBuilder();
            StringBuilder fieldBuilder = new StringBuilder();
            fieldBuilder.AppendLine();
            foreach (TableColumnDefinition column in table.Columns)
            {
                if (column.IsForeignKey && column.ForeignKeyColumn != null && column.ForeignKeyColumn.ParentTable.IsEnum)
                    columnBuilder.AppendLine(Indent.Three + "public static QueryColumn " + column.ForeignKeyColumn.ParentTable.ToPascalCase() + "\r\n            {");
                else
                    columnBuilder.AppendLine(Indent.Three + "public static QueryColumn " + column.ToPascalCase() + "\r\n            {");

                columnBuilder.AppendLine(Indent.Four + "get { return FetchColumn(\"" + column.Name + "\"); }");
                columnBuilder.AppendLine(Indent.Three + "}");
                columnBuilder.AppendLine();

                if (column.IsForeignKey && column.ForeignKeyColumn != null && column.ForeignKeyColumn.ParentTable.IsEnum)
                {
                    string enumType = column.ForeignKeyColumn.ParentTable.ToPascalCase();
                    fieldBuilder.AppendLine(Indent.Two + "private " + enumType + " _" + column.Name.ToCamelCase() + ";");
                }
                else
                    fieldBuilder.AppendLine(Indent.Two + "private " + column.GetDataTypeShort() + " _" + column.Name.ToCamelCase() + ";");
            }

            string className = table.ToPascalCase().Singularize(Plurality.CouldBeEither);
            var tableConfig = session.GetTableConfiguration(table.Owner, table.Name);
            if(tableConfig != null)
            {
                if (!Compare.IsNullOrEmpty(tableConfig.RenameTo))
                    className = tableConfig.RenameTo;
                if (!Compare.IsNullOrEmpty(tableConfig.AddPrefix))
                    className = tableConfig.AddPrefix + className;
                if (!Compare.IsNullOrEmpty(tableConfig.AddSuffix))
                    className += tableConfig.AddSuffix;
            }

            return processorState.ClassTemplate.
                Replace(Tokens.SchemaAttributes, GenerateTableAttribute(table)).
                Replace(Tokens.AdditionalAttributes, string.Empty).
                Replace(Tokens.ClassName, className).
                Replace(Tokens.SchemaName, table.Owner).
                Replace(Tokens.TableName, table.Name).
                Replace(Tokens.PrimaryKeyColumn, table.PrimaryKey == null ? "null" : table.ToPascalCase() + "Columns." + table.PrimaryKey.ToPascalCase()).
                Replace(Tokens.Properties, GenerateProperties(table)).
                Replace(Tokens.ColumnList, columnBuilder.ToString()).
                Replace(Tokens.TargetNamespace, serializationNamespace).
                Replace(Tokens.AllowNillable, allowNillable).
                Replace(Tokens.MemberFields, fieldBuilder.ToString());
        }

        private string GenerateTableAttribute(TableDefinition table)
        {
            StringBuilder attrBuilder = new StringBuilder();
            attrBuilder.AppendFormat("ActiveTable(\"{0}\", \"{1}\"", table.Owner, table.Name);
            if (table.PrimaryKey != null)
                attrBuilder.Append(", \"" + table.PrimaryKey.Name + "\"");
            if (table.IsReadOnly)
                attrBuilder.Append(", ReadOnly=true");

            foreach (TableColumnDefinition column in table.Columns)
            {
                if (Compare.AreSameOrdinal(deletedColumn, column.Name))
                    attrBuilder.AppendFormat(", DeletedColumn=\"{0}\"", column.Name);
                if (Compare.AreSameOrdinal(modifiedOnColumn, column.Name))
                    attrBuilder.AppendFormat(", ModifiedOnColumn=\"{0}\"", column.Name);
                if (Compare.AreSameOrdinal(modifiedByColumn, column.Name))
                    attrBuilder.AppendFormat(", ModifiedByColumn=\"{0}\"", column.Name);
                if (Compare.AreSameOrdinal(createdOnColumn, column.Name))
                    attrBuilder.AppendFormat(", CreatedOnColumn=\"{0}\"", column.Name);
                if (Compare.AreSameOrdinal(createdbyColumn, column.Name))
                    attrBuilder.AppendFormat(", CreatedByColumn=\"{0}\"", column.Name);
            }
            attrBuilder.Append(")");
            return attrBuilder.ToString();
        }

        private static string GeneratePropertyAttribute(TableColumnDefinition column)
        {
            StringBuilder attrBuilder = new StringBuilder();
            attrBuilder.Append("ActiveColumn(\"" + column.Name + "\", DbType." + column.DbType + ", ");
            AddColumnPropertyValues(attrBuilder, column);

            attrBuilder.Append(")");
            return attrBuilder.ToString();
        }

        private static void AddColumnPropertyValues(StringBuilder attributeBuilder, TableColumnDefinition column) 
        {
            bool hasProperty = false;
            if (column.IsComputed)
            {
                attributeBuilder.Append("ColumnProperties.Computed");
                hasProperty = true;
            }
            if (column.IsForeignKey)
            {
                if (hasProperty)
                    attributeBuilder.Append(" | ");
                attributeBuilder.Append("ColumnProperties.ForeignKey");
                hasProperty = true;
            }
            if (column.IsIdentity)
            {
                if (hasProperty)
                    attributeBuilder.Append(" | ");
                attributeBuilder.Append("ColumnProperties.Identity");
                hasProperty = true;
            }
            if (column.IsNullable)
            {
                if (hasProperty)
                    attributeBuilder.Append(" | ");
                attributeBuilder.Append("ColumnProperties.Nullable");
                hasProperty = true;
            }
            if (column.IsPrimaryKey)
            {
                if (hasProperty)
                    attributeBuilder.Append(" | ");
                attributeBuilder.Append("ColumnProperties.PrimaryKey");
                hasProperty = true;
            }
            if (column.IsUnique)
            {
                if (hasProperty)
                    attributeBuilder.Append(" | ");
                attributeBuilder.Append("ColumnProperties.Unique");
                hasProperty = true;
            }
            if (!hasProperty)
            {
                attributeBuilder.Append("ColumnProperties.None");
            }

            if (column.Ordinal >= 0)
                attributeBuilder.Append(", Ordinal=" + column.Ordinal);
            if (column.MaxLength >= 0)
                attributeBuilder.Append(", MaxLength=" + column.MaxLength);
            if (!Compare.IsNullOrEmpty(column.DefaultValue))
                attributeBuilder.Append(", DefaultValue=\"" + column.DefaultValue + "\"");
        }

        // FEATURE: Allow configuration of default xml serialization type for properties (Attribute/Element).
        // FEATURE: Allow configuration of individual property's xml serialization type.
        private string GenerateProperties(TableDefinition table)
        {
            StringBuilder builder = new StringBuilder();

            foreach (TableColumnDefinition column in table.Columns)
            {
                string foreignColumnAttribute = null;
                string localizableColumnAttribute = null;

                if (column.IsForeignKey && column.ForeignKeyColumn != null)
                {
                    StringBuilder columnPropertyBuilder = new StringBuilder();
                    AddColumnPropertyValues(columnPropertyBuilder, column.ForeignKeyColumn);
                    if (Compare.AreSameOrdinalIgnoreCase(column.ForeignKeyColumn.ParentTable.Owner, "dbo"))
                        foreignColumnAttribute = string.Format("        [ForeignColumn(\"{0}\", typeof({1}.{2}), ColumnProperties={3}, DbType=DbType.{4})]{5}", column.ForeignKeyColumn.Name, session.Configuration.CodeGen.BaseNamespace, column.ForeignKeyColumn.ParentTable.Name.ToPascalCase().Singularize(Plurality.CouldBeEither), columnPropertyBuilder, column.DbType, Environment.NewLine);
                    else
                        foreignColumnAttribute = string.Format("        [ForeignColumn(\"{0}\", typeof({1}.{2}.{3}), ColumnProperties={4}, DbType=DbType.{5})]{6}", column.ForeignKeyColumn.Name, session.Configuration.CodeGen.BaseNamespace, column.ForeignKeyColumn.ParentTable.Owner.ToPascalCase(), column.ForeignKeyColumn.ParentTable.Name.ToPascalCase().Singularize(Plurality.CouldBeEither), columnPropertyBuilder, column.DbType, Environment.NewLine);
                }

                if (column.IsLocalizable && column.LocalizationColumn != null)
                {
                    StringBuilder columnPropertyBuilder = new StringBuilder();
                    AddColumnPropertyValues(columnPropertyBuilder, column.LocalizationColumn);
                    if (Compare.AreSameOrdinalIgnoreCase(column.LocalizationColumn.ParentTable.Owner, "dbo"))
                        localizableColumnAttribute = string.Format("        [LocalizableColumn(\"{0}\", typeof({1}.{2}))]{3}", column.LocalizationColumn.Name, session.Configuration.CodeGen.BaseNamespace, column.LocalizationColumn.ParentTable.Name, Environment.NewLine);
                    else
                        localizableColumnAttribute = string.Format("        [LocalizableColumn(\"{0}\", typeof{1}.({2}.{3}))]{4}", column.LocalizationColumn.Name, session.Configuration.CodeGen.BaseNamespace, column.LocalizationColumn.ParentTable.Owner, column.LocalizationColumn.ParentTable.Name, Environment.NewLine);
                }

                builder.AppendLine();
                string propertyName = column.ToPascalCase(CodeFormatOptions.RemoveFKPrefix);
                string xmlElementName = propertyName;
                if (column.Name == table.Name)
                    propertyName += "_";
                // Create Enum Property
                if (column.IsForeignKey && column.ForeignKeyColumn != null && column.ForeignKeyColumn.ParentTable.IsEnum)
                {
                    string enumType = column.ForeignKeyColumn.ParentTable.ToPascalCase();

                    builder.AppendLine(processorState.EnumPropertyTemplate.
                                           Replace(Tokens.FieldName, column.Name.ToCamelCase()).
                                           Replace(Tokens.XmlAttribute, enumType).
                                           Replace(Tokens.AdditionalAttributes, foreignColumnAttribute + localizableColumnAttribute).
                                           Replace(Tokens.EnumType, enumType).
                                           Replace(Tokens.PropertyName, enumType).
                                           Replace(Tokens.Summary, column.Description).
                                           Replace(Tokens.SchemaAttributes, GeneratePropertyAttribute(column)).
                                           Replace(Tokens.ColumnName, column.Name));
                }
                else
                {
                    builder.AppendLine(processorState.PropertyTemplate.
                                           Replace(Tokens.FieldName, column.Name.ToCamelCase()).
                                           Replace(Tokens.XmlAttribute, xmlElementName).
                                           Replace(Tokens.AdditionalAttributes, foreignColumnAttribute + localizableColumnAttribute).
                                           Replace(Tokens.DataTypeShort, column.GetDataTypeShort()).
                                           Replace(Tokens.DataTypeLong, column.GetDataTypeLong()).
                                           Replace(Tokens.PropertyName, propertyName).
                                           Replace(Tokens.Summary, column.Description).
                                           Replace(Tokens.SchemaAttributes, GeneratePropertyAttribute(column)).
                                           Replace(Tokens.ColumnName, column.Name));
                }
            }

            return builder.ToString();
        }
    }

    internal struct Indent
    {
        internal const string One = "    ";
        internal const string Two = "        ";
        internal const string Three = "            ";
        internal const string Four = "                ";
    }
}
