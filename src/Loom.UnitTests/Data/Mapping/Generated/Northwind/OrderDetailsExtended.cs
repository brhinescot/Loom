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

namespace Northwind
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.Order Details Extended table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Order Details Extended", ReadOnly = true)]
    public class OrderDetailsExtended : DataRecord<OrderDetailsExtended>
    {
        private decimal _discount;
        private decimal? _extendedPrice;

        private int _orderId;
        private int _productId;
        private string _productName;
        private short _quantity;
        private decimal _unitPrice;

        public OrderDetailsExtended() { }
        protected OrderDetailsExtended(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("OrderID", DbType.Int32, ColumnProperties.None, Ordinal = 1, MaxLength = 0)]
        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value == _orderId)
                    return;

                _orderId = value;
                MarkDirty("OrderID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId)
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 40)]
        public string ProductName
        {
            get => _productName;
            set
            {
                if (value == _productName)
                    return;

                _productName = value;
                MarkDirty("ProductName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UnitPrice", DbType.Currency, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value == _unitPrice)
                    return;

                _unitPrice = value;
                MarkDirty("UnitPrice");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Quantity", DbType.Int16, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public short Quantity
        {
            get => _quantity;
            set
            {
                if (value == _quantity)
                    return;

                _quantity = value;
                MarkDirty("Quantity");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Discount", DbType.Decimal, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
        public decimal Discount
        {
            get => _discount;
            set
            {
                if (value == _discount)
                    return;

                _discount = value;
                MarkDirty("Discount");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ExtendedPrice", DbType.Currency, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public decimal? ExtendedPrice
        {
            get => _extendedPrice;
            set
            {
                if (value == _extendedPrice)
                    return;

                _extendedPrice = value;
                MarkDirty("ExtendedPrice");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn OrderId => FetchColumn("OrderID");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ProductName => FetchColumn("ProductName");

            public static QueryColumn UnitPrice => FetchColumn("UnitPrice");

            public static QueryColumn Quantity => FetchColumn("Quantity");

            public static QueryColumn Discount => FetchColumn("Discount");

            public static QueryColumn ExtendedPrice => FetchColumn("ExtendedPrice");
        }

        #endregion
    }
}