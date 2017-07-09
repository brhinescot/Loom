#region Using Directives

using System.Data.Common;
using Loom.Data.Mapping.CodeGeneration;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping.Providers
{
    public interface IActiveDataProvider : IDataSessionProvider
    {
        DbCommand FetchCommand(CodeGenQuery codeGenQuery);
        DbCommand FetchCommand(QueryTree queryTree, ConstraintType constraintType);
        DbCommand FetchCommand(Insert insert);
        DbCommand FetchCommand(Update update);
        DbCommand FetchCommand(Delete delete, bool obliterate);
        DbCommand FetchCommand<T>(StoredProcedure<T> procedure) where T : StoredProcedure<T>, new();
        DatabaseSchema FetchDatabaseSchema(string connectionString, ActiveMapCodeGenConfigurationSection configuration);
    }
}