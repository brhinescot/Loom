#region Using Directives

using System.Data.Common;

#endregion

namespace Loom.Data.SqlClient
{
    public class SqlDataSession : DataSessionBase<SqlDataSessionProvider>
    {
        public SqlDataSession(string connectionString) : base(new SqlDataSessionProvider(), connectionString) { }
        public SqlDataSession(DbConnection connection) : base(new SqlDataSessionProvider(), connection) { }
    }
}