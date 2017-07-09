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

namespace OmniMount.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.Localization table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Localization", "LocalizationId")]
    public class Localization : DataRecord<Localization>
    {
        private byte[] _binFile;
        private string _filename;
        private string _locale;

        private int _localizationId;
        private string _resource;
        private string _resourceSet;
        private string _textFile;
        private string _type;
        private string _value;

        public Localization() { }
        protected Localization(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("LocalizationId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int LocalizationId
        {
            get => _localizationId;
            set
            {
                if (value == _localizationId && IsPropertyDirty("LocalizationId"))
                    return;

                _localizationId = value;
                MarkDirty("LocalizationId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Resource", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 30, DefaultValue = "('')")]
        public string Resource
        {
            get => _resource;
            set
            {
                if (value == _resource && IsPropertyDirty("Resource"))
                    return;

                _resource = value;
                MarkDirty("Resource");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Value", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 1073741823, DefaultValue = "('')")]
        public string Value
        {
            get => _value;
            set
            {
                if (value == _value && IsPropertyDirty("Value"))
                    return;

                _value = value;
                MarkDirty("Value");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Locale", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 5, DefaultValue = "('')")]
        public string Locale
        {
            get => _locale;
            set
            {
                if (value == _locale && IsPropertyDirty("Locale"))
                    return;

                _locale = value;
                MarkDirty("Locale");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ResourceSet", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 512, DefaultValue = "('')")]
        public string ResourceSet
        {
            get => _resourceSet;
            set
            {
                if (value == _resourceSet && IsPropertyDirty("ResourceSet"))
                    return;

                _resourceSet = value;
                MarkDirty("ResourceSet");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Type", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 255, DefaultValue = "('')")]
        public string Type
        {
            get => _type;
            set
            {
                if (value == _type && IsPropertyDirty("Type"))
                    return;

                _type = value;
                MarkDirty("Type");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BinFile", DbType.Binary, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 2147483647)]
        public byte[] BinFile
        {
            get => _binFile;
            set
            {
                if (value == _binFile && IsPropertyDirty("BinFile"))
                    return;

                _binFile = value;
                MarkDirty("BinFile");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TextFile", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 1073741823)]
        public string TextFile
        {
            get => _textFile;
            set
            {
                if (value == _textFile && IsPropertyDirty("TextFile"))
                    return;

                _textFile = value;
                MarkDirty("TextFile");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Filename", DbType.String, ColumnProperties.None, Ordinal = 9, MaxLength = 128, DefaultValue = "('')")]
        public string Filename
        {
            get => _filename;
            set
            {
                if (value == _filename && IsPropertyDirty("Filename"))
                    return;

                _filename = value;
                MarkDirty("Filename");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn LocalizationId => FetchColumn("LocalizationId");

            public static QueryColumn Resource => FetchColumn("Resource");

            public static QueryColumn Value => FetchColumn("Value");

            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn ResourceSet => FetchColumn("ResourceSet");

            public static QueryColumn Type => FetchColumn("Type");

            public static QueryColumn BinFile => FetchColumn("BinFile");

            public static QueryColumn TextFile => FetchColumn("TextFile");

            public static QueryColumn Filename => FetchColumn("Filename");
        }

        #endregion
    }
}