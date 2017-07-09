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
    ///     This is an DataRecord class which wraps the Cms.MetaBag table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Cms", "MetaBag", "MetaBagId")]
    public class MetaBag : DataRecord<MetaBag>
    {
        private int _itemId;

        private int _metaBagId;
        private int _metaFieldId;
        private int _metaItemTypeId;

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
        [ActiveColumn("ItemId", DbType.Int32, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
        public int ItemId
        {
            get => _itemId;
            set
            {
                if (value == _itemId && IsPropertyDirty("ItemId"))
                    return;

                _itemId = value;
                MarkDirty("ItemId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaItemTypeId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ItemTypeId", typeof(ItemType), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int MetaItemTypeId
        {
            get => _metaItemTypeId;
            set
            {
                if (value == _metaItemTypeId && IsPropertyDirty("MetaItemTypeId"))
                    return;

                _metaItemTypeId = value;
                MarkDirty("MetaItemTypeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaFieldId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MetaBagId => FetchColumn("MetaBagId");

            public static QueryColumn ItemId => FetchColumn("ItemId");

            public static QueryColumn MetaItemTypeId => FetchColumn("MetaItemTypeId");

            public static QueryColumn MetaFieldId => FetchColumn("MetaFieldId");
        }

        #endregion
    }
}