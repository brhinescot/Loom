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
    ///     This is an DataRecord class which wraps the dbo.Products by Category table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Products by Category", ReadOnly = true)]
    public class ProductsByCategory : DataRecord<ProductsByCategory>
    {
        private string _categoryName;
        private bool _discontinued;
        private string _productName;
        private string _quantityPerUnit;
        private short? _unitsInStock;

        public ProductsByCategory() { }
        protected ProductsByCategory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryName", DbType.String, ColumnProperties.None, Ordinal = 1, MaxLength = 15)]
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
        [ActiveColumn("ProductName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 40)]
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
        [ActiveColumn("QuantityPerUnit", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 20)]
        public string QuantityPerUnit
        {
            get => _quantityPerUnit;
            set
            {
                if (value == _quantityPerUnit)
                    return;

                _quantityPerUnit = value;
                MarkDirty("QuantityPerUnit");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UnitsInStock", DbType.Int16, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public short? UnitsInStock
        {
            get => _unitsInStock;
            set
            {
                if (value == _unitsInStock)
                    return;

                _unitsInStock = value;
                MarkDirty("UnitsInStock");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Discontinued", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public bool Discontinued
        {
            get => _discontinued;
            set
            {
                if (value == _discontinued)
                    return;

                _discontinued = value;
                MarkDirty("Discontinued");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CategoryName => FetchColumn("CategoryName");

            public static QueryColumn ProductName => FetchColumn("ProductName");

            public static QueryColumn QuantityPerUnit => FetchColumn("QuantityPerUnit");

            public static QueryColumn UnitsInStock => FetchColumn("UnitsInStock");

            public static QueryColumn Discontinued => FetchColumn("Discontinued");
        }

        #endregion
    }
}