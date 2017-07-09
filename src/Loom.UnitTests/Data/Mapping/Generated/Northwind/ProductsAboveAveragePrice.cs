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
    ///     This is an DataRecord class which wraps the dbo.Products Above Average Price table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Products Above Average Price", ReadOnly = true)]
    public class ProductsAboveAveragePrice : DataRecord<ProductsAboveAveragePrice>
    {
        private string _productName;
        private decimal? _unitPrice;

        public ProductsAboveAveragePrice() { }
        protected ProductsAboveAveragePrice(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductName", DbType.String, ColumnProperties.None, Ordinal = 1, MaxLength = 40)]
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
        [ActiveColumn("UnitPrice", DbType.Currency, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public decimal? UnitPrice
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductName => FetchColumn("ProductName");

            public static QueryColumn UnitPrice => FetchColumn("UnitPrice");
        }

        #endregion
    }
}