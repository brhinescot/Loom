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
    ///     This is an DataRecord class which wraps the Production.ProductProductPhoto table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductProductPhoto", "ProductPhotoID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductProductPhoto : DataRecord<ProductProductPhoto>
    {
        private DateTime _modifiedDate;
        private bool _primary;

        private int _productId;
        private int _productPhotoId;

        public ProductProductPhoto() { }
        protected ProductProductPhoto(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Product identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId && IsPropertyDirty("ProductID"))
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        ///     Product photo identification number. Foreign key to ProductPhoto.ProductPhotoID.
        /// </summary>
        [ActiveColumn("ProductPhotoID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductPhotoID", typeof(ProductPhoto), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductPhotoId
        {
            get => _productPhotoId;
            set
            {
                if (value == _productPhotoId && IsPropertyDirty("ProductPhotoID"))
                    return;

                _productPhotoId = value;
                MarkDirty("ProductPhotoID");
            }
        }

        /// <summary>
        ///     0 = Photo is not the principal image. 1 = Photo is the principal image.
        /// </summary>
        [ActiveColumn("Primary", DbType.Boolean, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((0))")]
        public bool Primary
        {
            get => _primary;
            set
            {
                if (value == _primary && IsPropertyDirty("Primary"))
                    return;

                _primary = value;
                MarkDirty("Primary");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ProductPhotoId => FetchColumn("ProductPhotoID");

            public static QueryColumn Primary => FetchColumn("Primary");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}