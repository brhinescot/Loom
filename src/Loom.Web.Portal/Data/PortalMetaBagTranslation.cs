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
    ///     This is an DataRecord class which wraps the Portal.MetaBagTranslation table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "MetaBagTranslation", "MetaBagId")]
    public class MetaBagTranslation : DataRecord<MetaBagTranslation>
    {
        private string _locale;

        private int _metaBagId;
        private string _value;

        public MetaBagTranslation() { }
        protected MetaBagTranslation(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MetaBagId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Value", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 4000)]
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

            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn Value => FetchColumn("Value");
        }

        #endregion
    }
}