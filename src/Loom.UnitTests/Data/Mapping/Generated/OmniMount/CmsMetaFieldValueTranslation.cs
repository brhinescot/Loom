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
    ///     This is an DataRecord class which wraps the Cms.MetaFieldValueTranslation table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "MetaFieldValueTranslation", "MetaFieldValueId")]
    public class MetaFieldValueTranslation : DataRecord<MetaFieldValueTranslation>
    {
        private string _locale;

        private int _metaFieldValueId;
        private string _value;

        public MetaFieldValueTranslation() { }
        protected MetaFieldValueTranslation(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldValueId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("MetaFieldValueId", typeof(MetaFieldValue), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int MetaFieldValueId
        {
            get => _metaFieldValueId;
            set
            {
                if (value == _metaFieldValueId && IsPropertyDirty("MetaFieldValueId"))
                    return;

                _metaFieldValueId = value;
                MarkDirty("MetaFieldValueId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Locale", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 5)]
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
        [ActiveColumn("Value", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 4000)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MetaFieldValueId => FetchColumn("MetaFieldValueId");

            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn Value => FetchColumn("Value");
        }

        #endregion
    }
}