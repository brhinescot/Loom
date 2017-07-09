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

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.Product table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Product", "ProductID", ModifiedOnColumn = "ModifiedDate")]
    public class Product : DataRecord<Product>
    {
        private string _class;
        private string _color;
        private int _daysToManufacture;
        private DateTime? _discontinuedDate;
        private bool _finishedGoodsFlag;
        private decimal _listPrice;
        private bool _makeFlag;
        private DateTime _modifiedDate;
        private string _name;

        private int _productId;
        private string _productLine;
        private int? _productModelId;
        private string _productNumber;
        private int? _productSubcategoryId;
        private short _reorderPoint;
        private Guid _rowguid;
        private short _safetyStockLevel;
        private DateTime? _sellEndDate;
        private DateTime _sellStartDate;
        private string _size;
        private string _sizeUnitMeasureCode;
        private decimal _standardCost;
        private string _style;
        private decimal? _weight;
        private string _weightUnitMeasureCode;

        public Product() { }
        protected Product(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Product records.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Name of the product.
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name && IsPropertyDirty("Name"))
                    return;

                _name = value;
                MarkDirty("Name");
            }
        }

        /// <summary>
        ///     Unique product identification number.
        /// </summary>
        [ActiveColumn("ProductNumber", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 25)]
        public string ProductNumber
        {
            get => _productNumber;
            set
            {
                if (value == _productNumber && IsPropertyDirty("ProductNumber"))
                    return;

                _productNumber = value;
                MarkDirty("ProductNumber");
            }
        }

        /// <summary>
        ///     0 = Product is purchased, 1 = Product is manufactured in-house.
        /// </summary>
        [ActiveColumn("MakeFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((1))")]
        public bool MakeFlag
        {
            get => _makeFlag;
            set
            {
                if (value == _makeFlag && IsPropertyDirty("MakeFlag"))
                    return;

                _makeFlag = value;
                MarkDirty("MakeFlag");
            }
        }

        /// <summary>
        ///     0 = Product is not a salable item. 1 = Product is salable.
        /// </summary>
        [ActiveColumn("FinishedGoodsFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((1))")]
        public bool FinishedGoodsFlag
        {
            get => _finishedGoodsFlag;
            set
            {
                if (value == _finishedGoodsFlag && IsPropertyDirty("FinishedGoodsFlag"))
                    return;

                _finishedGoodsFlag = value;
                MarkDirty("FinishedGoodsFlag");
            }
        }

        /// <summary>
        ///     Product color.
        /// </summary>
        [ActiveColumn("Color", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 15)]
        public string Color
        {
            get => _color;
            set
            {
                if (value == _color && IsPropertyDirty("Color"))
                    return;

                _color = value;
                MarkDirty("Color");
            }
        }

        /// <summary>
        ///     Minimum inventory quantity.
        /// </summary>
        [ActiveColumn("SafetyStockLevel", DbType.Int16, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
        public short SafetyStockLevel
        {
            get => _safetyStockLevel;
            set
            {
                if (value == _safetyStockLevel && IsPropertyDirty("SafetyStockLevel"))
                    return;

                _safetyStockLevel = value;
                MarkDirty("SafetyStockLevel");
            }
        }

        /// <summary>
        ///     Inventory level that triggers a purchase order or work order.
        /// </summary>
        [ActiveColumn("ReorderPoint", DbType.Int16, ColumnProperties.None, Ordinal = 8, MaxLength = 0)]
        public short ReorderPoint
        {
            get => _reorderPoint;
            set
            {
                if (value == _reorderPoint && IsPropertyDirty("ReorderPoint"))
                    return;

                _reorderPoint = value;
                MarkDirty("ReorderPoint");
            }
        }

        /// <summary>
        ///     Standard cost of the product.
        /// </summary>
        [ActiveColumn("StandardCost", DbType.Currency, ColumnProperties.None, Ordinal = 9, MaxLength = 0)]
        public decimal StandardCost
        {
            get => _standardCost;
            set
            {
                if (value == _standardCost && IsPropertyDirty("StandardCost"))
                    return;

                _standardCost = value;
                MarkDirty("StandardCost");
            }
        }

        /// <summary>
        ///     Selling price.
        /// </summary>
        [ActiveColumn("ListPrice", DbType.Currency, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
        public decimal ListPrice
        {
            get => _listPrice;
            set
            {
                if (value == _listPrice && IsPropertyDirty("ListPrice"))
                    return;

                _listPrice = value;
                MarkDirty("ListPrice");
            }
        }

        /// <summary>
        ///     Product size.
        /// </summary>
        [ActiveColumn("Size", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 5)]
        public string Size
        {
            get => _size;
            set
            {
                if (value == _size && IsPropertyDirty("Size"))
                    return;

                _size = value;
                MarkDirty("Size");
            }
        }

        /// <summary>
        ///     Unit of measure for Size column.
        /// </summary>
        [ActiveColumn("SizeUnitMeasureCode", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 12, MaxLength = 3)]
        [ForeignColumn("UnitMeasureCode", typeof(UnitMeasure), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string SizeUnitMeasureCode
        {
            get => _sizeUnitMeasureCode;
            set
            {
                if (value == _sizeUnitMeasureCode && IsPropertyDirty("SizeUnitMeasureCode"))
                    return;

                _sizeUnitMeasureCode = value;
                MarkDirty("SizeUnitMeasureCode");
            }
        }

        /// <summary>
        ///     Unit of measure for Weight column.
        /// </summary>
        [ActiveColumn("WeightUnitMeasureCode", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 13, MaxLength = 3)]
        [ForeignColumn("UnitMeasureCode", typeof(UnitMeasure), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string WeightUnitMeasureCode
        {
            get => _weightUnitMeasureCode;
            set
            {
                if (value == _weightUnitMeasureCode && IsPropertyDirty("WeightUnitMeasureCode"))
                    return;

                _weightUnitMeasureCode = value;
                MarkDirty("WeightUnitMeasureCode");
            }
        }

        /// <summary>
        ///     Product weight.
        /// </summary>
        [ActiveColumn("Weight", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 14, MaxLength = 0)]
        public decimal? Weight
        {
            get => _weight;
            set
            {
                if (value == _weight && IsPropertyDirty("Weight"))
                    return;

                _weight = value;
                MarkDirty("Weight");
            }
        }

        /// <summary>
        ///     Number of days required to manufacture the product.
        /// </summary>
        [ActiveColumn("DaysToManufacture", DbType.Int32, ColumnProperties.None, Ordinal = 15, MaxLength = 0)]
        public int DaysToManufacture
        {
            get => _daysToManufacture;
            set
            {
                if (value == _daysToManufacture && IsPropertyDirty("DaysToManufacture"))
                    return;

                _daysToManufacture = value;
                MarkDirty("DaysToManufacture");
            }
        }

        /// <summary>
        ///     R = Road, M = Mountain, T = Touring, S = Standard
        /// </summary>
        [ActiveColumn("ProductLine", DbType.String, ColumnProperties.Nullable, Ordinal = 16, MaxLength = 2)]
        public string ProductLine
        {
            get => _productLine;
            set
            {
                if (value == _productLine && IsPropertyDirty("ProductLine"))
                    return;

                _productLine = value;
                MarkDirty("ProductLine");
            }
        }

        /// <summary>
        ///     H = High, M = Medium, L = Low
        /// </summary>
        [ActiveColumn("Class", DbType.String, ColumnProperties.Nullable, Ordinal = 17, MaxLength = 2)]
        public string Class
        {
            get => _class;
            set
            {
                if (value == _class && IsPropertyDirty("Class"))
                    return;

                _class = value;
                MarkDirty("Class");
            }
        }

        /// <summary>
        ///     W = Womens, M = Mens, U = Universal
        /// </summary>
        [ActiveColumn("Style", DbType.String, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 2)]
        public string Style
        {
            get => _style;
            set
            {
                if (value == _style && IsPropertyDirty("Style"))
                    return;

                _style = value;
                MarkDirty("Style");
            }
        }

        /// <summary>
        ///     Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID.
        /// </summary>
        [ActiveColumn("ProductSubcategoryID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 19, MaxLength = 0)]
        [ForeignColumn("ProductSubcategoryID", typeof(ProductSubcategory), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ProductSubcategoryId
        {
            get => _productSubcategoryId;
            set
            {
                if (value == _productSubcategoryId && IsPropertyDirty("ProductSubcategoryID"))
                    return;

                _productSubcategoryId = value;
                MarkDirty("ProductSubcategoryID");
            }
        }

        /// <summary>
        ///     Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
        /// </summary>
        [ActiveColumn("ProductModelID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 20, MaxLength = 0)]
        [ForeignColumn("ProductModelID", typeof(ProductModel), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ProductModelId
        {
            get => _productModelId;
            set
            {
                if (value == _productModelId && IsPropertyDirty("ProductModelID"))
                    return;

                _productModelId = value;
                MarkDirty("ProductModelID");
            }
        }

        /// <summary>
        ///     Date the product was available for sale.
        /// </summary>
        [ActiveColumn("SellStartDate", DbType.DateTime, ColumnProperties.None, Ordinal = 21, MaxLength = 0)]
        public DateTime SellStartDate
        {
            get => _sellStartDate;
            set
            {
                if (value == _sellStartDate && IsPropertyDirty("SellStartDate"))
                    return;

                _sellStartDate = value;
                MarkDirty("SellStartDate");
            }
        }

        /// <summary>
        ///     Date the product was no longer available for sale.
        /// </summary>
        [ActiveColumn("SellEndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 22, MaxLength = 0)]
        public DateTime? SellEndDate
        {
            get => _sellEndDate;
            set
            {
                if (value == _sellEndDate && IsPropertyDirty("SellEndDate"))
                    return;

                _sellEndDate = value;
                MarkDirty("SellEndDate");
            }
        }

        /// <summary>
        ///     Date the product was discontinued.
        /// </summary>
        [ActiveColumn("DiscontinuedDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 23, MaxLength = 0)]
        public DateTime? DiscontinuedDate
        {
            get => _discontinuedDate;
            set
            {
                if (value == _discontinuedDate && IsPropertyDirty("DiscontinuedDate"))
                    return;

                _discontinuedDate = value;
                MarkDirty("DiscontinuedDate");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 24, MaxLength = 0, DefaultValue = "(newid())")]
        public Guid Rowguid
        {
            get => _rowguid;
            set
            {
                if (value == _rowguid && IsPropertyDirty("rowguid"))
                    return;

                _rowguid = value;
                MarkDirty("rowguid");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 25, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ProductNumber => FetchColumn("ProductNumber");

            public static QueryColumn MakeFlag => FetchColumn("MakeFlag");

            public static QueryColumn FinishedGoodsFlag => FetchColumn("FinishedGoodsFlag");

            public static QueryColumn Color => FetchColumn("Color");

            public static QueryColumn SafetyStockLevel => FetchColumn("SafetyStockLevel");

            public static QueryColumn ReorderPoint => FetchColumn("ReorderPoint");

            public static QueryColumn StandardCost => FetchColumn("StandardCost");

            public static QueryColumn ListPrice => FetchColumn("ListPrice");

            public static QueryColumn Size => FetchColumn("Size");

            public static QueryColumn SizeUnitMeasureCode => FetchColumn("SizeUnitMeasureCode");

            public static QueryColumn WeightUnitMeasureCode => FetchColumn("WeightUnitMeasureCode");

            public static QueryColumn Weight => FetchColumn("Weight");

            public static QueryColumn DaysToManufacture => FetchColumn("DaysToManufacture");

            public static QueryColumn ProductLine => FetchColumn("ProductLine");

            public static QueryColumn Class => FetchColumn("Class");

            public static QueryColumn Style => FetchColumn("Style");

            public static QueryColumn ProductSubcategoryId => FetchColumn("ProductSubcategoryID");

            public static QueryColumn ProductModelId => FetchColumn("ProductModelID");

            public static QueryColumn SellStartDate => FetchColumn("SellStartDate");

            public static QueryColumn SellEndDate => FetchColumn("SellEndDate");

            public static QueryColumn DiscontinuedDate => FetchColumn("DiscontinuedDate");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}