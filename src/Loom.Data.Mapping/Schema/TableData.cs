#region Using Directives

using System;
using System.Diagnostics;
using System.Reflection;
using Loom.Data.Mapping.Query;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [DebuggerDisplay("{Owner,nq}.{Name,nq}, PrimaryKey={PrimaryKey[0].Name,nq}")]
    public class TableData : ITable
    {
        private readonly Type dataRecordType;

        private TableData(Type dataRecordType)
        {
            this.dataRecordType = dataRecordType;
            Initialize();
        }

        #region ITable Members

        public string Datasource { get; private set; }
        public string Owner { get; private set; }
        public string Name { get; private set; }
        public bool IsReadOnly { get; private set; }

        public QueryableColumns Columns { get; private set; }
        public QueryColumn CreatedByColumn { get; private set; }
        public QueryColumn CreatedOnColumn { get; private set; }
        public QueryColumn DeletedColumn { get; private set; }
        public QueryColumn IdentityColumn { get; private set; }
        public QueryColumn ModifiedByColumn { get; private set; }
        public QueryColumn ModifiedOnColumn { get; private set; }
        public PrimaryKeys PrimaryKey { get; private set; }

        public bool HasCreatedByColumn => !Equals(CreatedByColumn, null);

        public bool HasCreatedOnColumn => !Equals(CreatedOnColumn, null);

        public bool HasDeletedColumn => !Equals(DeletedColumn, null);

        public bool HasIdentityColumn => !Equals(IdentityColumn, null);

        public bool HasModifiedByColumn => !Equals(ModifiedByColumn, null);

        public bool HasModifiedOnColumn => !Equals(ModifiedOnColumn, null);

        public bool HasPrimaryKey => PrimaryKey.Exists;

        public QueryColumn FindColumn(string name)
        {
            return Columns.FindColumn(name);
        }

        public ColumnPredicate CreatePredicate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            ColumnPredicate predicate = PrimaryKey.CreatePredicate(record);
            if (!Equals(predicate, null))
                return predicate;

            foreach (DynamicProperty<TDataRecord> property in record.GetUpdatedProperties())
            {
                object value = property.InvokeGetterOn(record);
                if (value == null)
                    continue; // TODO: Maybe handle setting null on DataRecords and creating predicates that look for the null.

                if (Equals(predicate, null))
                    predicate = Columns.FindColumn(property.Name) == value;
                else
                    predicate = predicate & (Columns.FindColumn(property.Name) == value);
            }

            return predicate;
        }

        #endregion

        internal static ITable CreateUnitialized(Type dataRecordType)
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
        internal static ITable FromInitializedDataRecord(Type dataRecordType)
        {
            const BindingFlags bindings = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
            PropertyInfo property = dataRecordType.GetProperty("Table", bindings);

            if (property == null)
                throw new ArgumentException("The 'Table' property does not exist.");

            ITable table = property.GetValue(null, null) as ITable;
            if (table == null)
                throw new ArgumentException("The 'Table' property has not been initialized.");

            return table;
        }

        private void Initialize()
        {
            ActiveTableAttribute attribute = Attribute.GetCustomAttribute(dataRecordType, typeof(ActiveTableAttribute)) as ActiveTableAttribute;
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

        internal ITable Copy()
        {
            ITable table = CreateUnitialized(dataRecordType);
            return table;
        }
    }
}