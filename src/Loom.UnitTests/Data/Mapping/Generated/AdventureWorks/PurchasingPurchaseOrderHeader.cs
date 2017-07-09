#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.HumanResources;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Purchasing
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Purchasing.PurchaseOrderHeader table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Purchasing", "PurchaseOrderHeader", "PurchaseOrderID", ModifiedOnColumn = "ModifiedDate")]
    public class PurchaseOrderHeader : DataRecord<PurchaseOrderHeader>
    {
        private int _employeeId;
        private decimal _freight;
        private DateTime _modifiedDate;
        private DateTime _orderDate;

        private int _purchaseOrderId;
        private short _revisionNumber;
        private DateTime? _shipDate;
        private ShipMethod _shipMethodId;
        private short _status;
        private decimal _subTotal;
        private decimal _taxAmt;
        private decimal _totalDue;
        private int _vendorId;

        public PurchaseOrderHeader() { }
        protected PurchaseOrderHeader(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key.
        /// </summary>
        [ActiveColumn("PurchaseOrderID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int PurchaseOrderId
        {
            get => _purchaseOrderId;
            set
            {
                if (value == _purchaseOrderId && IsPropertyDirty("PurchaseOrderID"))
                    return;

                _purchaseOrderId = value;
                MarkDirty("PurchaseOrderID");
            }
        }

        /// <summary>
        ///     Incremental number to track changes to the purchase order over time.
        /// </summary>
        [ActiveColumn("RevisionNumber", DbType.Int16, ColumnProperties.None, Ordinal = 2, MaxLength = 0, DefaultValue = "((0))")]
        public short RevisionNumber
        {
            get => _revisionNumber;
            set
            {
                if (value == _revisionNumber && IsPropertyDirty("RevisionNumber"))
                    return;

                _revisionNumber = value;
                MarkDirty("RevisionNumber");
            }
        }

        /// <summary>
        ///     Order current status. 1 = Pending; 2 = Approved; 3 = Rejected; 4 = Complete
        /// </summary>
        [ActiveColumn("Status", DbType.Int16, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((1))")]
        public short Status
        {
            get => _status;
            set
            {
                if (value == _status && IsPropertyDirty("Status"))
                    return;

                _status = value;
                MarkDirty("Status");
            }
        }

        /// <summary>
        ///     Employee who created the purchase order. Foreign key to Employee.BusinessEntityID.
        /// </summary>
        [ActiveColumn("EmployeeID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Employee), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                if (value == _employeeId && IsPropertyDirty("EmployeeID"))
                    return;

                _employeeId = value;
                MarkDirty("EmployeeID");
            }
        }

        /// <summary>
        ///     Vendor with whom the purchase order is placed. Foreign key to Vendor.BusinessEntityID.
        /// </summary>
        [ActiveColumn("VendorID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Vendor), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int VendorId
        {
            get => _vendorId;
            set
            {
                if (value == _vendorId && IsPropertyDirty("VendorID"))
                    return;

                _vendorId = value;
                MarkDirty("VendorID");
            }
        }

        /// <summary>
        ///     Shipping method. Foreign key to ShipMethod.ShipMethodID.
        /// </summary>
        [ActiveColumn("ShipMethodID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("ShipMethodID", typeof(ShipMethod), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public ShipMethod ShipMethod
        {
            get => _shipMethodId;
            set
            {
                if (value == _shipMethodId)
                    return;

                _shipMethodId = value;
                MarkDirty("ShipMethodID");
            }
        }

        /// <summary>
        ///     Purchase order creation date.
        /// </summary>
        [ActiveColumn("OrderDate", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime OrderDate
        {
            get => _orderDate;
            set
            {
                if (value == _orderDate && IsPropertyDirty("OrderDate"))
                    return;

                _orderDate = value;
                MarkDirty("OrderDate");
            }
        }

        /// <summary>
        ///     Estimated shipment date from the vendor.
        /// </summary>
        [ActiveColumn("ShipDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public DateTime? ShipDate
        {
            get => _shipDate;
            set
            {
                if (value == _shipDate && IsPropertyDirty("ShipDate"))
                    return;

                _shipDate = value;
                MarkDirty("ShipDate");
            }
        }

        /// <summary>
        ///     Purchase order subtotal. Computed as SUM(PurchaseOrderDetail.LineTotal)for the appropriate PurchaseOrderID.
        /// </summary>
        [ActiveColumn("SubTotal", DbType.Currency, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal SubTotal
        {
            get => _subTotal;
            set
            {
                if (value == _subTotal && IsPropertyDirty("SubTotal"))
                    return;

                _subTotal = value;
                MarkDirty("SubTotal");
            }
        }

        /// <summary>
        ///     Tax amount.
        /// </summary>
        [ActiveColumn("TaxAmt", DbType.Currency, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal TaxAmt
        {
            get => _taxAmt;
            set
            {
                if (value == _taxAmt && IsPropertyDirty("TaxAmt"))
                    return;

                _taxAmt = value;
                MarkDirty("TaxAmt");
            }
        }

        /// <summary>
        ///     Shipping cost.
        /// </summary>
        [ActiveColumn("Freight", DbType.Currency, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal Freight
        {
            get => _freight;
            set
            {
                if (value == _freight && IsPropertyDirty("Freight"))
                    return;

                _freight = value;
                MarkDirty("Freight");
            }
        }

        /// <summary>
        ///     Total due to vendor. Computed as Subtotal + TaxAmt + Freight.
        /// </summary>
        [ActiveColumn("TotalDue", DbType.Currency, ColumnProperties.Computed, Ordinal = 12, MaxLength = 0)]
        public decimal TotalDue
        {
            get => _totalDue;
            set
            {
                if (value == _totalDue && IsPropertyDirty("TotalDue"))
                    return;

                _totalDue = value;
                MarkDirty("TotalDue");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 13, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn PurchaseOrderId => FetchColumn("PurchaseOrderID");

            public static QueryColumn RevisionNumber => FetchColumn("RevisionNumber");

            public static QueryColumn Status => FetchColumn("Status");

            public static QueryColumn EmployeeId => FetchColumn("EmployeeID");

            public static QueryColumn VendorId => FetchColumn("VendorID");

            public static QueryColumn ShipMethod => FetchColumn("ShipMethodID");

            public static QueryColumn OrderDate => FetchColumn("OrderDate");

            public static QueryColumn ShipDate => FetchColumn("ShipDate");

            public static QueryColumn SubTotal => FetchColumn("SubTotal");

            public static QueryColumn TaxAmt => FetchColumn("TaxAmt");

            public static QueryColumn Freight => FetchColumn("Freight");

            public static QueryColumn TotalDue => FetchColumn("TotalDue");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}