#region Using Directives

using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Providers;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    public class CodeGenSession
    {
        private static readonly object providerLockObject = new object();
        private static readonly object tableLockObject = new object();

        private IActiveDataProvider provider;
        private DatabaseSchema schema;

        public CodeGenSession()
        {
            Configuration = (ActiveMapCodeGenConfigurationSection) ConfigurationManager.GetSection("activeMapCodeGenConfiguration");
        }

        public CodeGenSession(string configurationFilePath)
        {
            if (configurationFilePath == null)
            {
                Configuration = new ActiveMapCodeGenConfigurationSection();
                return;
            }

            Argument.Assert.IsNotNullOrEmpty(configurationFilePath, "configurationFilePath");

            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = GetConfigFilePath(configurationFilePath);
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            Configuration = (ActiveMapCodeGenConfigurationSection) config.GetSection("activeMapCodeGenConfiguration");
            if (Configuration == null)
                throw new ConfigurationErrorsException("Could not load the configuration section 'activeMapCodeGenConfiguration'.");
        }

        public static CodeGenSession Default => new CodeGenSession(null);

        public ActiveMapCodeGenConfigurationSection Configuration { get; }

        private IActiveDataProvider Provider
        {
            get
            {
                if (provider == null)
                    lock (providerLockObject)
                    {
                        if (provider == null)
                        {
                            string providerType = Configuration.Provider.Type;
                            if (Compare.IsNullOrEmpty(providerType))
                                throw new ConfigurationErrorsException("A data provider has not been configured or the entry is blank.");

                            Type type = Type.GetType(providerType, false, false);
                            if (type != null)
                                provider = Activator.CreateInstance(type) as IActiveDataProvider;

                            if (provider == null)
                                throw new ConfigurationErrorsException(string.Format("No valid data provider found for the type '{0}'.", providerType));
                        }
                    }

                return provider;
            }
        }

        public DatabaseSchema Schema
        {
            get
            {
                if (schema == null)
                    lock (tableLockObject)
                    {
                        if (schema == null)
                            schema = Provider.FetchDatabaseSchema(Configuration.Provider.ConnectionString, Configuration);
                    }

                return schema;
            }
        }

        public TablesElement GetTableConfiguration(string owner, string name)
        {
            foreach (TablesElement table in Configuration.Tables)
                if (table.Owner == owner && table.Name == name)
                    return table;

            return null;
        }

        public ProceduresElement GetProcedureConfiguration(string owner, string name)
        {
            foreach (ProceduresElement procedure in Configuration.Procedures)
                if (procedure.Owner == owner && procedure.Name == name)
                    return procedure;

            return null;
        }

        private static string GetConfigFilePath(string path)
        {
            if (Path.IsPathRooted(path))
                return path;

            string currentDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(currentDirectory, path);
        }

        public IDataReader ExecuteReader(CodeGenQuery query)
        {
            using (DbCommand command = Provider.FetchCommand(query))
            {
                return ExecuteReader(command);
            }
        }

        internal IDataReader ExecuteReader(DbCommand command)
        {
            Argument.Assert.IsNotNull(command, nameof(command));

            return ExecuteReader(command, CommandBehavior.CloseConnection);
        }

        internal IDataReader ExecuteReader(DbCommand command, CommandBehavior behavior)
        {
            Argument.Assert.IsNotNull(command, nameof(command));

            try
            {
                EnlistConnection(command);
                return command.ExecuteReader(behavior);
            }
            finally
            {
                command.Dispose();
            }
        }

        internal DbConnection EnlistConnection(DbCommand command)
        {
            command.Connection = OpenConnection();

            return command.Connection;
        }

        private DbConnection OpenConnection()
        {
            DbConnection dynamicConnection = Provider.FetchConnection(Configuration.Provider.ConnectionString);
            dynamicConnection.Open();
            return dynamicConnection;
        }
    }
}