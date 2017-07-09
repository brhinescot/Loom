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

namespace OmniMount.Cms
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Cms.CategoryImage table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "CategoryImage", "ImageId")]
    public class CategoryImage : DataRecord<CategoryImage>
    {
        private int _categoryId;
        private int _imageId;
        private int _imageTypeId;
        private int _ordinal;

        public CategoryImage() { }
        protected CategoryImage(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("CategoryId", typeof(Category), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (value == _categoryId && IsPropertyDirty("CategoryId"))
                    return;

                _categoryId = value;
                MarkDirty("CategoryId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ImageId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ImageId", typeof(Image), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ImageId
        {
            get => _imageId;
            set
            {
                if (value == _imageId && IsPropertyDirty("ImageId"))
                    return;

                _imageId = value;
                MarkDirty("ImageId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((1))")]
        public int Ordinal
        {
            get => _ordinal;
            set
            {
                if (value == _ordinal && IsPropertyDirty("Ordinal"))
                    return;

                _ordinal = value;
                MarkDirty("Ordinal");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ImageTypeId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("ImageTypeId", typeof(ImageType), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ImageTypeId
        {
            get => _imageTypeId;
            set
            {
                if (value == _imageTypeId && IsPropertyDirty("ImageTypeId"))
                    return;

                _imageTypeId = value;
                MarkDirty("ImageTypeId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CategoryId => FetchColumn("CategoryId");

            public static QueryColumn ImageId => FetchColumn("ImageId");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn ImageTypeId => FetchColumn("ImageTypeId");
        }

        #endregion
    }
}