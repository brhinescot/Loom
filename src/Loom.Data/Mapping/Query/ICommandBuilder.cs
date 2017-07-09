using Loom.Data.Mapping.Schema;

namespace Loom.Data.Mapping.Query
{
    public interface ICommandBuilder
    {
        JoinPredicateCollection JoinPredicates { get; }
        ColumnPredicateCollection WherePredicates { get; }
        ColumnPredicateCollection HavingPredicates { get; }
        bool JoinInWhereClause { get; set; }
        void AppendFrom(TableData table);
    }
}
