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
    ///     This is an DataRecord class which wraps the Cms.Image table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "Image", "ImageId")]
    public class Image : DataRecord<Image>
    {
        private string _fileName;

        private int _imageId;

        public Image() { }
        protected Image(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ImageId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("FileName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 42)]
        public string FileName
        {
            get => _fileName;
            set
            {
                if (value == _fileName && IsPropertyDirty("FileName"))
                    return;

                _fileName = value;
                MarkDirty("FileName");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ImageId => FetchColumn("ImageId");

            public static QueryColumn FileName => FetchColumn("FileName");
        }

        #endregion
    }
}