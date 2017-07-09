#region Using Directives

using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    ///<summary>
    ///</summary>
    [Serializable]
    [DebuggerDisplay("{FullNameBracketed,nq}, DbType={DbType}, IsIdentity={IsIdentity}")]
    public sealed class TableColumnDefinition
    {
        private const string FullNamePrefix = ".[";
        private const string FullNameSuffix = "]";

        private static readonly int isPrimaryKey = BitVector32.CreateMask();
        private static readonly int isForeignKey = BitVector32.CreateMask(isPrimaryKey);
        private static readonly int isIdentity = BitVector32.CreateMask(isForeignKey);
        private static readonly int isNullable = BitVector32.CreateMask(isIdentity);
        private static readonly int isUnique = BitVector32.CreateMask(isNullable);
        private static readonly int isComputed = BitVector32.CreateMask(isUnique);
        private static readonly int isLocalizable = BitVector32.CreateMask(isComputed);
        private readonly ActiveMapCodeGenConfigurationSection configuration;
        private string alias;
        private string columnName;
        private DbType dbType;
        private string description;

        [NonSerialized] private BitVector32 flags = new BitVector32(0);

        private string fullNameBracketed;

        private TableDefinition parentTable;

        public TableColumnDefinition(ActiveMapCodeGenConfigurationSection configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     Gets the name of this column.
        /// </summary>
        public string Name
        {
            get => columnName;
            internal set
            {
                columnName = value;
                SetAuditFields(this);
            }
        }

        ///<summary>
        ///</summary>
        internal string Alias
        {
            get => alias;
            set => alias = value;
        }

        /// <summary>
        ///     Gets the fully qualified name of this column, including the schema and table names,
        ///     in bracketed format.
        /// </summary>
        public string FullNameBracketed
        {
            get
            {
                if (fullNameBracketed == null)
                    fullNameBracketed = parentTable.FullNameBracketed + FullNamePrefix + columnName + FullNameSuffix;
                return fullNameBracketed;
            }
        }

        /// <summary>
        ///     Gets a value indicating if the database column can contain a null value.
        /// </summary>
        public bool IsNullable
        {
            get => flags[isNullable];
            internal set => flags[isNullable] = value;
        }

        /// <summary>
        ///     Gets a value indicating the <see cref="DbType" /> of this column.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "Using BCL capitalization for type.")]
        public DbType DbType
        {
            get => dbType;
            internal set => dbType = value;
        }

        /// <summary>
        ///     Gets a value indicating the maximum length of this column in the database.
        /// </summary>
        public int MaxLength { get; internal set; }

        /// <summary>
        ///     Gets a value indicating if this column is the primary key.
        /// </summary>
        public bool IsPrimaryKey
        {
            get => flags[isPrimaryKey];
            internal set
            {
                flags[isPrimaryKey] = value;
                flags[isUnique] = flags[isPrimaryKey];
            }
        }

        /// <summary>
        ///     Gets a value indicating if this column is an autonumber/identity column.
        /// </summary>
        public bool IsIdentity
        {
            get => flags[isIdentity];
            internal set => flags[isIdentity] = value;
        }

        /// <summary>
        ///     Gets a value indicating if this column is a foreign key.
        /// </summary>
        public bool IsForeignKey
        {
            get => flags[isForeignKey];
            internal set => flags[isForeignKey] = value;
        }

        /// <summary>
        ///     Gets a value indicating if this column is a foreign key.
        /// </summary>
        public bool IsLocalizable
        {
            get => flags[isLocalizable];
            internal set => flags[isLocalizable] = value;
        }

        /// <summary>
        ///     Gets a value indicating if this column has a unique constraint applied to it in the data source.
        /// </summary>
        public bool IsUnique
        {
            get => flags[isUnique];
            internal set => flags[isUnique] = value;
        }

        /// <summary>
        ///     Gets a value indicating if this column is a computed column.
        /// </summary>
        public bool IsComputed
        {
            get => flags[isComputed];
            internal set => flags[isComputed] = value;
        }

        /// <summary>
        ///     Gets the ordinal value representing this columns position in the data source.
        /// </summary>
        public int Ordinal { get; internal set; }

        ///<summary>
        ///</summary>
        public string Description
        {
            get => description;
            internal set => description = value.Replace(Environment.NewLine, Environment.NewLine + "/// ");
        }

        internal TableDefinition ParentTable
        {
            get => parentTable;
            set => parentTable = value;
        }

        internal AuditField AuditField { get; set; }

        /// <summary>
        ///     Gets the column referenced by this column if <see cref="IsForeignKey" /> is true.
        /// </summary>
        /// <returns>
        ///     A <see cref="TableColumnDefinition" /> if this column is a foreign key,
        ///     or <see langword="null" /> if it is not.
        /// </returns>
        public TableColumnDefinition ForeignKeyColumn { get; internal set; }

        /// <summary>
        ///     Gets the column referenced by this column if <see cref="IsForeignKey" /> is true.
        /// </summary>
        /// <returns>
        ///     A <see cref="TableColumnDefinition" /> if this column is a foreign key,
        ///     or <see langword="null" /> if it is not.
        /// </returns>
        public TableColumnDefinition LocalizationColumn { get; internal set; }

        public string DefaultValue { get; internal set; }

        /// <summary>
        /// </summary>
        /// <param name="columnAlias"></param>
        /// <returns></returns>
        public TableColumnDefinition As(string columnAlias)
        {
            alias = columnAlias;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string ToCamelCase()
        {
            if (columnName == null)
                throw new InvalidOperationException("The column name has not been set. Unable to generate a camel cased representation of the column.");

            return CodeFormat.ToCamelCase(columnName);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string ToPascalCase()
        {
            return ToPascalCase(CodeFormatOptions.RemoveFKPrefix);
        }

        ///<summary>
        ///</summary>
        ///<param name="formatOptions"></param>
        ///<returns></returns>
        public string ToPascalCase(CodeFormatOptions formatOptions)
        {
            if (columnName == null)
                throw new InvalidOperationException("The column name has not been set. Unable to generate a pascal cased representation of the column.");

            return CodeFormat.ToPascalCase(columnName, formatOptions);
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string ToProperCase()
        {
            if (columnName == null)
                throw new InvalidOperationException("The column name has not been set. Unable to generate a proper cased representation of the column.");

            return CodeFormat.ToProperCase(columnName);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents the current <see cref="System.Object" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents the current <see cref="System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return FullNameBracketed;
        }

        private void SetAuditFields(TableColumnDefinition column)
        {
            AuditMappingElement config = configuration.AuditMapping;

            bool created = string.Compare(column.Name, config.CreatedOnColumn, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(column.Name, config.CreatedbyColumn, StringComparison.OrdinalIgnoreCase) == 0;
            bool modified = string.Compare(column.Name, config.ModifiedByColumn, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(column.Name, config.ModifiedOnColumn, StringComparison.OrdinalIgnoreCase) == 0;
            bool user = string.Compare(column.Name, config.CreatedbyColumn, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(column.Name, config.ModifiedByColumn, StringComparison.OrdinalIgnoreCase) == 0;
            bool date = string.Compare(column.Name, config.CreatedOnColumn, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(column.Name, config.ModifiedOnColumn, StringComparison.OrdinalIgnoreCase) == 0;
            bool deleted = string.Compare(column.Name, config.DeletedColumn, StringComparison.OrdinalIgnoreCase) == 0;

            column.AuditField = new AuditField(user, date, modified, created, deleted);
        }

        internal static string GetDataTypeLong(DbType dbType, bool columnIsNullable, bool useNullables)
        {
            bool nullable = columnIsNullable && useNullables;
            switch (dbType)
            {
                case DbType.String:
                case DbType.Binary:
                case DbType.AnsiStringFixedLength:
                    return dbType.ToString();

                case DbType.Int64:
                case DbType.Boolean:
                case DbType.DateTime:
                case DbType.Decimal:
                case DbType.Int32:
                case DbType.Currency:
                case DbType.Int16:
                case DbType.Byte:
                case DbType.Guid:
                    return nullable ? "Nullable" + dbType : dbType.ToString();

                default:
                    return "String";
            }
        }

        internal static string GetDataTypeShort(DbType dbType, bool columnIsNullable, bool useNullables)
        {
            bool nullable = columnIsNullable && useNullables;
            switch (dbType)
            {
                case DbType.String:
                    return "string";
                case DbType.Binary:
                    return "byte[]";
                case DbType.AnsiStringFixedLength:
                    return "char";
                case DbType.Int64:
                    return nullable ? "long?" : "long";
                case DbType.Boolean:
                    return nullable ? "bool?" : "bool";
                case DbType.DateTime:
                case DbType.Date:
                    return nullable ? "DateTime?" : "DateTime";
                case DbType.Int32:
                    return nullable ? "int?" : "int";
                case DbType.Currency:
                case DbType.Decimal:
                    return nullable ? "decimal?" : "decimal";
                case DbType.Int16:
                    return nullable ? "short?" : "short";
                case DbType.Byte:
                    return nullable ? "byte?" : "byte";
                case DbType.Guid:
                    return nullable ? "Guid?" : "Guid";
                default:
                    return "string";
            }
        }

        internal string GetDataTypeShort()
        {
            return GetDataTypeShort(dbType, flags[isNullable], configuration.CodeGen.UseNullableTypes);
        }

        internal string GetDataTypeLong()
        {
            return GetDataTypeLong(dbType, flags[isNullable], configuration.CodeGen.UseNullableTypes);
        }
    }
}