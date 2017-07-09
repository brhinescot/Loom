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
    ///     This is an DataRecord class which wraps the Production.ProductAccessory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductAccessory", "ProductId")]
    public class ProductAccessory : DataRecord<ProductAccessory>
    {
        private int _accessoryId;
        private int _productId;

        public ProductAccessory() { }
        protected ProductAccessory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("AccessoryId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ProductVariantId", typeof(ProductVariant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int AccessoryId
        {
            get => _accessoryId;
            set
            {
                if (value == _accessoryId && IsPropertyDirty("AccessoryId"))
                    return;

                _accessoryId = value;
                MarkDirty("AccessoryId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn AccessoryId => FetchColumn("AccessoryId");

            public static QueryColumn ProductId => FetchColumn("ProductId");
        }

        #endregion
    }
}