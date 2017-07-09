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
    ///     This is an DataRecord class which wraps the Production.ProductPhoto table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductPhoto", "ProductPhotoID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductPhoto : DataRecord<ProductPhoto>
    {
        private byte[] _largePhoto;
        private string _largePhotoFileName;
        private DateTime _modifiedDate;

        private int _productPhotoId;
        private byte[] _thumbNailPhoto;
        private string _thumbnailPhotoFileName;

        public ProductPhoto() { }
        protected ProductPhoto(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for ProductPhoto records.
        /// </summary>
        [ActiveColumn("ProductPhotoID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Small image of the product.
        /// </summary>
        [ActiveColumn("ThumbNailPhoto", DbType.Binary, ColumnProperties.Nullable, Ordinal = 2)]
        public byte[] ThumbNailPhoto
        {
            get => _thumbNailPhoto;
            set
            {
                if (value == _thumbNailPhoto && IsPropertyDirty("ThumbNailPhoto"))
                    return;

                _thumbNailPhoto = value;
                MarkDirty("ThumbNailPhoto");
            }
        }

        /// <summary>
        ///     Small image file name.
        /// </summary>
        [ActiveColumn("ThumbnailPhotoFileName", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 50)]
        public string ThumbnailPhotoFileName
        {
            get => _thumbnailPhotoFileName;
            set
            {
                if (value == _thumbnailPhotoFileName && IsPropertyDirty("ThumbnailPhotoFileName"))
                    return;

                _thumbnailPhotoFileName = value;
                MarkDirty("ThumbnailPhotoFileName");
            }
        }

        /// <summary>
        ///     Large image of the product.
        /// </summary>
        [ActiveColumn("LargePhoto", DbType.Binary, ColumnProperties.Nullable, Ordinal = 4)]
        public byte[] LargePhoto
        {
            get => _largePhoto;
            set
            {
                if (value == _largePhoto && IsPropertyDirty("LargePhoto"))
                    return;

                _largePhoto = value;
                MarkDirty("LargePhoto");
            }
        }

        /// <summary>
        ///     Large image file name.
        /// </summary>
        [ActiveColumn("LargePhotoFileName", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
        public string LargePhotoFileName
        {
            get => _largePhotoFileName;
            set
            {
                if (value == _largePhotoFileName && IsPropertyDirty("LargePhotoFileName"))
                    return;

                _largePhotoFileName = value;
                MarkDirty("LargePhotoFileName");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn ProductPhotoId => FetchColumn("ProductPhotoID");

            public static QueryColumn ThumbNailPhoto => FetchColumn("ThumbNailPhoto");

            public static QueryColumn ThumbnailPhotoFileName => FetchColumn("ThumbnailPhotoFileName");

            public static QueryColumn LargePhoto => FetchColumn("LargePhoto");

            public static QueryColumn LargePhotoFileName => FetchColumn("LargePhotoFileName");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}