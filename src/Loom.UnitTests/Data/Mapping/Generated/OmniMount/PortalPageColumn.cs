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
    ///     This is an DataRecord class which wraps the Portal.PageColumn table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "PageColumn", "PageColumId")]
    public class PageColumn : DataRecord<PageColumn>
    {
        private string _name;

        private int _pageColumId;

        public PageColumn() { }
        protected PageColumn(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("PageColumId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int PageColumId
        {
            get => _pageColumId;
            set
            {
                if (value == _pageColumId && IsPropertyDirty("PageColumId"))
                    return;

                _pageColumId = value;
                MarkDirty("PageColumId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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
            public static QueryColumn PageColumId => FetchColumn("PageColumId");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}