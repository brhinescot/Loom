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
    ///     This is an DataRecord class which wraps the dbo.Order Subtotals table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Order Subtotals", ReadOnly = true)]
    public class OrderSubtotals : DataRecord<OrderSubtotals>
    {
        private int _orderId;
        private decimal? _subtotal;

        public OrderSubtotals() { }
        protected OrderSubtotals(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        [ActiveColumn("Subtotal", DbType.Currency, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public decimal? Subtotal
        {
            get => _subtotal;
            set
            {
                if (value == _subtotal)
                    return;

                _subtotal = value;
                MarkDirty("Subtotal");
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

            public static QueryColumn Subtotal => FetchColumn("Subtotal");
        }

        #endregion
    }
}