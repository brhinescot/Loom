#region Using Directives

#endregion

namespace Loom.Data.Mapping.Query
{
    public sealed class QueryCondition<T> : CommandCondition<T, EntitySet<T>, QueryCondition<T>> where T : DataRecord<T>, new()
    {
        public QueryCondition(EntitySet<T> host, ColumnPredicateCollection predicates) : base(host, predicates) { }
    }
}