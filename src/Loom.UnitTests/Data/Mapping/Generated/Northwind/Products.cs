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
    ///     This is an DataRecord class which wraps the dbo.Products table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Products", "ProductID")]
    public class Products : DataRecord<Products>
    {
        private int? _categoryId;
        private bool _discontinued;

        private int _productId;
        private string _productName;
        private string _quantityPerUnit;
        private short? _reorderLevel;
        private int? _supplierId;
        private decimal? _unitPrice;
        private short? _unitsInStock;
        private short? _unitsOnOrder;

        public Products() { }
        protected Products(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("SupplierID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("SupplierID", typeof(Suppliers), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? SupplierId
        {
            get => _supplierId;
            set
            {
                if (value == _supplierId)
                    return;

                _supplierId = value;
                MarkDirty("SupplierID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("CategoryID", typeof(Categories), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? CategoryId
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
        [ActiveColumn("QuantityPerUnit", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 20)]
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
        [ActiveColumn("UnitPrice", DbType.Currency, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0, DefaultValue = "((0))")]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("UnitsInStock", DbType.Int16, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0, DefaultValue = "((0))")]
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
        [ActiveColumn("UnitsOnOrder", DbType.Int16, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
        public short? UnitsOnOrder
        {
            get => _unitsOnOrder;
            set
            {
                if (value == _unitsOnOrder)
                    return;

                _unitsOnOrder = value;
                MarkDirty("UnitsOnOrder");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ReorderLevel", DbType.Int16, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        public short? ReorderLevel
        {
            get => _reorderLevel;
            set
            {
                if (value == _reorderLevel)
                    return;

                _reorderLevel = value;
                MarkDirty("ReorderLevel");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Discontinued", DbType.Boolean, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "((0))")]
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
            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ProductName => FetchColumn("ProductName");

            public static QueryColumn SupplierId => FetchColumn("SupplierID");

            public static QueryColumn CategoryId => FetchColumn("CategoryID");

            public static QueryColumn QuantityPerUnit => FetchColumn("QuantityPerUnit");

            public static QueryColumn UnitPrice => FetchColumn("UnitPrice");

            public static QueryColumn UnitsInStock => FetchColumn("UnitsInStock");

            public static QueryColumn UnitsOnOrder => FetchColumn("UnitsOnOrder");

            public static QueryColumn ReorderLevel => FetchColumn("ReorderLevel");

            public static QueryColumn Discontinued => FetchColumn("Discontinued");
        }

        #endregion
    }
}