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
    ///     This is an DataRecord class which wraps the Portal.RouteType table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "RouteType", "RouteTypeId")]
    public class RouteType : DataRecord<RouteType>
    {
        private string _name;

        private int _routeTypeId;

        public RouteType() { }
        protected RouteType(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteTypeId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int RouteTypeId
        {
            get => _routeTypeId;
            set
            {
                if (value == _routeTypeId && IsPropertyDirty("RouteTypeId"))
                    return;

                _routeTypeId = value;
                MarkDirty("RouteTypeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 9)]
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
            public static QueryColumn RouteTypeId => FetchColumn("RouteTypeId");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}