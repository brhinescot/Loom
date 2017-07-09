#region Using Directives

using System;
using System.Data;
using System.Data.Common;

#endregion

namespace Loom.Data
{
    public interface IDataSessionBase
    {
        /// <summary>
        ///     Enlists the specified <paramref name="command" /> with an <see cref="IDbConnection" /> object.
        ///     This method also enlists an <see cref="IDbTransaction" /> with the specified <paramref name="command" />
        ///     if one is active in the same scope.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If the <see cref="IDbCommand.Connection" /> property has already been initialized this
        ///         method returns without performing any changes to the <paramref name="command" />
        ///     </para>
        /// </remarks>
        /// <param name="command">
        ///     The <see cref="DbCommand" /> to which the <see cref="IDbConnection" /> and
        ///     <see cref="IDbTransaction" /> are attached.
        /// </param>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        void EnlistConnection(IDbCommand command);

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        IDataReader ExecuteReader(string commandText, params object[] parameters);

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        object ExecuteScalar(string commandText, params object[] parameterValues);

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        DataSet FetchDataSet(string commandText, params object[] parameterValues);

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        DbCommand CreateCommand(string commandText, params object[] parameterValues);
    }
}