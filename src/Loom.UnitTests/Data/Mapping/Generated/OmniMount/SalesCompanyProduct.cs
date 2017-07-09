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
using OmniMount.Production;

#endregion

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.CompanyProduct table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "CompanyProduct", "ProductVariantId")]
    public class CompanyProduct : DataRecord<CompanyProduct>
    {
        private int _companyId;
        private string _custom1;
        private string _custom2;
        private string _custom3;
        private string _dealerUrl;
        private int _productVariantId;

        public CompanyProduct() { }
        protected CompanyProduct(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("CompanyId", typeof(Company), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int CompanyId
        {
            get => _companyId;
            set
            {
                if (value == _companyId && IsPropertyDirty("CompanyId"))
                    return;

                _companyId = value;
                MarkDirty("CompanyId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductVariantId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
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
        [ActiveColumn("DealerUrl", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 200)]
        public string DealerUrl
        {
            get => _dealerUrl;
            set
            {
                if (value == _dealerUrl && IsPropertyDirty("DealerUrl"))
                    return;

                _dealerUrl = value;
                MarkDirty("DealerUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Custom1", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 200)]
        public string Custom1
        {
            get => _custom1;
            set
            {
                if (value == _custom1 && IsPropertyDirty("Custom1"))
                    return;

                _custom1 = value;
                MarkDirty("Custom1");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Custom2", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 200)]
        public string Custom2
        {
            get => _custom2;
            set
            {
                if (value == _custom2 && IsPropertyDirty("Custom2"))
                    return;

                _custom2 = value;
                MarkDirty("Custom2");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Custom3", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 200)]
        public string Custom3
        {
            get => _custom3;
            set
            {
                if (value == _custom3 && IsPropertyDirty("Custom3"))
                    return;

                _custom3 = value;
                MarkDirty("Custom3");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CompanyId => FetchColumn("CompanyId");

            public static QueryColumn ProductVariantId => FetchColumn("ProductVariantId");

            public static QueryColumn DealerUrl => FetchColumn("DealerUrl");

            public static QueryColumn Custom1 => FetchColumn("Custom1");

            public static QueryColumn Custom2 => FetchColumn("Custom2");

            public static QueryColumn Custom3 => FetchColumn("Custom3");
        }

        #endregion
    }
}