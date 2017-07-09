#region Using Directives

using System;
using System.Data;
using System.Data.Common;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     Basic low level Data Access Layer
    /// </summary>
    internal abstract class SqlDataAccessBase : IDisposable
    {
        /// <summary>
        ///     The internally used dbProvider
        /// </summary>
        public DbProviderFactory DbProvider;

        /// <summary>
        ///     The SQL Connection object used for connections
        /// </summary>
        public DbConnection Connection { get; set; }

        /// <summary>
        ///     ConnectionString for the data access component
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     An error message if a method fails
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Optional error number returned by failed SQL commands
        /// </summary>
        public int ErrorNumber { get; set; }

        /// <summary>
        ///     A SQL Transaction object that may be active. You can
        ///     also set this object to
        /// </summary>
        public DbTransaction Transaction { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            if (Connection != null)
                CloseConnection();
        }

        #endregion

        /// <summary>
        ///     Closes a connection
        /// </summary>
        /// <param name="command"></param>
        public abstract void CloseConnection(DbCommand command);

        /// <summary>
        ///     Closes an active connection. If a transaction is pending the
        ///     connection is held open.
        /// </summary>
        public abstract void CloseConnection();

        /// <summary>
        ///     Creates a Command object and opens a connection
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract DbCommand CreateCommand(string sql, CommandType commandType, params DbParameter[] parameters);

        /// <summary>
        ///     Used to create named parameters to pass to commands or the various
        ///     methods of this class.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract DbParameter CreateParameter(string parameterName, object value);

        /// <summary>
        ///     Executes a SQL Command object and returns a SqlDataReader object
        /// </summary>
        /// <param name="command">Command should be created with GetSqlCommand and open connection</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <returns>A SqlDataReader. Make sure to call Close() to close the underlying connection.</returns>
        public abstract DbDataReader ExecuteReader(DbCommand command, params DbParameter[] parameters);

        /// <summary>
        ///     Creates a Command object and opens a connection
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbCommand CreateCommand(string sql, params DbParameter[] parameters)
        {
            return CreateCommand(sql, CommandType.Text, parameters);
        }

        /// <summary>
        ///     Executes a SQL command against the server and returns a DbDataReader
        /// </summary>
        /// <param name="sql">Sql String</param>
        /// <param name="parameters">Any SQL parameters </param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string sql, params DbParameter[] parameters)
        {
            DbCommand command = CreateCommand(sql, parameters);
            return ExecuteReader(command);
        }

        /// <summary>
        ///     Sets the error message for the failure operations
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorNumber"></param>
        protected void SetError(string message, int errorNumber)
        {
            if (string.IsNullOrEmpty(message))
            {
                ErrorMessage = string.Empty;
                ErrorNumber = 0;
                return;
            }

            ErrorMessage = message;
            ErrorNumber = errorNumber;
        }

        /// <summary>
        ///     Sets the error message and error number.
        /// </summary>
        /// <param name="message"></param>
        protected void SetError(string message)
        {
            SetError(message, 0);
        }

        /// <summary>
        ///     Sets the error message for failure operations.
        /// </summary>
        protected void SetError()
        {
            SetError(null);
        }
    }
}