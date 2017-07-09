#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Production;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Purchasing
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Purchasing.PurchaseOrderDetail table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Purchasing", "PurchaseOrderDetail", "PurchaseOrderID", ModifiedOnColumn = "ModifiedDate")]
    public class PurchaseOrderDetail : DataRecord<PurchaseOrderDetail>
    {
        private DateTime _dueDate;
        private decimal _lineTotal;
        private DateTime _modifiedDate;
        private short _orderQty;
        private int _productId;
        private int _purchaseOrderDetailId;

        private int _purchaseOrderId;
        private decimal _receivedQty;
        private decimal _rejectedQty;
        private decimal _stockedQty;
        private decimal _unitPrice;

        public PurchaseOrderDetail() { }
        protected PurchaseOrderDetail(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to PurchaseOrderHeader.PurchaseOrderID.
        /// </summary>
        [ActiveColumn("PurchaseOrderID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("PurchaseOrderID", typeof(PurchaseOrderHeader), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Primary key. One line number per purchased product.
        /// </summary>
        [ActiveColumn("PurchaseOrderDetailID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        public int PurchaseOrderDetailId
        {
            get => _purchaseOrderDetailId;
            set
            {
                if (value == _purchaseOrderDetailId && IsPropertyDirty("PurchaseOrderDetailID"))
                    return;

                _purchaseOrderDetailId = value;
                MarkDirty("PurchaseOrderDetailID");
            }
        }

        /// <summary>
        ///     Date the product is expected to be received.
        /// </summary>
        [ActiveColumn("DueDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
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
        ///     Quantity ordered.
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
        ///     Product identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Vendor's selling price of a single product.
        /// </summary>
        [ActiveColumn("UnitPrice", DbType.Currency, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
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
        ///     Per product subtotal. Computed as OrderQty * UnitPrice.
        /// </summary>
        [ActiveColumn("LineTotal", DbType.Currency, ColumnProperties.Computed, Ordinal = 7, MaxLength = 0)]
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
        ///     Quantity actually received from the vendor.
        /// </summary>
        [ActiveColumn("ReceivedQty", DbType.Decimal, ColumnProperties.None, Ordinal = 8, MaxLength = 0)]
        public decimal ReceivedQty
        {
            get => _receivedQty;
            set
            {
                if (value == _receivedQty && IsPropertyDirty("ReceivedQty"))
                    return;

                _receivedQty = value;
                MarkDirty("ReceivedQty");
            }
        }

        /// <summary>
        ///     Quantity rejected during inspection.
        /// </summary>
        [ActiveColumn("RejectedQty", DbType.Decimal, ColumnProperties.None, Ordinal = 9, MaxLength = 0)]
        public decimal RejectedQty
        {
            get => _rejectedQty;
            set
            {
                if (value == _rejectedQty && IsPropertyDirty("RejectedQty"))
                    return;

                _rejectedQty = value;
                MarkDirty("RejectedQty");
            }
        }

        /// <summary>
        ///     Quantity accepted into inventory. Computed as ReceivedQty - RejectedQty.
        /// </summary>
        [ActiveColumn("StockedQty", DbType.Decimal, ColumnProperties.Computed, Ordinal = 10, MaxLength = 0)]
        public decimal StockedQty
        {
            get => _stockedQty;
            set
            {
                if (value == _stockedQty && IsPropertyDirty("StockedQty"))
                    return;

                _stockedQty = value;
                MarkDirty("StockedQty");
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
            public static QueryColumn PurchaseOrderId => FetchColumn("PurchaseOrderID");

            public static QueryColumn PurchaseOrderDetailId => FetchColumn("PurchaseOrderDetailID");

            public static QueryColumn DueDate => FetchColumn("DueDate");

            public static QueryColumn OrderQty => FetchColumn("OrderQty");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn UnitPrice => FetchColumn("UnitPrice");

            public static QueryColumn LineTotal => FetchColumn("LineTotal");

            public static QueryColumn ReceivedQty => FetchColumn("ReceivedQty");

            public static QueryColumn RejectedQty => FetchColumn("RejectedQty");

            public static QueryColumn StockedQty => FetchColumn("StockedQty");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}