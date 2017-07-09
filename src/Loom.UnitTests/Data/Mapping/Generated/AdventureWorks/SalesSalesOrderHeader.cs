#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Person;
using AdventureWorks.Purchasing;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.SalesOrderHeader table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesOrderHeader", "SalesOrderID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesOrderHeader : DataRecord<SalesOrderHeader>
    {
        private string _accountNumber;
        private int _billToAddressId;
        private string _comment;
        private string _creditCardApprovalCode;
        private int? _creditCardId;
        private int? _currencyRateId;
        private int _customerId;
        private DateTime _dueDate;
        private decimal _freight;
        private DateTime _modifiedDate;
        private bool _onlineOrderFlag;
        private DateTime _orderDate;
        private string _purchaseOrderNumber;
        private short _revisionNumber;
        private Guid _rowguid;

        private int _salesOrderId;
        private string _salesOrderNumber;
        private int? _salesPersonId;
        private DateTime? _shipDate;
        private ShipMethod _shipMethodId;
        private int _shipToAddressId;
        private short _status;
        private decimal _subTotal;
        private decimal _taxAmt;
        private int? _territoryId;
        private decimal _totalDue;

        public SalesOrderHeader() { }
        protected SalesOrderHeader(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key.
        /// </summary>
        [ActiveColumn("SalesOrderID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int SalesOrderId
        {
            get => _salesOrderId;
            set
            {
                if (value == _salesOrderId && IsPropertyDirty("SalesOrderID"))
                    return;

                _salesOrderId = value;
                MarkDirty("SalesOrderID");
            }
        }

        /// <summary>
        ///     Incremental number to track changes to the sales order over time.
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
        ///     Dates the sales order was created.
        /// </summary>
        [ActiveColumn("OrderDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
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
        ///     Date the order is due to the customer.
        /// </summary>
        [ActiveColumn("DueDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                if (value == _dueDate && IsPropertyDirty("DueDate"))
                    return;

                _dueDate = value;
                MarkDirty("DueDate");
            }
        }

        /// <summary>
        ///     Date the order was shipped to the customer.
        /// </summary>
        [ActiveColumn("ShipDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
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
        ///     Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled
        /// </summary>
        [ActiveColumn("Status", DbType.Int16, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((1))")]
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
        ///     0 = Order placed by sales person. 1 = Order placed online by customer.
        /// </summary>
        [ActiveColumn("OnlineOrderFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((1))")]
        public bool OnlineOrderFlag
        {
            get => _onlineOrderFlag;
            set
            {
                if (value == _onlineOrderFlag && IsPropertyDirty("OnlineOrderFlag"))
                    return;

                _onlineOrderFlag = value;
                MarkDirty("OnlineOrderFlag");
            }
        }

        /// <summary>
        ///     Unique sales order identification number.
        /// </summary>
        [ActiveColumn("SalesOrderNumber", DbType.String, ColumnProperties.Computed, Ordinal = 8, MaxLength = 25)]
        public string SalesOrderNumber
        {
            get => _salesOrderNumber;
            set
            {
                if (value == _salesOrderNumber && IsPropertyDirty("SalesOrderNumber"))
                    return;

                _salesOrderNumber = value;
                MarkDirty("SalesOrderNumber");
            }
        }

        /// <summary>
        ///     Customer purchase order number reference.
        /// </summary>
        [ActiveColumn("PurchaseOrderNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 25)]
        public string PurchaseOrderNumber
        {
            get => _purchaseOrderNumber;
            set
            {
                if (value == _purchaseOrderNumber && IsPropertyDirty("PurchaseOrderNumber"))
                    return;

                _purchaseOrderNumber = value;
                MarkDirty("PurchaseOrderNumber");
            }
        }

        /// <summary>
        ///     Financial accounting number reference.
        /// </summary>
        [ActiveColumn("AccountNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 15)]
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
        ///     Customer identification number. Foreign key to Customer.BusinessEntityID.
        /// </summary>
        [ActiveColumn("CustomerID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 11, MaxLength = 0)]
        [ForeignColumn("CustomerID", typeof(Customer), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.
        /// </summary>
        [ActiveColumn("SalesPersonID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 12, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(SalesPerson), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? SalesPersonId
        {
            get => _salesPersonId;
            set
            {
                if (value == _salesPersonId && IsPropertyDirty("SalesPersonID"))
                    return;

                _salesPersonId = value;
                MarkDirty("SalesPersonID");
            }
        }

        /// <summary>
        ///     Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 13, MaxLength = 0)]
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
        ///     Customer billing address. Foreign key to Address.AddressID.
        /// </summary>
        [ActiveColumn("BillToAddressID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 14, MaxLength = 0)]
        [ForeignColumn("AddressID", typeof(Address), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int BillToAddressId
        {
            get => _billToAddressId;
            set
            {
                if (value == _billToAddressId && IsPropertyDirty("BillToAddressID"))
                    return;

                _billToAddressId = value;
                MarkDirty("BillToAddressID");
            }
        }

        /// <summary>
        ///     Customer shipping address. Foreign key to Address.AddressID.
        /// </summary>
        [ActiveColumn("ShipToAddressID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 15, MaxLength = 0)]
        [ForeignColumn("AddressID", typeof(Address), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ShipToAddressId
        {
            get => _shipToAddressId;
            set
            {
                if (value == _shipToAddressId && IsPropertyDirty("ShipToAddressID"))
                    return;

                _shipToAddressId = value;
                MarkDirty("ShipToAddressID");
            }
        }

        /// <summary>
        ///     Shipping method. Foreign key to ShipMethod.ShipMethodID.
        /// </summary>
        [ActiveColumn("ShipMethodID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 16, MaxLength = 0)]
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
        ///     Credit card identification number. Foreign key to CreditCard.CreditCardID.
        /// </summary>
        [ActiveColumn("CreditCardID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 17, MaxLength = 0)]
        [ForeignColumn("CreditCardID", typeof(CreditCard), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? CreditCardId
        {
            get => _creditCardId;
            set
            {
                if (value == _creditCardId && IsPropertyDirty("CreditCardID"))
                    return;

                _creditCardId = value;
                MarkDirty("CreditCardID");
            }
        }

        /// <summary>
        ///     Approval code provided by the credit card company.
        /// </summary>
        [ActiveColumn("CreditCardApprovalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 15)]
        public string CreditCardApprovalCode
        {
            get => _creditCardApprovalCode;
            set
            {
                if (value == _creditCardApprovalCode && IsPropertyDirty("CreditCardApprovalCode"))
                    return;

                _creditCardApprovalCode = value;
                MarkDirty("CreditCardApprovalCode");
            }
        }

        /// <summary>
        ///     Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.
        /// </summary>
        [ActiveColumn("CurrencyRateID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 19, MaxLength = 0)]
        [ForeignColumn("CurrencyRateID", typeof(CurrencyRate), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? CurrencyRateId
        {
            get => _currencyRateId;
            set
            {
                if (value == _currencyRateId && IsPropertyDirty("CurrencyRateID"))
                    return;

                _currencyRateId = value;
                MarkDirty("CurrencyRateID");
            }
        }

        /// <summary>
        ///     Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.
        /// </summary>
        [ActiveColumn("SubTotal", DbType.Currency, ColumnProperties.None, Ordinal = 20, MaxLength = 0, DefaultValue = "((0.00))")]
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
        [ActiveColumn("TaxAmt", DbType.Currency, ColumnProperties.None, Ordinal = 21, MaxLength = 0, DefaultValue = "((0.00))")]
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
        [ActiveColumn("Freight", DbType.Currency, ColumnProperties.None, Ordinal = 22, MaxLength = 0, DefaultValue = "((0.00))")]
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
        ///     Total due from customer. Computed as Subtotal + TaxAmt + Freight.
        /// </summary>
        [ActiveColumn("TotalDue", DbType.Currency, ColumnProperties.Computed, Ordinal = 23, MaxLength = 0)]
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
        ///     Sales representative comments.
        /// </summary>
        [ActiveColumn("Comment", DbType.String, ColumnProperties.Nullable, Ordinal = 24, MaxLength = 128)]
        public string Comment
        {
            get => _comment;
            set
            {
                if (value == _comment && IsPropertyDirty("Comment"))
                    return;

                _comment = value;
                MarkDirty("Comment");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 25, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 26, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn SalesOrderId => FetchColumn("SalesOrderID");

            public static QueryColumn RevisionNumber => FetchColumn("RevisionNumber");

            public static QueryColumn OrderDate => FetchColumn("OrderDate");

            public static QueryColumn DueDate => FetchColumn("DueDate");

            public static QueryColumn ShipDate => FetchColumn("ShipDate");

            public static QueryColumn Status => FetchColumn("Status");

            public static QueryColumn OnlineOrderFlag => FetchColumn("OnlineOrderFlag");

            public static QueryColumn SalesOrderNumber => FetchColumn("SalesOrderNumber");

            public static QueryColumn PurchaseOrderNumber => FetchColumn("PurchaseOrderNumber");

            public static QueryColumn AccountNumber => FetchColumn("AccountNumber");

            public static QueryColumn CustomerId => FetchColumn("CustomerID");

            public static QueryColumn SalesPersonId => FetchColumn("SalesPersonID");

            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");

            public static QueryColumn BillToAddressId => FetchColumn("BillToAddressID");

            public static QueryColumn ShipToAddressId => FetchColumn("ShipToAddressID");

            public static QueryColumn ShipMethod => FetchColumn("ShipMethodID");

            public static QueryColumn CreditCardId => FetchColumn("CreditCardID");

            public static QueryColumn CreditCardApprovalCode => FetchColumn("CreditCardApprovalCode");

            public static QueryColumn CurrencyRateId => FetchColumn("CurrencyRateID");

            public static QueryColumn SubTotal => FetchColumn("SubTotal");

            public static QueryColumn TaxAmt => FetchColumn("TaxAmt");

            public static QueryColumn Freight => FetchColumn("Freight");

            public static QueryColumn TotalDue => FetchColumn("TotalDue");

            public static QueryColumn Comment => FetchColumn("Comment");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}