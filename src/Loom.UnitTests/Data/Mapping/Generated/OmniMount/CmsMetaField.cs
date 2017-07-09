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
using OmniMount.Portal;

#endregion

namespace OmniMount.Cms
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Cms.MetaField table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "MetaField", "MetaFieldId")]
    public class MetaField : DataRecord<MetaField>
    {
        private int _applicationId;

        private int _metaFieldId;
        private string _name;

        public MetaField() { }
        protected MetaField(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ApplicationId", typeof(Application), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                if (value == _applicationId && IsPropertyDirty("ApplicationId"))
                    return;

                _applicationId = value;
                MarkDirty("ApplicationId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MetaFieldId => FetchColumn("MetaFieldId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}