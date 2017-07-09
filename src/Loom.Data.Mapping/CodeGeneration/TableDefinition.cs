#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    ///<summary>
    ///</summary>
    [Serializable]
    [DebuggerDisplay("{FullNameBracketed,nq}")]
    public sealed class TableDefinition : IEquatable<TableDefinition>, IComparable<TableDefinition>, ISchema
    {
        private const string LeftBracket = "[";
        private const string RightBracket = "]";
        private const string Separator = ".";

        [SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")] public static readonly string DefaultOwner = "dbo";

        private static readonly int isReadOnly;
        private static readonly int isLookup;
        private static readonly int isEnum;
        private readonly ActiveMapCodeGenConfigurationSection configuration;
        private string alias;
        private Dictionary<string, object> charAsBooleans;

        private TableColumnDefinitionCollection columns;
        private TableColumnDefinition deletedColumn;

        [NonSerialized] private BitVector32 flags = new BitVector32(0);

        private string fullName;
        private string fullNameBracketed;
        private TableColumnDefinition primaryKey;

        static TableDefinition()
        {
            isReadOnly = BitVector32.CreateMask();
            isLookup = BitVector32.CreateMask(isReadOnly);
            isEnum = BitVector32.CreateMask(isLookup);
        }

        // TODO: Standardize the order of data source, schema, table, column parameters to methods.
        /// <summary>
        ///     Initializes a new instance of the <see cref="TableDefinition" /> class.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="name">The name of the table.</param>
        /// <param name="configuration"></param>
        public TableDefinition(string datasource, string name, ActiveMapCodeGenConfigurationSection configuration) : this(datasource, name, DefaultOwner, configuration) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TableDefinition" /> class.
        /// </summary>
        /// <param name="owner">The schema that owns the table.</param>
        /// <param name="datasource"></param>
        /// <param name="name">The name of the table.</param>
        /// <param name="configuration"></param>
        public TableDefinition(string datasource, string name, string owner, ActiveMapCodeGenConfigurationSection configuration)
        {
            Argument.Assert.IsNotNull(name, nameof(name));
            Argument.Assert.IsNotNull(owner, nameof(owner));

            Datasource = datasource;
            Name = name;
            Owner = owner;
            this.configuration = configuration;
            SetTableProperties();
        }

        internal Dictionary<string, object> CharAsBooleans => charAsBooleans ?? (charAsBooleans = new Dictionary<string, object>());

        /// <summary>
        ///     Gets the full name of the table including the schema.
        /// </summary>
        /// <remarks>
        ///     The format is SchemaName.TableName. Spaces in the schema or table name will cause an error
        ///     if used in a query. Use <see cref="FullNameBracketed" /> or <see cref="CalculateSafeFullName" /> for queries
        ///     to ensure spaces are correctly handled.
        /// </remarks>
        public string FullName => fullName ?? (fullName = Owner + Separator + Name);

        /// <summary>
        ///     Gets the full bracketed name of the table including the schema.
        /// </summary>
        /// <remarks>The format is [SchemaName].[TableName].</remarks>
        /// <value></value>
        public string FullNameBracketed => fullNameBracketed ?? (fullNameBracketed = LeftBracket + Owner + RightBracket + Separator + LeftBracket + Name + RightBracket);

        internal string Alias
        {
            get => alias;
            set => alias = value;
        }

        /// <summary>
        ///     Gets a <see cref="TableColumnDefinition" /> object representing the table's primary key.
        /// </summary>
        /// <value>
        ///     A <see cref="TableColumnDefinition" /> if the table has a primary key, or null if it
        ///     does not.
        /// </value>
        public TableColumnDefinition PrimaryKey
        {
            get => primaryKey;
            internal set => primaryKey = value;
        }

        /// <summary>
        ///     Gets a <see cref="TableColumnDefinition" /> object representing the table's deleted indicator column.
        /// </summary>
        /// <value>
        ///     A <see cref="TableColumnDefinition" /> if the table has a deleted indicator column, or null if it
        ///     does not.
        /// </value>
        public TableColumnDefinition DeletedColumn
        {
            get => deletedColumn;
            internal set => deletedColumn = value;
        }

        /// <summary>
        ///     Gets a value indicating if this table has a primary key;
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this table has a primary key; otherwise, <see langword="false" />.
        /// </value>
        public bool HasPrimaryKey => primaryKey != null;

        /// <summary>
        ///     Gets a value indicating if this table has a deleted indicator column;
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this table has a primary key; otherwise, <see langword="false" />.
        /// </value>
        public bool HasDeletedColumn => deletedColumn != null;

        /// <summary>
        ///     Gets a collection of the columns in this table.
        /// </summary>
        /// <value>The columns in this table.</value>
        public TableColumnDefinitionCollection Columns => columns ?? (columns = new TableColumnDefinitionCollection(this, configuration));

        /// <summary>
        ///     Gets a value indicating whether this table is a lookup table.
        /// </summary>
        /// <remarks>
        ///     Lookup tables are represented by an <see langword="enum" /> and corresponding
        ///     <see langword="enum" /> properties in generated classes.
        /// </remarks>
        /// <value>
        ///     <see langword="true" /> if this table is a lookup table; otherwise,
        ///     <see langword="false" />.
        /// </value>
        public bool IsLookup => flags[isLookup];

        /// <summary>
        ///     Gets a value indicating whether this table is a lookup table.
        /// </summary>
        /// <remarks>
        ///     Lookup tables are represented by an <see langword="enum" /> and corresponding
        ///     <see langword="enum" /> properties in generated classes.
        /// </remarks>
        /// <value>
        ///     <see langword="true" /> if this table is a lookup table; otherwise,
        ///     <see langword="false" />.
        /// </value>
        public bool IsEnum => flags[isEnum];

        internal int KeyOrdinal { get; private set; }

        internal int ValueOrdinal { get; private set; }

        #region IComparable<TableDefinition> Members

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relative order of the objects
        ///     being compared. The return value has the following meanings: Value Meaning
        ///     Less than zero This object is less than the other parameter.Zero This object
        ///     is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(TableDefinition other)
        {
            return other == null ? 0 : string.Compare(FullName, other.FullName, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region IEquatable<TableDefinition> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(TableDefinition other)
        {
            return Equals(Name, other.Name) && Equals(Owner, other.Owner);
        }

        #endregion

        #region ISchema Members

        /// <summary>
        ///     Gets the data source the table belongs to.
        /// </summary>
        public string Datasource { get; }

        /// <summary>
        ///     Gets the name of the table.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the schema that owns the table.
        /// </summary>
        public string Owner { get; }

        /// <summary>
        ///     Gets a value indicating whether this table is read only.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this table is read only; otherwise, <see langword="false" />.
        /// </value>
        public bool IsReadOnly
        {
            get => flags[isReadOnly];
            internal set => flags[isReadOnly] = value;
        }

        #endregion

        /// <summary>
        ///     Gets the full name of the table including the schema. If the name contains spaces then the schema
        ///     and table are wrapped in brackets.
        /// </summary>
        /// <returns></returns>
        public string CalculateSafeFullName()
        {
            const string blankSpace = " ";
            return fullName.Contains(blankSpace) ? FullNameBracketed : FullName;
        }

        private void SetTableProperties()
        {
            CheckForCharAsBooleans();
            CheckForEnumTable();
            if (!flags[isEnum])
                CheckForLookupTable();
        }

        private void CheckForCharAsBooleans()
        {
            TablesCollection classTables = configuration.Tables;

            foreach (TablesElement tables in classTables)
            {
                if (Name != tables.Name || Owner != tables.Owner || Compare.IsNullOrEmpty(tables.CharAsBooleanColumns))
                    continue;

                foreach (string item in tables.CharAsBooleanColumns.Split(';'))
                    CharAsBooleans.Add(item, null);
                break;
            }
        }

        private void CheckForLookupTable()
        {
            LookupTablesCollection lookupTables = configuration.LookupTables;

            foreach (LookupTablesElement lookupTable in lookupTables)
            {
                if (Name != lookupTable.Name || Owner != lookupTable.Owner)
                    continue;

                if (lookupTable.Exclude)
                    return;

                flags[isLookup] = true;
                KeyOrdinal = lookupTable.KeyOrdinal;
                ValueOrdinal = lookupTable.ValueOrdinal;
                return;
            }

            if (!Compare.IsNullOrEmpty(lookupTables.SchemaIncludes) && Owner == lookupTables.SchemaIncludes)
                flags[isLookup] = true;
            else if (!Compare.IsNullOrEmpty(lookupTables.PrefixIncludes) && Name.StartsWith(lookupTables.PrefixIncludes))
                flags[isLookup] = true;
            else if (!Compare.IsNullOrEmpty(lookupTables.SuffixIncludes) && Name.EndsWith(lookupTables.SuffixIncludes))
                flags[isLookup] = true;

            if (!flags[isLookup])
                return;

            KeyOrdinal = lookupTables.KeyOrdinal;
            ValueOrdinal = lookupTables.ValueOrdinal;
        }

        private void CheckForEnumTable()
        {
            EnumTablesCollection enumTables = configuration.EnumTables;

            foreach (EnumTablesElement element in enumTables)
            {
                if (Name != element.Name || Owner != element.Owner)
                    continue;

                if (element.Exclude)
                    return;

                flags[isEnum] = true;
                KeyOrdinal = element.KeyOrdinal;
                ValueOrdinal = element.ValueOrdinal;
                return;
            }

            if (!Compare.IsNullOrEmpty(enumTables.SchemaIncludes) && Owner == enumTables.SchemaIncludes)
                flags[isEnum] = true;
            else if (!Compare.IsNullOrEmpty(enumTables.PrefixIncludes) && Name.StartsWith(enumTables.PrefixIncludes))
                flags[isEnum] = true;
            else if (!Compare.IsNullOrEmpty(enumTables.SuffixIncludes) && Name.EndsWith(enumTables.SuffixIncludes))
                flags[isEnum] = true;

            if (!flags[isEnum])
                return;

            KeyOrdinal = enumTables.KeyOrdinal;
            ValueOrdinal = enumTables.ValueOrdinal;
        }

        /// <summary>
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public TableDefinition As(string tableAlias)
        {
            alias = tableAlias;
            return this;
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string ToCamelCase()
        {
            return CodeFormat.ToCamelCase(Name);
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string ToPascalCase()
        {
            return CodeFormat.ToPascalCase(Name, CodeFormatOptions.None);
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string ToProperCase()
        {
            return CodeFormat.ToProperCase(Name);
        }

        /// <summary>
        ///     Returns a <see cref="System.String"></see> that represents the current
        ///     <see cref="TableDefinition">
        ///     </see>
        ///     .
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String"></see> that represents the current <see cref="TableDefinition"></see>.
        /// </returns>
        public override string ToString()
        {
            return FullNameBracketed;
        }

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        ///     true if obj and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            TableDefinition info = obj as TableDefinition;
            return info != null && Equals(info);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0) + 29 * (Owner != null ? Owner.GetHashCode() : 0);
        }
    }
}