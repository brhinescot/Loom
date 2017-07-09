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
    ///     This is an DataRecord class which wraps the Production.ProductAdapter table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductAdapter", "ProductId")]
    public class ProductAdapter : DataRecord<ProductAdapter>
    {
        private int _adapterId;

        private int _productId;

        public ProductAdapter() { }
        protected ProductAdapter(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ProductVariantId", typeof(ProductVariant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId && IsPropertyDirty("ProductId"))
                    return;

                _productId = value;
                MarkDirty("ProductId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AdapterId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductVariantId", typeof(ProductVariant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int AdapterId
        {
            get => _adapterId;
            set
            {
                if (value == _adapterId && IsPropertyDirty("AdapterId"))
                    return;

                _adapterId = value;
                MarkDirty("AdapterId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductId => FetchColumn("ProductId");

            public static QueryColumn AdapterId => FetchColumn("AdapterId");
        }

        #endregion
    }
}