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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable, DebuggerDisplay("{Name,nq}, DbType = {DbType}")]
    public sealed class ProcedureParameterDefinition
    {
        #region Member Fields

        private string name;
        private int position;
        private ParameterType parameterType;
        private bool isResult;
        private int maxLength;
        private DbType dbType;
        private readonly ActiveMapCodeGenConfigurationSection configuration;

        #endregion

        #region Property Accessors

        /// <summary>
        /// Gets or sets the name of this column.
        /// </summary>
        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the <see cref="DbType"/> of this column.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType
        {
            get { return dbType; }
            internal set { dbType = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the maximum length of this column in the database.
        /// </summary>
        public int MaxLength
        {
            get { return maxLength; }
            internal set { maxLength = value; }
        }

        public int Position
        {
            get { return position; }
            internal set { position = value; }
        }

        public ParameterType ParameterType
        {
            get { return parameterType; }
            internal set { parameterType = value; }
        }

        public bool IsResult
        {
            get { return isResult; }
            internal set { isResult = value; }
        }

        #endregion

        public ProcedureParameterDefinition(ActiveMapCodeGenConfigurationSection configuration)
        {
            this.configuration = configuration;
        }

        public string ToCamelCase()
        {
            return CodeFormat.ToCamelCase(name);
        }

        public string ToPascalCase()
        {
            return CodeFormat.ToPascalCase(name, CodeFormatOptions.RemoveFKPrefix);
        }

        public string ToPascalCase(CodeFormatOptions formatOptions)
        {
            return CodeFormat.ToPascalCase(name, formatOptions);
        }

        public string ToProperCase()
        {
            return CodeFormat.ToProperCase(name);
        }

        public string GetDataTypeShort()
        {
            return TableColumnDefinition.GetDataTypeShort(dbType, false, configuration.CodeGen.UseNullableTypes);
        }

        public string GetDataTypeLong()
        {
            return TableColumnDefinition.GetDataTypeLong(dbType, false, configuration.CodeGen.UseNullableTypes);
        }
    }
}
