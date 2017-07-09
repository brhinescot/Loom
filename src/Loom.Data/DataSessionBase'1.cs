#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Transactions;
using Loom.Data.Entities;
using IsolationLevel = System.Transactions.IsolationLevel;

#endregion

namespace Loom.Data
{
    public abstract class DataSessionBase<TProvider> : IDisposable, IDataSessionBase where TProvider : class, IDataSessionProvider
    {
        private const string ObjectName = "DataSessionBase";

        [SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")] public static readonly string NewIdInsertParameterName = "NewId";

        private TextWriter logger;

        private IIdentity userIdentity;
        private bool userSet;

        protected DataSessionBase() { }

        protected DataSessionBase(TProvider provider, string connectionString)
        {
            Argument.Assert.IsNotNull(provider, nameof(provider));
            Argument.Assert.IsNotNullOrEmpty(connectionString, nameof(connectionString));

            State = new PersistentSessionConnectionState<TProvider>(provider, connectionString);
        }

        protected DataSessionBase(TProvider provider, DbConnection connection)
        {
            Argument.Assert.IsNotNull(provider, nameof(provider));
            Argument.Assert.IsNotNull(connection, nameof(connection));

            State = new PersistentSessionConnectionState<TProvider>(provider, connection);
        }

        protected bool IsDisposed { get; private set; }

        /// <summary>
        ///     Gets or sets the current user name.
        /// </summary>
        /// <remarks>
        ///     If not explicitly set, this property defaults to the login name of the
        ///     user who started the process.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public virtual IIdentity User
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);

                if (!userSet)
                {
                    IPrincipal threadPrincipal = Thread.CurrentPrincipal;
                    if (threadPrincipal != null)
                        userIdentity = threadPrincipal.Identity;

                    if (userIdentity == null)
                    {
                        WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
                        if (windowsIdentity != null)
                            userIdentity = windowsIdentity;
                    }
                }
                return userIdentity;
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);
                userIdentity = value;
                userSet = true;
            }
        }

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If a connection string is configured in the configuration file, the
        ///         <see cref="ConnectionString" /> property will default to that connection string.
        ///     </para>
        /// </remarks>
        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public string ConnectionString
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);

                return State.ConnectionString;
            }
            set => State.ConnectionString = value;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public TextWriter Logger
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);
                return logger;
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(ObjectName);
                logger = value;
            }
        }

        protected SessionConnectionState<TProvider> State { get; set; }

        #region IDataSessionBase Members

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
        public void EnlistConnection(IDbCommand command)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            if (command.Connection != null)
                return;

            command.Connection = State.DefaultConnection;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public IDataReader ExecuteReader(string commandText, params object[] parameters)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            using (DbCommand command = CreateCommand(commandText, parameters))
            {
                return ExecuteReader(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public object ExecuteScalar(string commandText, params object[] parameterValues)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNullOrEmpty(commandText, nameof(commandText));

            using (DbCommand command = CreateCommand(commandText, parameterValues))
            {
                return ExecuteScalar(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DataSet FetchDataSet(string commandText, params object[] parameterValues)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNullOrEmpty(commandText, nameof(commandText));

            using (DbCommand command = CreateCommand(commandText, parameterValues))
            {
                return FetchDataSet(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DbCommand CreateCommand(string commandText, params object[] parameterValues)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNullOrEmpty(commandText, nameof(commandText));

            return State.Provider.FetchCommand(commandText, parameterValues);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (IsDisposed)
                return;

            if (State != null)
                State.Dispose();

            if (logger != null)
                logger.Dispose();

            IsDisposed = true;
        }

        public static Collection<T> Filter<T>(IEnumerable<T> items, Predicate<T> filter)
        {
            Argument.Assert.IsNotNull(filter, nameof(filter));

            Collection<T> collection = new Collection<T>();
            foreach (T item in items)
                if (filter(item))
                    collection.Add(item);

            return collection;
        }

        /// <summary>
        ///     Performs the specified <paramref name="action" /> on each element of the <see cref="Collection{T}" />.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="action">
        ///     The <see cref="Action{TDataRecord}" />delegate to perform on each element of
        ///     the <see cref="Collection{T}" />.
        /// </param>
        /// <param name="suppressTransaction"></param>
        /// <see cref="System.Data.IsolationLevel" />
        /// ; otherwise the operation is not performed as part of a transaction.
        public void ForEach<T>(IEnumerable<T> items, Action<T> action, bool suppressTransaction)
        {
            if (!suppressTransaction)
            {
                Transaction transaction = Transaction.Current;
                ForEach(items, action, transaction == null ? IsolationLevel.Serializable : transaction.IsolationLevel);
            }

            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(action, nameof(action));

            foreach (T item in items)
                action(item);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public void ForEach<T>(IEnumerable<T> items, Action<T> action, IsolationLevel il = IsolationLevel.Serializable)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(action, nameof(action));

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransactionIsolationOption.FromIsolationLevel(il)))
                {
                    foreach (T item in items)
                        action(item);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("Exception during ForEach(IEnumerable<T>, Action<T>, IsolationLevel): {0}", ex));
                throw;
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public IDataReader ExecuteReader(DbCommand command, CommandBehavior behavior = CommandBehavior.Default)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(command, nameof(command));

            EnlistConnection(command);
            return command.ExecuteReader(behavior);
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int Execute(string commandText, params object[] parameterValues)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNullOrEmpty(commandText, nameof(commandText));

            using (DbCommand command = CreateCommand(commandText, parameterValues))
            {
                return Execute(command);
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public int Execute(DbCommand command)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(command, nameof(command));

            EnlistConnection(command);

            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteLog("Exception during Execute(DbCommand), CommandText = " + command.CommandText + ": " + ex);
                throw;
            }
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public object ExecuteScalar(DbCommand command)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(command, nameof(command));

            EnlistConnection(command);
            object result = command.ExecuteScalar();
            if (result == DBNull.Value)
                result = null;
            return result;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public DataSet FetchDataSet(DbCommand command)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(command, nameof(command));

            EnlistConnection(command);
            Debug.Assert(command.Connection.State == ConnectionState.Open);
            return FetchDataSet(State.Provider.FetchDataAdapter(command));
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        private DataSet FetchDataSet(IDataAdapter adapter)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(adapter, nameof(adapter));

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public IEntityAdapter CreateEntityAdapter(IDbCommand selectCommand)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            Argument.Assert.IsNotNull(selectCommand, nameof(selectCommand));

            EnlistConnection(selectCommand);
            IEntityAdapter adapter = new EntityAdapter(selectCommand);
            return adapter;
        }

        /// <exception cref="ObjectDisposedException">This instance has been disposed.</exception>
        public IEntityAdapter CreateEntityAdapter(string commandText, params object[] parameterValues)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ObjectName);

            return CreateEntityAdapter(CreateCommand(commandText, parameterValues));
        }

        [Conditional("DEBUG")]
        protected void WriteLog(string message)
        {
            if (Compare.IsNullOrEmpty(message))
                return;

            if (logger == null)
                Debug.WriteLine(message);
            else
                logger.WriteLine(message);
        }
    }
}