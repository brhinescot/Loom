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

namespace OmniMount.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.ProductCompatibility table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductCompatibility", "ProductCompatibilityId")]
    public class ProductCompatibility : DataRecord<ProductCompatibility>
    {
        private decimal _holeMaxHeight;
        private decimal _holeMaxWidth;
        private decimal _holeMinHeight;
        private decimal _holeMinWidth;
        private bool _m8Compatible;
        private decimal _maxScreenSize;
        private decimal _maxWeight;
        private decimal _minScreenSize;
        private decimal _minWeight;

        private int _productCompatibilityId;
        private int _productVariantId;

        public ProductCompatibility() { }
        protected ProductCompatibility(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductCompatibilityId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ProductCompatibilityId
        {
            get => _productCompatibilityId;
            set
            {
                if (value == _productCompatibilityId && IsPropertyDirty("ProductCompatibilityId"))
                    return;

                _productCompatibilityId = value;
                MarkDirty("ProductCompatibilityId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductVariantId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductVariantId", typeof(ProductVariant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductVariantId
        {
            get => _productVariantId;
            set
            {
                if (value == _productVariantId && IsPropertyDirty("ProductVariantId"))
                    return;

                _productVariantId = value;
                MarkDirty("ProductVariantId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HoleMinWidth", DbType.Decimal, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((0))")]
        public decimal HoleMinWidth
        {
            get => _holeMinWidth;
            set
            {
                if (value == _holeMinWidth && IsPropertyDirty("HoleMinWidth"))
                    return;

                _holeMinWidth = value;
                MarkDirty("HoleMinWidth");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HoleMaxWidth", DbType.Decimal, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public decimal HoleMaxWidth
        {
            get => _holeMaxWidth;
            set
            {
                if (value == _holeMaxWidth && IsPropertyDirty("HoleMaxWidth"))
                    return;

                _holeMaxWidth = value;
                MarkDirty("HoleMaxWidth");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HoleMinHeight", DbType.Decimal, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public decimal HoleMinHeight
        {
            get => _holeMinHeight;
            set
            {
                if (value == _holeMinHeight && IsPropertyDirty("HoleMinHeight"))
                    return;

                _holeMinHeight = value;
                MarkDirty("HoleMinHeight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HoleMaxHeight", DbType.Decimal, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((0))")]
        public decimal HoleMaxHeight
        {
            get => _holeMaxHeight;
            set
            {
                if (value == _holeMaxHeight && IsPropertyDirty("HoleMaxHeight"))
                    return;

                _holeMaxHeight = value;
                MarkDirty("HoleMaxHeight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MinWeight", DbType.Decimal, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0))")]
        public decimal MinWeight
        {
            get => _minWeight;
            set
            {
                if (value == _minWeight && IsPropertyDirty("MinWeight"))
                    return;

                _minWeight = value;
                MarkDirty("MinWeight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MaxWeight", DbType.Decimal, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
        public decimal MaxWeight
        {
            get => _maxWeight;
            set
            {
                if (value == _maxWeight && IsPropertyDirty("MaxWeight"))
                    return;

                _maxWeight = value;
                MarkDirty("MaxWeight");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MinScreenSize", DbType.Decimal, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        public decimal MinScreenSize
        {
            get => _minScreenSize;
            set
            {
                if (value == _minScreenSize && IsPropertyDirty("MinScreenSize"))
                    return;

                _minScreenSize = value;
                MarkDirty("MinScreenSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MaxScreenSize", DbType.Decimal, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "((0))")]
        public decimal MaxScreenSize
        {
            get => _maxScreenSize;
            set
            {
                if (value == _maxScreenSize && IsPropertyDirty("MaxScreenSize"))
                    return;

                _maxScreenSize = value;
                MarkDirty("MaxScreenSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("M8Compatible", DbType.Boolean, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "((1))")]
        public bool M8Compatible
        {
            get => _m8Compatible;
            set
            {
                if (value == _m8Compatible && IsPropertyDirty("M8Compatible"))
                    return;

                _m8Compatible = value;
                MarkDirty("M8Compatible");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductCompatibilityId => FetchColumn("ProductCompatibilityId");

            public static QueryColumn ProductVariantId => FetchColumn("ProductVariantId");

            public static QueryColumn HoleMinWidth => FetchColumn("HoleMinWidth");

            public static QueryColumn HoleMaxWidth => FetchColumn("HoleMaxWidth");

            public static QueryColumn HoleMinHeight => FetchColumn("HoleMinHeight");

            public static QueryColumn HoleMaxHeight => FetchColumn("HoleMaxHeight");

            public static QueryColumn MinWeight => FetchColumn("MinWeight");

            public static QueryColumn MaxWeight => FetchColumn("MaxWeight");

            public static QueryColumn MinScreenSize => FetchColumn("MinScreenSize");

            public static QueryColumn MaxScreenSize => FetchColumn("MaxScreenSize");

            public static QueryColumn M8Compatible => FetchColumn("M8Compatible");
        }

        #endregion
    }
}