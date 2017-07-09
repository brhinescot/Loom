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
    ///     This is an DataRecord class which wraps the Portal.Module table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Module", "ModuleId")]
    public class Module : DataRecord<Module>
    {
        private int _moduleId;
        private string _name;
        private int? _networkId;
        private string _path;

        public Module() { }
        protected Module(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModuleId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Path", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 150)]
        public string Path
        {
            get => _path;
            set
            {
                if (value == _path && IsPropertyDirty("Path"))
                    return;

                _path = value;
                MarkDirty("Path");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NetworkId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("NetworkId", typeof(Network), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? NetworkId
        {
            get => _networkId;
            set
            {
                if (value == _networkId && IsPropertyDirty("NetworkId"))
                    return;

                _networkId = value;
                MarkDirty("NetworkId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ModuleId => FetchColumn("ModuleId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Path => FetchColumn("Path");

            public static QueryColumn NetworkId => FetchColumn("NetworkId");
        }

        #endregion
    }
}