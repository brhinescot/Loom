#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [DebuggerDisplay("{Owner,nq}.{Name,nq}, PrimaryKey={PrimaryKey[0].Name,nq}")]
    public class TableData : ISchema
    {
        #region Instance Fields

        private readonly Type dataRecordType;

        #endregion

        #region Property Accessors

        #region Public Properties

        public QueryableColumns Columns { get; private set; }
        public QueryColumn CreatedByColumn { get; private set; }
        public QueryColumn CreatedOnColumn { get; private set; }
        public QueryColumn DeletedColumn { get; private set; }

        public bool HasCreatedByColumn
        {
            get { return !Equals(CreatedByColumn, null); }
        }

        public bool HasCreatedOnColumn
        {
            get { return !Equals(CreatedOnColumn, null); }
        }

        public bool HasDeletedColumn
        {
            get { return !Equals(DeletedColumn, null); }
        }

        public bool HasIdentityColumn
        {
            get { return !Equals(IdentityColumn, null); }
        }

        public bool HasModifiedByColumn
        {
            get { return !Equals(ModifiedByColumn, null); }
        }

        public bool HasModifiedOnColumn
        {
            get { return !Equals(ModifiedOnColumn, null); }
        }

        public bool HasPrimaryKey
        {
            get { return PrimaryKey.Exists; }
        }

        public QueryColumn IdentityColumn { get; private set; }
        public bool IsReadOnly { get; private set; }

        public QueryColumn ModifiedByColumn { get; private set; }
        public QueryColumn ModifiedOnColumn { get; private set; }
        public PrimaryKeys PrimaryKey { get; private set; }

        #endregion

        public string Datasource { get; private set; }

        public string Owner { get; private set; }

        public string Name { get; private set; }

        #endregion

        #region .ctor

        private TableData(Type dataRecordType)
        {
            this.dataRecordType = dataRecordType;
        }

        #endregion

        internal static TableData CreateUnitialized(Type dataRecordType)
        {
            return new TableData(dataRecordType);
        }

        /// <summary>
        /// </summary>
        /// <param name="dataRecordType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="dataRecordType" /> does not have a public static 'Table'
        ///     property or the property has not been initialized.
        /// </exception>
        internal static TableData FromInitializedDataRecord(Type dataRecordType)
        {
            const BindingFlags bindings = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
            PropertyInfo property = dataRecordType.GetProperty("Table", bindings);

            if (property == null)
                throw new ArgumentException("The 'Table' property does not exist.");

            TableData table = property.GetValue(null, null) as TableData;
            if (table == null)
                throw new ArgumentException("The 'Table' property has not been initialized.");

            return table;
        }

        public QueryColumn FindColumn(string name)
        {
            return Columns.FindColumn(name);
        }

        internal ColumnPredicate CreatePredicate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            ColumnPredicate predicate = PrimaryKey.CreatePredicate(record);
            if (!Equals(predicate, null))
                return predicate;

            foreach (var property in record.GetUpdatedProperties())
            {
                object value = property.InvokeGetter(record);
                if (value == null)
                    continue; // TODO: Maybe handle setting null on DataRecords and creating predicates that look for the null.

                if (Equals(predicate, null))
                    predicate = (Columns.FindColumn(property.Name) == value);
                else
                    predicate = (predicate & (Columns.FindColumn(property.Name) == value));
            }

            return predicate;
        }

        internal void Initialize()
        {
            ActiveTableAttribute attribute = Attribute.GetCustomAttribute(dataRecordType, typeof (ActiveTableAttribute)) as ActiveTableAttribute;
            if (attribute == null)
            {
                Datasource = string.Empty;
                Owner = "dbo";
                Name = dataRecordType.Name;
                IsReadOnly = false;
                return;
            }

            Datasource = string.Empty;
            Owner = attribute.Owner;
            Name = attribute.Name;
            IsReadOnly = attribute.ReadOnly;

            Columns = new QueryableColumns();
            foreach (QueryColumn column in TableColumnData.CreateColumns(this, dataRecordType))
                Columns.Add(column);

            PrimaryKey = new PrimaryKeys(this);
            IdentityColumn = PrimaryKey.GetIdentityColumn();
            DeletedColumn = attribute.DeletedColumn == null ? null : Columns.FindColumn(attribute.DeletedColumn);
            CreatedOnColumn = attribute.CreatedOnColumn == null ? null : Columns.FindColumn(attribute.CreatedOnColumn);
            ModifiedOnColumn = attribute.ModifiedOnColumn == null ? null : Columns.FindColumn(attribute.ModifiedOnColumn);
            CreatedByColumn = attribute.CreatedByColumn == null ? null : Columns.FindColumn(attribute.CreatedByColumn);
            ModifiedByColumn = attribute.ModifiedByColumn == null ? null : Columns.FindColumn(attribute.ModifiedByColumn);
        }

        internal TableData Copy()
        {
            TableData table = CreateUnitialized(dataRecordType);
            table.Initialize();
            return table;
        }
    }

    /// <summary>
    /// </summary>
    public class PrimaryKeys : IEnumerable<QueryColumn>
    {
        #region Member Fields

        private readonly QueryColumn[] keys;

        #endregion

        #region Public Properties

        public bool Exists
        {
            get { return keys != null && keys.Length > 0 && !Equals(keys[0], null) && !Compare.IsNullOrEmpty(keys[0].Name); }
        }

        public QueryColumn this[int index]
        {
            get
            {
                if (index >= keys.Length)
                    throw new ArgumentOutOfRangeException("index", index, null);

                return keys[index];
            }
        }

        public bool Multiple
        {
            get { return keys.Length > 1; }
        }

        #endregion

        #region .ctors

        internal PrimaryKeys(TableData table)
        {
            List<QueryColumn> primaryKeys = new List<QueryColumn>();
            foreach (var column in table.Columns)
            {
                if ((column.ColumnProperties & ColumnProperties.PrimaryKey) == ColumnProperties.PrimaryKey)
                    primaryKeys.Add(column);
            }
            keys = primaryKeys.ToArray();
        }

        #endregion

        #region IEnumerable<QueryColumn> Members

        public IEnumerator<QueryColumn> GetEnumerator()
        {
            for (int i = 0; i < keys.Length; i++)
                yield return keys[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return keys.GetEnumerator();
        }

        #endregion

        public ColumnPredicate CreatePredicate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            ColumnPredicate predicate = null;
            foreach (QueryColumn key in keys)
            {
                object value = record[key];
                if (value == null)
                    throw new InvalidOperationException(SR.ExPrimaryKeyValueNotSet);

                if (Equals(predicate, null))
                    predicate = (key == value);
                else
                    predicate = (predicate & (key == value));
            }

            return predicate;
        }

        public QueryColumn GetIdentityColumn()
        {
            foreach (QueryColumn key in keys)
            {
                if ((key.ColumnProperties & ColumnProperties.Identity) == ColumnProperties.Identity)
                    return key;
            }

            return null;
        }
    }
}