#region Using Directives

using System.Data;
using System.Data.SqlClient;
using Loom.Data.Entities;

#endregion

namespace Loom.Data.SqlClient
{
    public class SqlEntityAdapter : EntityAdapter
    {
        public SqlEntityAdapter(string connectionString, string commandText) : this(null, connectionString, commandText) { }

        public SqlEntityAdapter(string connectionString, string commandText, params object[] parameterValues) : this(null, connectionString, commandText, parameterValues) { }

        public SqlEntityAdapter(SqlTransaction transaction, string connectionString, string commandText)
        {
            SelectCommand = new SqlCommand(commandText, new SqlConnection(connectionString), transaction);
        }

        public SqlEntityAdapter(SqlTransaction transaction, string connectionString, string commandText, params object[] parameterValues)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = new SqlConnection(connectionString),
                Transaction = transaction
            };
            FormatCommand(command, commandText, parameterValues);

            SelectCommand = command;
        }

        public CommandType CommandType
        {
            get => SelectCommand.CommandType;
            set => SelectCommand.CommandType = value;
        }

        protected override void Dispose(bool disposing)
        {
            SelectCommand.Connection.Dispose();
            SelectCommand.Dispose();
            base.Dispose(disposing);
        }
    }
}