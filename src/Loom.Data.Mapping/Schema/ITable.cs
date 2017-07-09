#region Using Directives

using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping.Schema
{
    public interface ITable : ISchema
    {
        bool HasPrimaryKey { get; }
        PrimaryKeys PrimaryKey { get; }
        bool HasDeletedColumn { get; }
        QueryColumn DeletedColumn { get; }
        bool HasIdentityColumn { get; }
        QueryColumn IdentityColumn { get; }
        QueryableColumns Columns { get; }
        QueryColumn CreatedByColumn { get; }
        QueryColumn CreatedOnColumn { get; }
        QueryColumn ModifiedByColumn { get; }
        QueryColumn ModifiedOnColumn { get; }
        bool HasCreatedByColumn { get; }
        bool HasCreatedOnColumn { get; }
        bool HasModifiedByColumn { get; }
        bool HasModifiedOnColumn { get; }
        QueryColumn FindColumn(string columnName);
        ColumnPredicate CreatePredicate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new();
    }
}