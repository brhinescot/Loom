#region Using Directives

using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ActiveProcedureParameterAttribute : DynamicPropertyAttribute
    {
        public ActiveProcedureParameterAttribute(string name, DbType dbType, int maxLength, ParameterType parameterType, bool isResult)
        {
            Name = name;
            DbType = dbType;
            MaxLength = maxLength;
            ParameterType = parameterType;
            IsResult = isResult;
        }

        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType { get; }

        public bool IsResult { get; }

        public int MaxLength { get; }

        public ParameterType ParameterType { get; }
    }
}