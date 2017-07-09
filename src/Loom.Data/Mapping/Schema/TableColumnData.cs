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
using System.Diagnostics;
using System.Reflection;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [DebuggerDisplay("{Table.Owner,nq}.{Table.Name,nq}.{Name,nq}, ColumnProperties={ColumnProperties}, ForeignKeyColumn={ForeignKeyColumn == null ? \"None\" : ForeignKeyColumn.Table.Owner + \".\" + ForeignKeyColumn.Table.Name + \".\" + ForeignKeyColumn.Name, nq}")]
    internal sealed class TableColumnData : QueryColumn
    {
        #region Instance Fields

        private TableData table;
        private readonly string columnName;
        private readonly DbType dbType;
        private readonly int maxLength;
        private readonly ColumnProperties columnProperties;
        private readonly string defaultValue;
        private readonly string alias;
        private readonly QueryColumn foreignKeyColumn;
        private readonly IQueryableColumn localizedColumn;

        #endregion

        #region Property Accessors

        /// <summary>
        /// Gets the name of the column in the data source.
        /// </summary>
        public override string Name
        {
            get { return columnName; }
        }

        /// <summary>
        /// Gets the parent <see cref="TableData"/> implementation to which this instance belongs.
        /// </summary>
        public override TableData Table
        {
            get { return table; }
            set { table = value; }
        }

        /// <summary>
        /// Gets the alias, if any, that has been applied to this column.
        /// </summary>
        /// <remarks>
        /// Add an alias to this instance by calling <see cref="As(string)"/> and passing the value for the alias.
        /// </remarks>
        public override string Alias
        {
            get { return alias; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ColumnProperties ColumnProperties
        {
            get { return columnProperties; }
        }

        /// <summary>
        /// Gets the data source <see cref="DbType"/> of this instance.
        /// </summary>
        public override DbType DbType
        {
            get { return dbType; }
        }

        /// <summary>
        /// Gets the data source <see cref="DbType"/> of this instance.
        /// </summary>
        public override int MaxLength
        {
            get { return maxLength; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DefaultValue
        {
            get { return defaultValue; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ColumnFormat { get; set; }

        /// <summary>
        /// Gets an <see cref="IQueryableColumn"/> that represents a foreign key reference in the data source.
        /// </summary>
        public override IQueryableColumn ForeignKeyColumn
        {
            get { return foreignKeyColumn; }
        }

        /// <summary>
        /// Gets an <see cref="IQueryableColumn"/> that represents a foreign key reference in the data source.
        /// </summary>
        public override IQueryableColumn LocalizedColumn
        {
            get { return localizedColumn; }
        }

        public override IQueryableColumn LocalizeFallbackColumn { get; set; }

        #endregion

        #region .ctor

        private TableColumnData(TableData table, string columnName, DbType dbType, int maxLength, ColumnProperties columnProperties, string defaultValue, string alias, QueryColumn foreignKeyColumn, IQueryableColumn localizedColumn)
        {
            this.table = table;
            this.localizedColumn = localizedColumn;
            this.foreignKeyColumn = foreignKeyColumn;
            this.alias = alias;
            this.defaultValue = defaultValue;
            this.columnProperties = columnProperties;
            this.dbType = dbType;
            this.maxLength = maxLength;
            this.columnName = columnName;
        }

        #endregion

        /// <summary>
        /// Returns a copy of this <see cref="IQueryableColumn"/> with the supplied <paramref name="columnAlias"/> applied.
        /// </summary>
        /// <param name="columnAlias">The alias to associate with this instance in the generated query.</param>
        /// <returns>An <see cref="IQueryableColumn"/> with the specified <paramref name="columnAlias"/> applied.</returns>
        public override IQueryableColumn As(string columnAlias)
        {
            return new TableColumnData(table, columnName, dbType, maxLength, columnProperties, defaultValue, columnAlias, foreignKeyColumn, localizedColumn);
        }

        #region Factory Create Methods

        private static QueryColumn Create(ITableColumnDataProvider columnProvider)
        {
            return Create(columnProvider.GetTable(), columnProvider.DataRecordType, columnProvider.Name);
        }

        /// <summary>
        /// Creates a new <see cref="IQueryableColumn"/> from the supplied <paramref name="dataRecordType"/> and <paramref name="columnName"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload creates a column from the metadata contained in an <see cref="ActiveColumnAttribute"/>.
        /// The attribute should be applied to a member of the type represented by the <paramref name="dataRecordType"/> parameter.</para>
        /// <para>
        /// The <see cref="Type"/> specified by the <paramref name="dataRecordType"/> will be searched for an <see cref="ActiveColumnAttribute"/>
        /// that has a value in the <see cref="DynamicPropertyAttribute.Name"/> property matching the supplied <paramref name="columnName"/>.</para>
        /// </remarks>
        /// <param name="parentTable">An <see cref="TableData"/> implementation representing the table this column belongs to.</param>
        /// <param name="dataRecordType">A <see cref="Type"/> containing a member with an <see cref="ActiveColumnAttribute"/> applied to it.</param>
        /// <param name="columnName">The value specified in the <see cref="DynamicPropertyAttribute.Name"/> property of the <see cref="ActiveColumnAttribute"/>
        /// applied to the member.</param>
        /// <returns>A new <see cref="IQueryableColumn"/>.</returns>
        private static QueryColumn Create(TableData parentTable, Type dataRecordType, string columnName)
        {
            foreach (PropertyInfo property in dataRecordType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ActiveColumnAttribute attribute = (ActiveColumnAttribute)Attribute.GetCustomAttribute(property, typeof(ActiveColumnAttribute));
                if (attribute == null || attribute.Name != columnName) 
                    continue;
                
                return CreateFromAttribute(attribute, property, parentTable);
            }

            return null;
        }

        internal static IEnumerable<QueryColumn> CreateColumns(TableData parentTable, Type dataRecordType)
        {
            foreach (PropertyInfo property in dataRecordType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ActiveColumnAttribute attribute = (ActiveColumnAttribute)Attribute.GetCustomAttribute(property, typeof(ActiveColumnAttribute));
                if (attribute != null)
                    yield return CreateFromAttribute(attribute, property, parentTable);
            }
        }

        private static QueryColumn CreateFromAttribute(ActiveColumnAttribute attr, MemberInfo info, TableData parentTable) 
        {
            ForeignColumnAttribute foreignAttr = Attribute.GetCustomAttribute(info, typeof (ForeignColumnAttribute)) as ForeignColumnAttribute;
            LocalizableColumnAttribute localizeAttr = Attribute.GetCustomAttribute(info, typeof (LocalizableColumnAttribute)) as LocalizableColumnAttribute;

            return new TableColumnData(parentTable, attr.Name, attr.DbType, attr.MaxLength, attr.ColumnProperties, attr.DefaultValue, null, 
                                       foreignAttr == null ? null : Create(foreignAttr), localizeAttr == null ? null : Create(localizeAttr));
        }
        #endregion
    }
}
