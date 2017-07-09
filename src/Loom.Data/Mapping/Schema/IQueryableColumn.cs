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

using System.Data;

namespace Loom.Data.Mapping.Schema
{
    public interface IDbColumn 
    {
        /// <summary>
        /// Gets the name of the column in the data source.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the data source <see cref="IQueryableColumn.DbType"/> of this instance.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        DbType DbType { get; }

        int MaxLength { get; }
    }

    public interface IQueryableColumn : IDbColumn
    {
        TableData Table { get; set; }

        /// <summary>
        /// Gets a reference to an <see cref="IQueryableColumn"/> instance that represents a foreign key reference
        /// in the data source.
        /// </summary>
        IQueryableColumn ForeignKeyColumn { get; }

        IQueryableColumn LocalizedColumn { get; }

        IQueryableColumn LocalizeFallbackColumn { get; set; }

        /// <summary>
        /// Gets the alias, if any, that has been applied to this column.
        /// </summary>
        /// <remarks>
        /// Add an alias to this instance by calling <see cref="IQueryableColumn.As"/> and passing the value for the alias.
        /// </remarks>
        string Alias { get; }

        ColumnProperties ColumnProperties { get; }

        /// <summary>
        /// Returns a copy of this <see cref="IQueryableColumn"/> with the supplied <paramref name="columnAlias"/> applied.
        /// </summary>
        /// <param name="columnAlias">The alias to associate with this instance in the generated query.</param>
        /// <returns>An <see cref="IQueryableColumn"/> with the specified <paramref name="columnAlias"/> applied.</returns>
        IQueryableColumn As(string columnAlias);

        string ColumnFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DefaultValue { get; }
    }
}
