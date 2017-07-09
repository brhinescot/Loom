#region Using Directives

using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     Basic low level Data Access Layer
    /// </summary>
    internal sealed class SqlDataAccess : SqlDataAccessBase
    {
        public SqlDataAccess(string connectionString)
        {
            DbProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
            ConnectionString = connectionString;
        }

        /// <summary>
        ///     Creates a Command object and opens a connection
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override DbCommand CreateCommand(string sql, CommandType commandType, params DbParameter[] parameters)
        {
            SetError();

            DbCommand command = DbProvider.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;

            try
            {
                if (Transaction != null)
                {
                    command.Transaction = Transaction;
                    command.Connection = Transaction.Connection;
                }
                else
                {
                    if (!OpenConnection())
                        throw new DataException("Unable to open the database " + command.Connection.Database + ".");

                    command.Connection = Connection;
                }
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return null;
            }

            if (parameters != null)
                foreach (DbParameter parm in parameters)
                    command.Parameters.Add(parm);

            return command;
        }

        /// <summary>
        ///     Creates a Sql Parameter for the specific provider
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override DbParameter CreateParameter(string parameterName, object value)
        {
            DbParameter parm = DbProvider.CreateParameter();
            parm.ParameterName = parameterName;
            if (value == null)
                value = DBNull.Value;
            parm.Value = value;
            return parm;
        }

        /// <summary>
        ///     Executes a SQL Command object and returns a SqlDataReader object
        /// </summary>
        /// <param name="command">Command should be created with GetSqlCommand and open connection</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <returns>A SqlDataReader. Make sure to call Close() to close the underlying connection.</returns>
        public override DbDataReader ExecuteReader(DbCommand command, params DbParameter[] parameters)
        {
            SetError();

            if (command.Connection == null || command.Connection.State != ConnectionState.Open)
            {
                if (!OpenConnection())
                    return null;

                command.Connection = Connection;
            }

            foreach (DbParameter parameter in parameters)
                command.Parameters.Add(parameter);

            DbDataReader reader;
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                SetError(ex.Message, ex.Number);
                CloseConnection(command);
                return null;
            }

            return reader;
        }

        /// <summary>
        ///     Closes a connection
        /// </summary>
        /// <param name="command"></param>
        public override void CloseConnection(DbCommand command)
        {
            if (Transaction != null)
                return;

            if (command.Connection != null &&
                command.Connection.State == ConnectionState.Open)
                command.Connection.Close();

            Connection = null;
        }

        /// <summary>
        ///     Closes an active connection. If a transaction is pending the
        ///     connection is held open.
        /// </summary>
        public override void CloseConnection()
        {
            if (Transaction != null)
                return;

            if (Connection != null &&
                Connection.State == ConnectionState.Open)
                Connection.Close();

            Connection = null;
        }

        /// <summary>
        ///     Opens a Sql Connection based on the connection string.
        ///     Called internally but externally accessible. Sets the internal
        ///     DbConnection property.
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            try
            {
                if (Connection == null)
                    if (ConnectionString.Contains(";"))
                    {
                        Connection = DbProvider.CreateConnection();
                        Connection.ConnectionString = ConnectionString;
                    }
                    else
                    {
                        // Assume it's a connection string value
                        Connection = DbProvider.CreateConnection();
                        Connection.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
                    }

                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }
    }
}