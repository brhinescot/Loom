#region Using Directives

using System.Data;
using System.Data.Common;

#endregion

namespace Loom.Data
{
    public interface IDataSessionProvider
    {
        DbConnection FetchConnection(string connectionString);
        IDataAdapter FetchDataAdapter(DbCommand command);
        DbCommand FetchCommand(string commandText, params object[] parameters);
    }
}