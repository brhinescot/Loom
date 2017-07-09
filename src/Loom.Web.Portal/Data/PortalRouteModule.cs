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
    ///     This is an DataRecord class which wraps the Portal.RouteModule table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "RouteModule", "RouteModuleId")]
    public class RouteModule : DataRecord<RouteModule>
    {
        private string _containerName;
        private string _data;
        private int _moduleId;
        private string _name;
        private short? _ordinal;
        private int _routeId;

        private int _routeModuleId;
        private string _settings;

        public RouteModule() { }
        protected RouteModule(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteModuleId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int RouteModuleId
        {
            get => _routeModuleId;
            set
            {
                if (value == _routeModuleId && IsPropertyDirty("RouteModuleId"))
                    return;

                _routeModuleId = value;
                MarkDirty("RouteModuleId");
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

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("RouteId", typeof(Route), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int RouteId
        {
            get => _routeId;
            set
            {
                if (value == _routeId && IsPropertyDirty("RouteId"))
                    return;

                _routeId = value;
                MarkDirty("RouteId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModuleId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("ModuleId", typeof(Module), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ModuleId
        {
            get => _moduleId;
            set
            {
                if (value == _moduleId && IsPropertyDirty("ModuleId"))
                    return;

                _moduleId = value;
                MarkDirty("ModuleId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContainerName", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
        public string ContainerName
        {
            get => _containerName;
            set
            {
                if (value == _containerName && IsPropertyDirty("ContainerName"))
                    return;

                _containerName = value;
                MarkDirty("ContainerName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Settings", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 250)]
        public string Settings
        {
            get => _settings;
            set
            {
                if (value == _settings && IsPropertyDirty("Settings"))
                    return;

                _settings = value;
                MarkDirty("Settings");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Data", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 500)]
        public string Data
        {
            get => _data;
            set
            {
                if (value == _data && IsPropertyDirty("Data"))
                    return;

                _data = value;
                MarkDirty("Data");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int16, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public short? Ordinal
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn RouteModuleId => FetchColumn("RouteModuleId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn RouteId => FetchColumn("RouteId");

            public static QueryColumn ModuleId => FetchColumn("ModuleId");

            public static QueryColumn ContainerName => FetchColumn("ContainerName");

            public static QueryColumn Settings => FetchColumn("Settings");

            public static QueryColumn Data => FetchColumn("Data");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");
        }

        #endregion
    }
}