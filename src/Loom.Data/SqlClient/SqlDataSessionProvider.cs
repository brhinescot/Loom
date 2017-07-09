#region Using Directives

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Loom.Annotations;

#endregion

namespace Loom.Data.SqlClient
{
    public class SqlDataSessionProvider : IDataSessionProvider
    {
        #region IDataSessionProvider Members

        public DbCommand FetchCommand([NotNull] string commandText, params object[] parameters)
        {
            Argument.Assert.IsNotNullOrEmpty(commandText, nameof(commandText));

            SqlCommand command = new SqlCommand();

            if (parameters == null)
            {
                command.CommandText = commandText;
                return command;
            }

            object[] names = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                object parameter = parameters[i] ?? DBNull.Value;

                string name = "@_p" + i;
                names[i] = name;
                command.Parameters.AddWithValue(name, parameter);
            }

            command.CommandText = string.Format(commandText, names);
            return command;
        }

        public DbConnection FetchConnection([NotNull] string connectionString)
        {
            Argument.Assert.IsNotNull(connectionString, nameof(connectionString));

            return new SqlConnection(connectionString);
        }

        public IDataAdapter FetchDataAdapter([NotNull] DbCommand command)
        {
            Argument.Assert.IsNotNull(command, nameof(command));

            return new SqlDataAdapter((SqlCommand) command);
        }

        #endregion
    }
}