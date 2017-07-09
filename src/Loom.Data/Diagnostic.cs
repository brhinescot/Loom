#region Using Directives

using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

#endregion

namespace Loom.Data
{
    public static class Diagnostic
    {
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        [Conditional("DEBUG")]
        public static void WriteDbParameters(DbParameterCollection parameters)
        {
            Argument.Assert.IsNotNull(parameters, nameof(parameters));

            if (parameters.Count == 0)
                return;

            Debug.WriteLine("Parameters:");
            foreach (DbParameter parameter in parameters)
            {
                object parameterValue = parameter.Value;
                if (parameterValue == DBNull.Value)
                    parameterValue = "NULL";
                else if (parameter.DbType == DbType.String)
                    parameterValue = "\"" + parameterValue + "\"";

                Debug.WriteLine("    {0} = {1} [{2}]", parameter.ParameterName, parameterValue, parameter.DbType);
            }
        }

        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        [Conditional("DEBUG")]
        public static void WriteDbParameters(DbParameterCollection parameters, TextWriter writer)
        {
            if (writer == null)
                return;

            if (parameters == null || parameters.Count == 0)
                return;

            writer.WriteLine("Parameters:");
            foreach (DbParameter parameter in parameters)
            {
                object parameterValue = parameter.Value;
                if (parameterValue == DBNull.Value)
                    parameterValue = "NULL";
                else if (parameter.DbType == DbType.String)
                    parameterValue = "\"" + parameterValue + "\"";

                writer.WriteLine("    {0} = {1} [{2}]", parameter.ParameterName, parameterValue, parameter.DbType);
            }
        }
    }
}