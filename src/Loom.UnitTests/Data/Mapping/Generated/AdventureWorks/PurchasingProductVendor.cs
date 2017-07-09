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
    ///     This is an DataRecord class which wraps the Purchasing.ProductVendor table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Purchasing", "ProductVendor", "ProductID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductVendor : DataRecord<ProductVendor>
    {
        private int _averageLeadTime;
        private int _businessEntityId;
        private decimal? _lastReceiptCost;
        private DateTime? _lastReceiptDate;
        private int _maxOrderQty;
        private int _minOrderQty;
        private DateTime _modifiedDate;
        private int? _onOrderQty;

        private int _productId;
        private decimal _standardPrice;
        private string _unitMeasureCode;

        public ProductVendor() { }
        protected ProductVendor(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Primary key. Foreign key to Vendor.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Vendor), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        ///     The average span of time (in days) between placing an order with the vendor and receiving the purchased product.
        /// </summary>
        [ActiveColumn("AverageLeadTime", DbType.Int32, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public int AverageLeadTime
        {
            get => _averageLeadTime;
            set
            {
                if (value == _averageLeadTime && IsPropertyDirty("AverageLeadTime"))
                    return;

                _averageLeadTime = value;
                MarkDirty("AverageLeadTime");
            }
        }

        /// <summary>
        ///     The vendor's usual selling price.
        /// </summary>
        [ActiveColumn("StandardPrice", DbType.Currency, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public decimal StandardPrice
        {
            get => _standardPrice;
            set
            {
                if (value == _standardPrice && IsPropertyDirty("StandardPrice"))
                    return;

                _standardPrice = value;
                MarkDirty("StandardPrice");
            }
        }

        /// <summary>
        ///     The selling price when last purchased.
        /// </summary>
        [ActiveColumn("LastReceiptCost", DbType.Currency, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public decimal? LastReceiptCost
        {
            get => _lastReceiptCost;
            set
            {
                if (value == _lastReceiptCost && IsPropertyDirty("LastReceiptCost"))
                    return;

                _lastReceiptCost = value;
                MarkDirty("LastReceiptCost");
            }
        }

        /// <summary>
        ///     Date the product was last received by the vendor.
        /// </summary>
        [ActiveColumn("LastReceiptDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public DateTime? LastReceiptDate
        {
            get => _lastReceiptDate;
            set
            {
                if (value == _lastReceiptDate && IsPropertyDirty("LastReceiptDate"))
                    return;

                _lastReceiptDate = value;
                MarkDirty("LastReceiptDate");
            }
        }

        /// <summary>
        ///     The maximum quantity that should be ordered.
        /// </summary>
        [ActiveColumn("MinOrderQty", DbType.Int32, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
        public int MinOrderQty
        {
            get => _minOrderQty;
            set
            {
                if (value == _minOrderQty && IsPropertyDirty("MinOrderQty"))
                    return;

                _minOrderQty = value;
                MarkDirty("MinOrderQty");
            }
        }

        /// <summary>
        ///     The minimum quantity that should be ordered.
        /// </summary>
        [ActiveColumn("MaxOrderQty", DbType.Int32, ColumnProperties.None, Ordinal = 8, MaxLength = 0)]
        public int MaxOrderQty
        {
            get => _maxOrderQty;
            set
            {
                if (value == _maxOrderQty && IsPropertyDirty("MaxOrderQty"))
                    return;

                _maxOrderQty = value;
                MarkDirty("MaxOrderQty");
            }
        }

        /// <summary>
        ///     The quantity currently on order.
        /// </summary>
        [ActiveColumn("OnOrderQty", DbType.Int32, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        public int? OnOrderQty
        {
            get => _onOrderQty;
            set
            {
                if (value == _onOrderQty && IsPropertyDirty("OnOrderQty"))
                    return;

                _onOrderQty = value;
                MarkDirty("OnOrderQty");
            }
        }

        /// <summary>
        ///     The product's unit of measure.
        /// </summary>
        [ActiveColumn("UnitMeasureCode", DbType.String, ColumnProperties.ForeignKey, Ordinal = 10, MaxLength = 3)]
        [ForeignColumn("UnitMeasureCode", typeof(UnitMeasure), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string UnitMeasureCode
        {
            get => _unitMeasureCode;
            set
            {
                if (value == _unitMeasureCode && IsPropertyDirty("UnitMeasureCode"))
                    return;

                _unitMeasureCode = value;
                MarkDirty("UnitMeasureCode");
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
            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn AverageLeadTime => FetchColumn("AverageLeadTime");

            public static QueryColumn StandardPrice => FetchColumn("StandardPrice");

            public static QueryColumn LastReceiptCost => FetchColumn("LastReceiptCost");

            public static QueryColumn LastReceiptDate => FetchColumn("LastReceiptDate");

            public static QueryColumn MinOrderQty => FetchColumn("MinOrderQty");

            public static QueryColumn MaxOrderQty => FetchColumn("MaxOrderQty");

            public static QueryColumn OnOrderQty => FetchColumn("OnOrderQty");

            public static QueryColumn UnitMeasureCode => FetchColumn("UnitMeasureCode");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}