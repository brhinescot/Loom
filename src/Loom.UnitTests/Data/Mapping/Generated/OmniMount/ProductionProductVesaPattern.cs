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
    ///     This is an DataRecord class which wraps the Production.ProductVesaPattern table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductVesaPattern", "VesaPatternId")]
    public class ProductVesaPattern : DataRecord<ProductVesaPattern>
    {
        private int _productId;
        private int _vesaPatternId;

        public ProductVesaPattern() { }
        protected ProductVesaPattern(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        [ActiveColumn("VesaPatternId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("VesaPatternId", typeof(VesaPattern), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int VesaPatternId
        {
            get => _vesaPatternId;
            set
            {
                if (value == _vesaPatternId && IsPropertyDirty("VesaPatternId"))
                    return;

                _vesaPatternId = value;
                MarkDirty("VesaPatternId");
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

            public static QueryColumn VesaPatternId => FetchColumn("VesaPatternId");
        }

        #endregion
    }
}