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

namespace OmniMount.Cms
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Cms.ImageType table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "ImageType", "ImageTypeId")]
    public class ImageType : DataRecord<ImageType>
    {
        private int _imageTypeId;
        private string _name;
        private string _pattern;
        private string _rootPath;

        public ImageType() { }
        protected ImageType(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ImageTypeId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 20)]
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
        [ActiveColumn("RootPath", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50, DefaultValue = "('~/')")]
        public string RootPath
        {
            get => _rootPath;
            set
            {
                if (value == _rootPath && IsPropertyDirty("RootPath"))
                    return;

                _rootPath = value;
                MarkDirty("RootPath");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Pattern", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 150)]
        public string Pattern
        {
            get => _pattern;
            set
            {
                if (value == _pattern && IsPropertyDirty("Pattern"))
                    return;

                _pattern = value;
                MarkDirty("Pattern");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ImageTypeId => FetchColumn("ImageTypeId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn RootPath => FetchColumn("RootPath");

            public static QueryColumn Pattern => FetchColumn("Pattern");
        }

        #endregion
    }
}