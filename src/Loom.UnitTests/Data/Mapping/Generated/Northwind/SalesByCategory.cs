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
    ///     This is an DataRecord class which wraps the dbo.Sales by Category table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Sales by Category", ReadOnly = true)]
    public class SalesByCategory : DataRecord<SalesByCategory>
    {
        private int _categoryId;
        private string _categoryName;
        private string _productName;
        private decimal? _productSales;

        public SalesByCategory() { }
        protected SalesByCategory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryID", DbType.Int32, ColumnProperties.None, Ordinal = 1, MaxLength = 0)]
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (value == _categoryId)
                    return;

                _categoryId = value;
                MarkDirty("CategoryID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 15)]
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                if (value == _categoryName)
                    return;

                _categoryName = value;
                MarkDirty("CategoryName");
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
        [ActiveColumn("ProductSales", DbType.Currency, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public decimal? ProductSales
        {
            get => _productSales;
            set
            {
                if (value == _productSales)
                    return;

                _productSales = value;
                MarkDirty("ProductSales");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CategoryId => FetchColumn("CategoryID");

            public static QueryColumn CategoryName => FetchColumn("CategoryName");

            public static QueryColumn ProductName => FetchColumn("ProductName");

            public static QueryColumn ProductSales => FetchColumn("ProductSales");
        }

        #endregion
    }
}