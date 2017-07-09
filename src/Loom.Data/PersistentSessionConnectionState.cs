#region Using Directives

using System;
using System.Data;
using System.Data.Common;

#endregion

namespace Loom.Data
{
    public class PersistentSessionConnectionState<T> : SessionConnectionState<T> where T : class, IDataSessionProvider
    {
        private readonly T provider;
        private string connectionString;
        private DbConnection dbConnection;

        public PersistentSessionConnectionState(T provider, string connectionString)
        {
            this.provider = provider;
            this.connectionString = connectionString;
        }

        public PersistentSessionConnectionState(T provider, DbConnection connection)
        {
            this.provider = provider;
            connectionString = connection.ConnectionString;
            dbConnection = connection;
        }

        public PersistentSessionConnectionState(Type type, string connectionString)
        {
            provider = CreateProvider(type);
            this.connectionString = connectionString;
        }

        public PersistentSessionConnectionState(Type type, DbConnection connection)
        {
            provider = CreateProvider(type);
            connectionString = connection.ConnectionString;
            dbConnection = connection;
        }

        public PersistentSessionConnectionState(string typeName, string connectionStringName)
        {
            provider = CreateProvider(typeName);
            connectionString = FindConnectionString(connectionStringName);
        }

        public override string ConnectionString
        {
            get => DefaultConnection.ConnectionString;
            set
            {
                connectionString = value;
                if (dbConnection == null)
                    dbConnection = provider.FetchConnection(connectionString);
                else if (dbConnection.ConnectionString != value)
                    dbConnection = provider.FetchConnection(connectionString);
            }
        }

        public override DbConnection DefaultConnection
        {
            get
            {
                if (dbConnection == null)
                    dbConnection = provider.FetchConnection(connectionString);

                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                return dbConnection;
            }
        }

        public override T Provider => provider;

        public override DbConnection OpenDynamicConnection()
        {
            DbConnection dynamicConnection = provider.FetchConnection(ConnectionString);
            dynamicConnection.Open();
            return dynamicConnection;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            if (dbConnection != null)
                dbConnection.Dispose();
        }
    }
}