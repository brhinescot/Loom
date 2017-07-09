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
    ///     This is an DataRecord class which wraps the Portal.ModuleDefinition table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "ModuleDefinition", "ModuleDefinitionId", DeletedColumn = "Deleted")]
    public class ModuleDefinition : DataRecord<ModuleDefinition>
    {
        private int _applicationId;
        private bool _deleted;
        private string _friendlyName;

        private int _moduleDefinitionId;
        private string _path;
        private string _typeName;

        public ModuleDefinition() { }
        protected ModuleDefinition(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModuleDefinitionId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ModuleDefinitionId
        {
            get => _moduleDefinitionId;
            set
            {
                if (value == _moduleDefinitionId && IsPropertyDirty("ModuleDefinitionId"))
                    return;

                _moduleDefinitionId = value;
                MarkDirty("ModuleDefinitionId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FriendlyName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string FriendlyName
        {
            get => _friendlyName;
            set
            {
                if (value == _friendlyName && IsPropertyDirty("FriendlyName"))
                    return;

                _friendlyName = value;
                MarkDirty("FriendlyName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TypeName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 70)]
        public string TypeName
        {
            get => _typeName;
            set
            {
                if (value == _typeName && IsPropertyDirty("TypeName"))
                    return;

                _typeName = value;
                MarkDirty("TypeName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Path", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
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
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public bool Deleted
        {
            get => _deleted;
            set
            {
                if (value == _deleted && IsPropertyDirty("Deleted"))
                    return;

                _deleted = value;
                MarkDirty("Deleted");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0, DefaultValue = "((1))")]
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
            public static QueryColumn ModuleDefinitionId => FetchColumn("ModuleDefinitionId");

            public static QueryColumn FriendlyName => FetchColumn("FriendlyName");

            public static QueryColumn TypeName => FetchColumn("TypeName");

            public static QueryColumn Path => FetchColumn("Path");

            public static QueryColumn Deleted => FetchColumn("Deleted");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}