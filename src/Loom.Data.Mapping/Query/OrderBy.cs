#region Using Directives

using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    public sealed class OrderBy
    {
        internal OrderBy(IQueryableColumn column, OrderByDirection direction)
        {
            Argument.Assert.IsNotNull(column, nameof(column));

            Column = column;
            Direction = direction;
        }

        internal IQueryableColumn Column { get; }

        internal OrderByDirection Direction { get; set; }
    }
}