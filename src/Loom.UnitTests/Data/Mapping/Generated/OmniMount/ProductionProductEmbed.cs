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
using OmniMount.Portal;

#endregion

namespace OmniMount.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.ProductEmbed table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductEmbed", "ProductId")]
    public class ProductEmbed : DataRecord<ProductEmbed>
    {
        private int _embedId;

        private int _productId;

        public ProductEmbed() { }
        protected ProductEmbed(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        [ActiveColumn("EmbedId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("EmbedId", typeof(Embed), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int EmbedId
        {
            get => _embedId;
            set
            {
                if (value == _embedId && IsPropertyDirty("EmbedId"))
                    return;

                _embedId = value;
                MarkDirty("EmbedId");
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

            public static QueryColumn EmbedId => FetchColumn("EmbedId");
        }

        #endregion
    }
}