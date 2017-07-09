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
    ///     This is an DataRecord class which wraps the dbo.Sales Totals by Amount table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Sales Totals by Amount", ReadOnly = true)]
    public class SalesTotalsByAmount : DataRecord<SalesTotalsByAmount>
    {
        private string _companyName;
        private int _orderId;

        private decimal? _saleAmount;
        private DateTime? _shippedDate;

        public SalesTotalsByAmount() { }
        protected SalesTotalsByAmount(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("SaleAmount", DbType.Currency, ColumnProperties.Nullable, Ordinal = 1, MaxLength = 0)]
        public decimal? SaleAmount
        {
            get => _saleAmount;
            set
            {
                if (value == _saleAmount)
                    return;

                _saleAmount = value;
                MarkDirty("SaleAmount");
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
        [ActiveColumn("CompanyName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 40)]
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (value == _companyName)
                    return;

                _companyName = value;
                MarkDirty("CompanyName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShippedDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn SaleAmount => FetchColumn("SaleAmount");

            public static QueryColumn OrderId => FetchColumn("OrderID");

            public static QueryColumn CompanyName => FetchColumn("CompanyName");

            public static QueryColumn ShippedDate => FetchColumn("ShippedDate");
        }

        #endregion
    }
}