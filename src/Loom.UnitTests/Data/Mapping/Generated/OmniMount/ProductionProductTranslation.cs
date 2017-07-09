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
    ///     This is an DataRecord class which wraps the Production.ProductTranslation table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductTranslation", "ProductVariantId")]
    public class ProductTranslation : DataRecord<ProductTranslation>
    {
        private string _description;
        private string _locale;
        private string _name;

        private int _productVariantId;

        public ProductTranslation() { }
        protected ProductTranslation(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductVariantId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Locale", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 5)]
        public string Locale
        {
            get => _locale;
            set
            {
                if (value == _locale && IsPropertyDirty("Locale"))
                    return;

                _locale = value;
                MarkDirty("Locale");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 200)]
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
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 4000)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description && IsPropertyDirty("Description"))
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductVariantId => FetchColumn("ProductVariantId");

            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");
        }

        #endregion
    }
}