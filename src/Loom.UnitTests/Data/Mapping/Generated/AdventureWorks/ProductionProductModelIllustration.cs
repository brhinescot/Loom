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
    ///     This is an DataRecord class which wraps the Production.ProductModelIllustration table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductModelIllustration", "ProductModelID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductModelIllustration : DataRecord<ProductModelIllustration>
    {
        private int _illustrationId;
        private DateTime _modifiedDate;

        private int _productModelId;

        public ProductModelIllustration() { }
        protected ProductModelIllustration(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to ProductModel.ProductModelID.
        /// </summary>
        [ActiveColumn("ProductModelID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ProductModelID", typeof(ProductModel), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductModelId
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
        ///     Primary key. Foreign key to Illustration.IllustrationID.
        /// </summary>
        [ActiveColumn("IllustrationID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("IllustrationID", typeof(Illustration), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int IllustrationId
        {
            get => _illustrationId;
            set
            {
                if (value == _illustrationId && IsPropertyDirty("IllustrationID"))
                    return;

                _illustrationId = value;
                MarkDirty("IllustrationID");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn ProductModelId => FetchColumn("ProductModelID");

            public static QueryColumn IllustrationId => FetchColumn("IllustrationID");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}