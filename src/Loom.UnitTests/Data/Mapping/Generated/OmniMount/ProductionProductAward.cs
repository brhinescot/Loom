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
    ///     This is an DataRecord class which wraps the Production.ProductAward table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductAward", "ProductVariantId")]
    public class ProductAward : DataRecord<ProductAward>
    {
        private int _awardId;
        private string _description;

        private int _productVariantId;
        private string _summary;

        public ProductAward() { }
        protected ProductAward(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        [ActiveColumn("AwardId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("AwardId", typeof(Award), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int AwardId
        {
            get => _awardId;
            set
            {
                if (value == _awardId && IsPropertyDirty("AwardId"))
                    return;

                _awardId = value;
                MarkDirty("AwardId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Summary", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 200)]
        public string Summary
        {
            get => _summary;
            set
            {
                if (value == _summary && IsPropertyDirty("Summary"))
                    return;

                _summary = value;
                MarkDirty("Summary");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 4000)]
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

            public static QueryColumn AwardId => FetchColumn("AwardId");

            public static QueryColumn Summary => FetchColumn("Summary");

            public static QueryColumn Description => FetchColumn("Description");
        }

        #endregion
    }
}