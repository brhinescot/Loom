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
    ///     This is an DataRecord class which wraps the Portal.MetaField table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "MetaField", "MetaFieldId")]
    public class MetaField : DataRecord<MetaField>
    {
        private string _dataType;
        private string _description;

        private int _metaFieldId;
        private string _name;
        private string _validation;

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
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 100)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description && IsPropertyDirty("Description"))
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DataType", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 11)]
        public string DataType
        {
            get => _dataType;
            set
            {
                if (value == _dataType && IsPropertyDirty("DataType"))
                    return;

                _dataType = value;
                MarkDirty("DataType");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Validation", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
        public string Validation
        {
            get => _validation;
            set
            {
                if (value == _validation && IsPropertyDirty("Validation"))
                    return;

                _validation = value;
                MarkDirty("Validation");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MetaFieldId => FetchColumn("MetaFieldId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn DataType => FetchColumn("DataType");

            public static QueryColumn Validation => FetchColumn("Validation");
        }

        #endregion
    }
}