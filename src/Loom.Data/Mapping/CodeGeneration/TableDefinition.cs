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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Loom.Data.Mapping.Configuration;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    ///<summary>
    ///</summary>
    [Serializable, DebuggerDisplay("{FullNameBracketed,nq}")]
    public sealed class TableDefinition : IEquatable<TableDefinition>, IComparable<TableDefinition>, ISchema
    {
        #region Type Fields

        private const string LeftBracket = "[";
        private const string RightBracket = "]";
        private const string Seperator = ".";

        [SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")] 
        public static readonly string DefaultOwner = "dbo";
        private static readonly int isReadOnly;
        private static readonly int isLookup;
        private static readonly int isEnum;

        #endregion

        #region Instance Fields

        private TableColumnDefinitionCollection columns;
        private TableColumnDefinition primaryKey;
        private TableColumnDefinition deletedColumn;
        private readonly string datasource;
        private readonly string name;
        private readonly string owner;
        private readonly ActiveMapCodeGenConfigurationSection configuration;
        private string fullNameBracketed;
        private string fullName;
        private string alias;
        private int keyOrdinal;
        private int valueOrdinal;
        private Dictionary<string, object> charAsBooleans;

        [NonSerialized]
        private BitVector32 flags = new BitVector32(0);

        #endregion

        #region Property Accessors

        internal Dictionary<string, object> CharAsBooleans
        {
            get
            {
                if (charAsBooleans == null)
                    charAsBooleans = new Dictionary<string, object>();
                return charAsBooleans;
            }
        }

        /// <summary>
        /// Gets the data source the table belongs to.
        /// </summary>
        public string Datasource
        {
            get { return datasource; }
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the schema that owns the table.
        /// </summary>
        public string Owner
        {
            get { return owner; }
        }

        /// <summary>
        /// Gets the full name of the table including the schema.
        /// </summary>
        /// <remarks>The format is SchemaName.TableName. Spaces in the schema or table name will cause an error
        /// if used in a query. Use <see cref="FullNameBracketed"/> or <see cref="CalculateSafeFullName"/> for queries 
        /// to ensure spaces are correctly handled.</remarks>
        public string FullName
        {
            get
            {
                if (fullName == null)
                    fullName = owner + Seperator + name;
                return fullName;
            }
        }

        /// <summary>
        /// Gets the full bracketed name of the table including the schema.
        /// </summary>
        /// <remarks>The format is [SchemaName].[TableName].</remarks>
        /// <value></value>
        public string FullNameBracketed
        {
            get
            {
                if (fullNameBracketed == null)
                    fullNameBracketed = LeftBracket + owner + RightBracket + Seperator + LeftBracket + name + RightBracket;
                return fullNameBracketed;
            }
        }

        internal string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        /// <summary>
        /// Gets a <see cref="TableColumnDefinition"/> object representing the table's primary key.
        /// </summary>
        /// <value>A <see cref="TableColumnDefinition"/> if the table has a primary key, or null if it 
        /// does not.</value>
        public TableColumnDefinition PrimaryKey
        {
            get { return primaryKey; }
            internal set { primaryKey = value; }
        }

        /// <summary>
        /// Gets a <see cref="TableColumnDefinition"/> object representing the table's deleted indicator column.
        /// </summary>
        /// <value>A <see cref="TableColumnDefinition"/> if the table has a deleted indicator column, or null if it 
        /// does not.</value>
        public TableColumnDefinition DeletedColumn
        {
            get { return deletedColumn; }
            internal set { deletedColumn = value; }
        }

        /// <summary>
        /// Gets a value indicating if this table has a primary key;
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if this table has a primary key; otherwise, <see langword="false"/>.
        /// </value>
        public bool HasPrimaryKey
        {
            get { return primaryKey != null; }
        }

        /// <summary>
        /// Gets a value indicating if this table has a deleted indicator column;
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if this table has a primary key; otherwise, <see langword="false"/>.
        /// </value>
        public bool HasDeletedColumn
        {
            get { return deletedColumn != null; }
        }

        /// <summary>
        /// Gets a collection of the columns in this table.
        /// </summary>
        /// <value>The columns in this table.</value>
        public TableColumnDefinitionCollection Columns
        {
            get
            {
                if (columns == null)
                    columns = new TableColumnDefinitionCollection(this, configuration);
                return columns;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this table is read only.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if this table is read only; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsReadOnly
        {
            get { return flags[isReadOnly]; }
            internal set { flags[isReadOnly] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this table is a lookup table.
        /// </summary>
        /// <remarks>Lookup tables are represented by an <see langword="enum"/> and corresponding
        /// <see langword="enum"/> properties in generated classes.</remarks>
        /// <value>
        ///     <see langword="true"/> if this table is a lookup table; otherwise, 
        /// <see langword="false"/>.</value>
        public bool IsLookup
        {
            get { return flags[isLookup]; }
        }

        /// <summary>
        /// Gets a value indicating whether this table is a lookup table.
        /// </summary>
        /// <remarks>Lookup tables are represented by an <see langword="enum"/> and corresponding
        /// <see langword="enum"/> properties in generated classes.</remarks>
        /// <value>
        ///     <see langword="true"/> if this table is a lookup table; otherwise, 
        /// <see langword="false"/>.</value>
        public bool IsEnum
        {
            get { return flags[isEnum]; }
        }

        internal int KeyOrdinal
        {
            get { return keyOrdinal; }
        }

        internal int ValueOrdinal
        {
            get { return valueOrdinal; }
        }

        #endregion

        #region .ctors

        static TableDefinition()
        {
            isReadOnly = BitVector32.CreateMask();
            isLookup = BitVector32.CreateMask(isReadOnly);
            isEnum = BitVector32.CreateMask(isLookup);
        }

        // TODO: Standardize the order of data source, schema, table, column parameters to methods.
        /// <summary>
        /// Initializes a new instance of the <see cref="TableDefinition"/> class.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="name">The name of the table.</param>
        /// <param name="configuration"></param>
        public TableDefinition(string datasource, string name, ActiveMapCodeGenConfigurationSection configuration) : this(datasource, name, DefaultOwner, configuration) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDefinition"/> class.
        /// </summary>
        /// <param name="owner">The schema that owns the table.</param>
        /// <param name="datasource"></param>
        /// <param name="name">The name of the table.</param>
        /// <param name="configuration"></param>
        public TableDefinition(string datasource, string name, string owner, ActiveMapCodeGenConfigurationSection configuration)
        {
            Argument.Assert.IsNotNull(name, Argument.Names.name);
            Argument.Assert.IsNotNull(owner, Argument.Names.schema);

            this.datasource = datasource;
            this.name = name;
            this.owner = owner;
            this.configuration = configuration;
            SetTableProperties();
        }

        #endregion

        /// <summary>
        /// Gets the full name of the table including the schema. If the name contains spaces then the schema
        /// and table are wrapped in brackets.
        /// </summary>
        /// <returns></returns>
        public string CalculateSafeFullName()
        {
            const string BlankSpace = " ";
            if (fullName.Contains(BlankSpace))
                return FullNameBracketed;
            return FullName;
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
                if (name == tables.Name && owner == tables.Owner && !Compare.IsNullOrEmpty(tables.CharAsBooleanColumns))
                {
                    foreach (string item in tables.CharAsBooleanColumns.Split(';'))
                        CharAsBooleans.Add(item, null);
                    break;
                }
            }
        }

        private void CheckForLookupTable()
        {
            LookupTablesCollection lookupTables = configuration.LookupTables;

            foreach (LookupTablesElement lookupTable in lookupTables)
            {
                if (name == lookupTable.Name && owner == lookupTable.Owner)
                {
                    if (lookupTable.Exclude)
                        return;

                    flags[isLookup] = true;
                    keyOrdinal = lookupTable.KeyOrdinal;
                    valueOrdinal = lookupTable.ValueOrdinal;
                    return;
                }
            }

            if (!Compare.IsNullOrEmpty(lookupTables.SchemaIncludes) && owner == lookupTables.SchemaIncludes)
                flags[isLookup] = true;
            else if (!Compare.IsNullOrEmpty(lookupTables.PrefixIncludes) && name.StartsWith(lookupTables.PrefixIncludes))
                flags[isLookup] = true;
            else if (!Compare.IsNullOrEmpty(lookupTables.SuffixIncludes) && name.EndsWith(lookupTables.SuffixIncludes))
                flags[isLookup] = true;

            if (flags[isLookup])
            {
                keyOrdinal = lookupTables.KeyOrdinal;
                valueOrdinal = lookupTables.ValueOrdinal;
                return;
            }
        }

        private void CheckForEnumTable()
        {
            EnumTablesCollection enumTables = configuration.EnumTables;

            foreach (EnumTablesElement element in enumTables)
            {
                if (name == element.Name && owner == element.Owner)
                {
                    if (element.Exclude)
                        return;

                    flags[isEnum] = true;
                    keyOrdinal = element.KeyOrdinal;
                    valueOrdinal = element.ValueOrdinal;
                    return;
                }
            }

            if (!Compare.IsNullOrEmpty(enumTables.SchemaIncludes) && owner == enumTables.SchemaIncludes)
                flags[isEnum] = true;
            else if (!Compare.IsNullOrEmpty(enumTables.PrefixIncludes) && name.StartsWith(enumTables.PrefixIncludes))
                flags[isEnum] = true;
            else if (!Compare.IsNullOrEmpty(enumTables.SuffixIncludes) && name.EndsWith(enumTables.SuffixIncludes))
                flags[isEnum] = true;

            if (flags[isEnum])
            {
                keyOrdinal = enumTables.KeyOrdinal;
                valueOrdinal = enumTables.ValueOrdinal;
                return;
            }
        }

        /// <summary>
        /// 
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
            return CodeFormat.ToCamelCase(name);
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string ToPascalCase()
        {
            return CodeFormat.ToPascalCase(name, CodeFormatOptions.None);
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public string ToProperCase()
        {
            return CodeFormat.ToProperCase(name);
        }

        #region Object Overrides

        /// <summary>
        /// Returns a <see cref="System.String"></see> that represents the current <see cref="TableDefinition">
        /// </see>.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"></see> that represents the current <see cref="TableDefinition"></see>.
        /// </returns>
        public override string ToString()
        {
            return FullNameBracketed;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if obj and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            TableDefinition info = obj as TableDefinition;
            if (info == null)
                return false;

            return Equals(info);
        }

        ///<summary>
        ///Indicates whether the current object is equal to another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///true if the current object is equal to the other parameter; otherwise, false.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public bool Equals(TableDefinition other)
        {
            if (!Equals(name, other.name))
                return false;
            if (!Equals(owner, other.owner))
                return false;

            return true;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return (name != null ? name.GetHashCode() : 0) + 29*(owner != null ? owner.GetHashCode() : 0);
        }

        ///<summary>
        ///Compares the current object with another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer that indicates the relative order of the objects 
        /// being compared. The return value has the following meanings: Value Meaning 
        /// Less than zero This object is less than the other parameter.Zero This object 
        /// is equal to other. Greater than zero This object is greater than other. 
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public int CompareTo(TableDefinition other)
        {
            if (other == null)
                return 0;

            return string.Compare(FullName, other.FullName, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
