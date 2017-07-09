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

namespace Loom.Web.Portal.Data.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.MetaBag table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "MetaBag", "MetaBagId")]
    public class MetaBag : DataRecord<MetaBag>
    {
        private int _entityId;

        private int _metaBagId;
        private int _metaFieldId;
        private string _value;

        public MetaBag() { }
        protected MetaBag(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaBagId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("EntityId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("EntityId", typeof(EntityBase), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int EntityId
        {
            get => _entityId;
            set
            {
                if (value == _entityId && IsPropertyDirty("EntityId"))
                    return;

                _entityId = value;
                MarkDirty("EntityId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
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
        [ActiveColumn("Value", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 4000)]
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
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MetaBagId => FetchColumn("MetaBagId");

            public static QueryColumn EntityId => FetchColumn("EntityId");

            public static QueryColumn MetaFieldId => FetchColumn("MetaFieldId");

            public static QueryColumn Value => FetchColumn("Value");
        }

        #endregion
    }
}