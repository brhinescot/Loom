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
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    // SIZE: 32 bytes
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class ActiveColumnAttribute : DynamicPropertyAttribute
    {
        #region Property Accessors

        public int MaxLength { get; set; }
        public int Ordinal { get; set; }
        public string Description { get; set; }
        public string DefaultValue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType { get; private set; }
        public ColumnProperties ColumnProperties { get; private set; }

        #endregion

        public ActiveColumnAttribute(string name, DbType dbType) : this(name, dbType, ColumnProperties.None) {}

        public ActiveColumnAttribute(string name, DbType dbType, ColumnProperties columnProperties)
        {
            Name = name;
            DbType = dbType;
            ColumnProperties = columnProperties;
        }
    }
}
