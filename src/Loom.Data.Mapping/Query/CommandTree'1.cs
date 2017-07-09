#region Using Directives

using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    /// <summary>
    ///     Represents a dynamic SQL statement to execute against a data source. Provides a base
    ///     class for database-specific classes that represent queries.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommandTree<T> where T : CommandTree<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandTree{T}" /> class.
        /// </summary>
        /// <param name="table">The table to query.</param>
        protected CommandTree(ITable table)
        {
            Argument.Assert.IsNotNull(table, nameof(table));

            Table = table;
        }

        internal ColumnPredicateCollection WherePredicates { get; } = new ColumnPredicateCollection(ParameterNameGenerator.GetShortName());

        internal bool HasWhereClause => !Compare.IsNullOrEmpty(WherePredicates);

        /// <summary>
        ///     Gets the <see cref="ISchema" /> instance representing a table in a data source to be queried.
        /// </summary>
        public ITable Table { get; set; }
    }
}