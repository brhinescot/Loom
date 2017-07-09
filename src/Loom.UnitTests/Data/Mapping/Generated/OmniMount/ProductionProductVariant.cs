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
    ///     This is an DataRecord class which wraps the Production.ProductVariant table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductVariant", "ProductVariantId", DeletedColumn = "Deleted")]
    public class ProductVariant : DataRecord<ProductVariant>
    {
        private string _alternateUrl;
        private int _applicationId;
        private string _buyNowUrl;
        private DateTime _dateCreated;
        private bool _deleted;
        private string _description;
        private bool _featured;
        private bool _live;
        private string _name;
        private int _productTypeId;

        private int _productVariantId;

        public ProductVariant() { }
        protected ProductVariant(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductVariantId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ApplicationId", typeof(Application), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                if (value == _applicationId && IsPropertyDirty("ApplicationId"))
                    return;

                _applicationId = value;
                MarkDirty("ApplicationId");
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Live", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public bool Live
        {
            get => _live;
            set
            {
                if (value == _live && IsPropertyDirty("Live"))
                    return;

                _live = value;
                MarkDirty("Live");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Featured", DbType.Boolean, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((0))")]
        public bool Featured
        {
            get => _featured;
            set
            {
                if (value == _featured && IsPropertyDirty("Featured"))
                    return;

                _featured = value;
                MarkDirty("Featured");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AlternateUrl", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 200)]
        public string AlternateUrl
        {
            get => _alternateUrl;
            set
            {
                if (value == _alternateUrl && IsPropertyDirty("AlternateUrl"))
                    return;

                _alternateUrl = value;
                MarkDirty("AlternateUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BuyNowUrl", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 200)]
        public string BuyNowUrl
        {
            get => _buyNowUrl;
            set
            {
                if (value == _buyNowUrl && IsPropertyDirty("BuyNowUrl"))
                    return;

                _buyNowUrl = value;
                MarkDirty("BuyNowUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductTypeId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        [ForeignColumn("ProductTypeId", typeof(ProductType), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductTypeId
        {
            get => _productTypeId;
            set
            {
                if (value == _productTypeId && IsPropertyDirty("ProductTypeId"))
                    return;

                _productTypeId = value;
                MarkDirty("ProductTypeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "((0))")]
        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value == _deleted && IsPropertyDirty("Deleted"))
                    return;

                _deleted = value;
                MarkDirty("Deleted");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DateCreated", DbType.DateTime, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                if (value == _dateCreated && IsPropertyDirty("DateCreated"))
                    return;

                _dateCreated = value;
                MarkDirty("DateCreated");
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

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn Live => FetchColumn("Live");

            public static QueryColumn Featured => FetchColumn("Featured");

            public static QueryColumn AlternateUrl => FetchColumn("AlternateUrl");

            public static QueryColumn BuyNowUrl => FetchColumn("BuyNowUrl");

            public static QueryColumn ProductTypeId => FetchColumn("ProductTypeId");

            public static QueryColumn Deleted => FetchColumn("Deleted");

            public static QueryColumn DateCreated => FetchColumn("DateCreated");
        }

        #endregion
    }
}