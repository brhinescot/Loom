#region Using Directives

using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ForeignColumnAttribute : Attribute, ITableColumnDataProvider
    {
        public ForeignColumnAttribute(string name, Type dataRecordType)
        {
            Name = name;
            DataRecordType = dataRecordType;
        }

        public ColumnProperties ColumnProperties { get; set; }

        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType { get; set; }

        public string Description { get; set; }
        public int MaxLength { get; set; }
        public int Ordinal { get; set; }

        #region ITableColumnDataProvider Members

        public string Name { get; }
        public Type DataRecordType { get; }

        public ITable GetTable()
        {
            return DataRecordType.IsEnum ? null : TableData.FromInitializedDataRecord(DataRecordType);
        }

        #endregion
    }
}