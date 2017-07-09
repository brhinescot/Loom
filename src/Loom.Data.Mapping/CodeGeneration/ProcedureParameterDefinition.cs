#region Using Directives

using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable]
    [DebuggerDisplay("{Name,nq}, DbType = {DbType}")]
    public sealed class ProcedureParameterDefinition
    {
        private readonly ActiveMapCodeGenConfigurationSection configuration;
        private DbType dbType;
        private bool isResult;
        private int maxLength;

        private string name;
        private ParameterType parameterType;
        private int position;

        public ProcedureParameterDefinition(ActiveMapCodeGenConfigurationSection configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     Gets or sets the name of this column.
        /// </summary>
        public string Name
        {
            get => name;
            internal set => name = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating the <see cref="DbType" /> of this column.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType
        {
            get => dbType;
            internal set => dbType = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating the maximum length of this column in the database.
        /// </summary>
        public int MaxLength
        {
            get => maxLength;
            internal set => maxLength = value;
        }

        public int Position
        {
            get => position;
            internal set => position = value;
        }

        public ParameterType ParameterType
        {
            get => parameterType;
            internal set => parameterType = value;
        }

        public bool IsResult
        {
            get => isResult;
            internal set => isResult = value;
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