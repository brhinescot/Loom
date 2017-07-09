#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ForeignColumnAttribute : Attribute, ITableColumnDataProvider
    {
        #region Property Accessors

        public ColumnProperties ColumnProperties { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType { get; set; }
        public string Description { get; set; }
        public int MaxLength { get; set; }
        public string Name { get; private set; }
        public int Ordinal { get; set; }
        public Type DataRecordType { get; private set; }

        #endregion

        #region .ctor

        public ForeignColumnAttribute(string name, Type dataRecordType)
        {
            Name = name;
            DataRecordType = dataRecordType;
        }

        #endregion

        public TableData GetTable()
        {
            if (DataRecordType.IsEnum)
                return null;
            return TableData.FromInitializedDataRecord(DataRecordType);
        }
    }
}
