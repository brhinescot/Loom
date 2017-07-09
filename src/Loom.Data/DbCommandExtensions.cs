#region Using Directives

using System;
using System.Data;

#endregion

namespace Loom.Data
{
    public static class DbCommandExtensions
    {
        public static void AddParameterizedCommandText(this IDbCommand command, string commandText, params object[] parameterValues)
        {
            object[] names = new object[parameterValues.Length];

            for (int i = 0; i < parameterValues.Length; i++)
            {
                object parameterValue = parameterValues[i] ?? DBNull.Value;

                string parameterName = "@_p" + i;
                names[i] = parameterName;

                IDbDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = parameterName;
                parameter.Value = parameterValue;
                command.Parameters.Add(parameter);
            }
            command.CommandText = string.Format(commandText, names);
        }

        internal static int GetCacheKey(this IDbCommand command)
        {
            if (command == null || command.CommandText == null)
                return 0;

            int key = command.CommandText.GetHashCode();
            foreach (IDbDataParameter parameter in command.Parameters)
            {
                int nameHash = parameter.ParameterName != null ? parameter.ParameterName.GetHashCode() : 0;
                int typeHash = parameter.DbType.GetHashCode();
                int sizeHash = parameter.Size.GetHashCode();
                int valueHash = parameter.Value != null ? parameter.Value.GetHashCode() : 0;

                key = HashCode.Combine(key, nameHash, typeHash, sizeHash, valueHash);
            }

            return key;
        }
    }
}