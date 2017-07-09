namespace Loom.Data.Mapping.Schema
{
    public interface IQueryableColumn : IDbColumn
    {
        /// <summary>
        /// </summary>
        ITable Table { get; set; }

        /// <summary>
        ///     Gets a reference to an <see cref="IQueryableColumn" /> instance that represents a foreign key reference
        ///     in the data source.
        /// </summary>
        IQueryableColumn ForeignKeyColumn { get; }

        /// <summary>
        /// </summary>
        IQueryableColumn LocalizedColumn { get; }

        /// <summary>
        /// </summary>
        IQueryableColumn LocalizeFallbackColumn { get; set; }

        /// <summary>
        ///     Gets the alias, if any, that has been applied to this column.
        /// </summary>
        /// <remarks>
        ///     Add an alias to this instance by calling <see cref="IQueryableColumn.As" /> and passing the value for the alias.
        /// </remarks>
        string Alias { get; }

        /// <summary>
        /// </summary>
        ColumnProperties ColumnProperties { get; }

        /// <summary>
        /// </summary>
        string ColumnFormat { get; set; }

        /// <summary>
        /// </summary>
        string DefaultValue { get; }

        /// <summary>
        ///     Returns a copy of this <see cref="IQueryableColumn" /> with the supplied <paramref name="columnAlias" /> applied.
        /// </summary>
        /// <param name="columnAlias">The alias to associate with this instance in the generated query.</param>
        /// <returns>An <see cref="IQueryableColumn" /> with the specified <paramref name="columnAlias" /> applied.</returns>
        IQueryableColumn As(string columnAlias);
    }
}