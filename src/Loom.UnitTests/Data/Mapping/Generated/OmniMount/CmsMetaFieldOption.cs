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
    ///     This is an DataRecord class which wraps the Cms.MetaFieldOption table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "MetaFieldOption", "MetaFieldOptionId")]
    public class MetaFieldOption : DataRecord<MetaFieldOption>
    {
        private int _metaFieldId;

        private int _metaFieldOptionId;
        private string _name;
        private int _ordinal;

        public MetaFieldOption() { }
        protected MetaFieldOption(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldOptionId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int MetaFieldOptionId
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

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("MetaFieldId", typeof(MetaField), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int MetaFieldId
        {
            get => _metaFieldId;
            set
            {
                if (value == _metaFieldId && IsPropertyDirty("MetaFieldId"))
                    return;

                _metaFieldId = value;
                MarkDirty("MetaFieldId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
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
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MetaFieldOptionId => FetchColumn("MetaFieldOptionId");

            public static QueryColumn MetaFieldId => FetchColumn("MetaFieldId");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}