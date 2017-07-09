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
    ///     This is an DataRecord class which wraps the dbo.Summary of Sales by Year table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Summary of Sales by Year", ReadOnly = true)]
    public class SummaryOfSalesByYear : DataRecord<SummaryOfSalesByYear>
    {
        private int _orderId;

        private DateTime? _shippedDate;
        private decimal? _subtotal;

        public SummaryOfSalesByYear() { }
        protected SummaryOfSalesByYear(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShippedDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 1, MaxLength = 0)]
        public DateTime? ShippedDate
        {
            get => _shippedDate;
            set
            {
                if (value == _shippedDate)
                    return;

                _shippedDate = value;
                MarkDirty("ShippedDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("OrderID", DbType.Int32, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
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
        [ActiveColumn("Subtotal", DbType.Currency, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
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
            public static QueryColumn ShippedDate => FetchColumn("ShippedDate");

            public static QueryColumn OrderId => FetchColumn("OrderID");

            public static QueryColumn Subtotal => FetchColumn("Subtotal");
        }

        #endregion
    }
}