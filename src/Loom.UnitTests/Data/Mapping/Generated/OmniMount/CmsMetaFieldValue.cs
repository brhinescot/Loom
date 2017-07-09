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
    ///     This is an DataRecord class which wraps the Cms.MetaFieldValue table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "MetaFieldValue", "MetaFieldValueId")]
    public class MetaFieldValue : DataRecord<MetaFieldValue>
    {
        private int _metaBagId;
        private int? _metaFieldOptionId;

        private int _metaFieldValueId;
        private string _value;

        public MetaFieldValue() { }
        protected MetaFieldValue(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldValueId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Value", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 4000)]
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
        [ActiveColumn("MetaBagId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("MetaBagId", typeof(MetaBag), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int MetaBagId
        {
            get => _metaBagId;
            set
            {
                if (value == _metaBagId && IsPropertyDirty("MetaBagId"))
                    return;

                _metaBagId = value;
                MarkDirty("MetaBagId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldOptionId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("MetaFieldOptionId", typeof(MetaFieldOption), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? MetaFieldOptionId
        {
            get => _metaFieldOptionId;
            set
            {
                if (value == _metaFieldOptionId && IsPropertyDirty("MetaFieldOptionId"))
                    return;

                _metaFieldOptionId = value;
                MarkDirty("MetaFieldOptionId");
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

            public static QueryColumn Value => FetchColumn("Value");

            public static QueryColumn MetaBagId => FetchColumn("MetaBagId");

            public static QueryColumn MetaFieldOptionId => FetchColumn("MetaFieldOptionId");
        }

        #endregion
    }
}