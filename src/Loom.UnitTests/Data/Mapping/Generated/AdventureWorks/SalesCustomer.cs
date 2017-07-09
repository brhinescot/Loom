#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.Customer table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "Customer", "CustomerID", ModifiedOnColumn = "ModifiedDate")]
    public class Customer : DataRecord<Customer>
    {
        private string _accountNumber;

        private int _customerId;
        private DateTime _modifiedDate;
        private int? _personId;
        private Guid _rowguid;
        private int? _storeId;
        private int? _territoryId;

        public Customer() { }
        protected Customer(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key.
        /// </summary>
        [ActiveColumn("CustomerID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (value == _customerId && IsPropertyDirty("CustomerID"))
                    return;

                _customerId = value;
                MarkDirty("CustomerID");
            }
        }

        /// <summary>
        ///     Foreign key to Person.BusinessEntityID
        /// </summary>
        [ActiveColumn("PersonID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Person.Person), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? PersonId
        {
            get => _personId;
            set
            {
                if (value == _personId && IsPropertyDirty("PersonID"))
                    return;

                _personId = value;
                MarkDirty("PersonID");
            }
        }

        /// <summary>
        ///     Foreign key to Store.BusinessEntityID
        /// </summary>
        [ActiveColumn("StoreID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Store), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? StoreId
        {
            get => _storeId;
            set
            {
                if (value == _storeId && IsPropertyDirty("StoreID"))
                    return;

                _storeId = value;
                MarkDirty("StoreID");
            }
        }

        /// <summary>
        ///     ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("TerritoryID", typeof(SalesTerritory), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? TerritoryId
        {
            get => _territoryId;
            set
            {
                if (value == _territoryId && IsPropertyDirty("TerritoryID"))
                    return;

                _territoryId = value;
                MarkDirty("TerritoryID");
            }
        }

        /// <summary>
        ///     Unique number identifying the customer assigned by the accounting system.
        /// </summary>
        [ActiveColumn("AccountNumber", DbType.String, ColumnProperties.Computed, Ordinal = 5, MaxLength = 10)]
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value == _accountNumber && IsPropertyDirty("AccountNumber"))
                    return;

                _accountNumber = value;
                MarkDirty("AccountNumber");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(newid())")]
        public Guid Rowguid
        {
            get => _rowguid;
            set
            {
                if (value == _rowguid && IsPropertyDirty("rowguid"))
                    return;

                _rowguid = value;
                MarkDirty("rowguid");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                if (value == _modifiedDate && IsPropertyDirty("ModifiedDate"))
                    return;

                _modifiedDate = value;
                MarkDirty("ModifiedDate");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CustomerId => FetchColumn("CustomerID");

            public static QueryColumn PersonId => FetchColumn("PersonID");

            public static QueryColumn StoreId => FetchColumn("StoreID");

            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");

            public static QueryColumn AccountNumber => FetchColumn("AccountNumber");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}