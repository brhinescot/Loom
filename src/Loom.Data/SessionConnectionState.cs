#region Using Directives

using System;
using System.Configuration;
using System.Data.Common;

#endregion

namespace Loom.Data
{
    public abstract class SessionConnectionState<T> : IDisposable where T : class, IDataSessionProvider
    {
        public abstract DbConnection DefaultConnection { get; }

        public abstract string ConnectionString { get; set; }
        public abstract T Provider { get; }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public abstract void Dispose();

        #endregion

        protected static string FindConnectionString(string connectionString)
        {
            string connectionStringName = connectionString;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (settings == null)
                throw new ConfigurationErrorsException(string.Format("The connection string entry '{0}' is not configured in the connection string section of the configuration file.", connectionStringName));

            return settings.ConnectionString;
        }

        protected static T CreateProvider(string typeName)
        {
            if (Compare.IsNullOrEmpty(typeName))
                throw new ConfigurationErrorsException("A data provider has not been configured or the entry is blank.");

            Type type = Type.GetType(typeName, false, false);
            return CreateProvider(type);
        }

        protected static T CreateProvider(Type providerType)
        {
            T provider = null;
            if (providerType != null)
                provider = Activator.CreateInstance(providerType) as T;

            if (provider == null)
                throw new ConfigurationErrorsException(string.Format("No valid data provider found for the type '{0}'.", providerType));

            return provider;
        }

        public abstract DbConnection OpenDynamicConnection();
    }
}