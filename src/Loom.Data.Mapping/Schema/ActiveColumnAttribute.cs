#region Using Directives

using System;
using System.Data;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    // SIZE: 32 bytes
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ActiveColumnAttribute : DynamicPropertyAttribute
    {
        public ActiveColumnAttribute(string name, DbType dbType, ColumnProperties columnProperties = ColumnProperties.None)
        {
            Name = name;
            DbType = dbType;
            ColumnProperties = columnProperties;
        }

        public ColumnProperties ColumnProperties { get; }

        public DbType DbType { get; }

        public string DefaultValue { get; set; }
        public string Description { get; set; }

        public int MaxLength { get; set; }
        public int Ordinal { get; set; }
    }
}