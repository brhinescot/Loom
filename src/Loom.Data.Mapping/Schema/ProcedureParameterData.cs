#region Using Directives

using System.Collections.ObjectModel;
using System.Data;

#endregion

namespace Loom.Data.Mapping.Schema
{
    internal sealed class ProcedureParameterData : ICallableParameter
    {
        public ProcedureParameterData(string name, DbType dbType, int maxLength, ParameterType parameterType, bool isResult)
        {
            Name = name;
            DbType = dbType;
            MaxLength = maxLength;
            ParameterType = parameterType;
            IsResult = isResult;
        }

        #region ICallableParameter Members

        public string Name { get; }

        public DbType DbType { get; }

        public int MaxLength { get; }

        public ParameterType ParameterType { get; }

        public bool IsResult { get; }

        #endregion
    }

    public class ICallableParameterCollection : Collection<ICallableParameter> { }
}