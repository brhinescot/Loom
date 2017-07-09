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
    ///     This is an DataRecord class which wraps the Sales.SalesOrderDetail table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesOrderDetail", "SalesOrderID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesOrderDetail : DataRecord<SalesOrderDetail>
    {
        private string _carrierTrackingNumber;
        private decimal _lineTotal;
        private DateTime _modifiedDate;
        private short _orderQty;
        private int _productId;
        private Guid _rowguid;
        private int _salesOrderDetailId;

        private int _salesOrderId;
        private int _specialOfferId;
        private decimal _unitPrice;
        private decimal _unitPriceDiscount;

        public SalesOrderDetail() { }
        protected SalesOrderDetail(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to SalesOrderHeader.SalesOrderID.
        /// </summary>
        [ActiveColumn("SalesOrderID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("SalesOrderID", typeof(SalesOrderHeader), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Primary key. One incremental unique number per product sold.
        /// </summary>
        [ActiveColumn("SalesOrderDetailID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        public int SalesOrderDetailId
        {
            get => _salesOrderDetailId;
            set
            {
                if (value == _salesOrderDetailId && IsPropertyDirty("SalesOrderDetailID"))
                    return;

                _salesOrderDetailId = value;
                MarkDirty("SalesOrderDetailID");
            }
        }

        /// <summary>
        ///     Shipment tracking number supplied by the shipper.
        /// </summary>
        [ActiveColumn("CarrierTrackingNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 25)]
        public string CarrierTrackingNumber
        {
            get => _carrierTrackingNumber;
            set
            {
                if (value == _carrierTrackingNumber && IsPropertyDirty("CarrierTrackingNumber"))
                    return;

                _carrierTrackingNumber = value;
                MarkDirty("CarrierTrackingNumber");
            }
        }

        /// <summary>
        ///     Quantity ordered per product.
        /// </summary>
        [ActiveColumn("OrderQty", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short OrderQty
        {
            get => _orderQty;
            set
            {
                if (value == _orderQty && IsPropertyDirty("OrderQty"))
                    return;

                _orderQty = value;
                MarkDirty("OrderQty");
            }
        }

        /// <summary>
        ///     Product sold to customer. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("SpecialOfferID", typeof(SpecialOfferProduct), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId && IsPropertyDirty("ProductID"))
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        ///     Promotional code. Foreign key to SpecialOffer.SpecialOfferID.
        /// </summary>
        [ActiveColumn("SpecialOfferID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("SpecialOfferID", typeof(SpecialOfferProduct), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int SpecialOfferId
        {
            get => _specialOfferId;
            set
            {
                if (value == _specialOfferId && IsPropertyDirty("SpecialOfferID"))
                    return;

                _specialOfferId = value;
                MarkDirty("SpecialOfferID");
            }
        }

        /// <summary>
        ///     Selling price of a single product.
        /// </summary>
        [ActiveColumn("UnitPrice", DbType.Currency, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value == _unitPrice && IsPropertyDirty("UnitPrice"))
                    return;

                _unitPrice = value;
                MarkDirty("UnitPrice");
            }
        }

        /// <summary>
        ///     Discount amount.
        /// </summary>
        [ActiveColumn("UnitPriceDiscount", DbType.Currency, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((0.0))")]
        public decimal UnitPriceDiscount
        {
            get => _unitPriceDiscount;
            set
            {
                if (value == _unitPriceDiscount && IsPropertyDirty("UnitPriceDiscount"))
                    return;

                _unitPriceDiscount = value;
                MarkDirty("UnitPriceDiscount");
            }
        }

        /// <summary>
        ///     Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.
        /// </summary>
        [ActiveColumn("LineTotal", DbType.Decimal, ColumnProperties.Computed, Ordinal = 9, MaxLength = 0)]
        public decimal LineTotal
        {
            get => _lineTotal;
            set
            {
                if (value == _lineTotal && IsPropertyDirty("LineTotal"))
                    return;

                _lineTotal = value;
                MarkDirty("LineTotal");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn SalesOrderDetailId => FetchColumn("SalesOrderDetailID");

            public static QueryColumn CarrierTrackingNumber => FetchColumn("CarrierTrackingNumber");

            public static QueryColumn OrderQty => FetchColumn("OrderQty");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn SpecialOfferId => FetchColumn("SpecialOfferID");

            public static QueryColumn UnitPrice => FetchColumn("UnitPrice");

            public static QueryColumn UnitPriceDiscount => FetchColumn("UnitPriceDiscount");

            public static QueryColumn LineTotal => FetchColumn("LineTotal");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}