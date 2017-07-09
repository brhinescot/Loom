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

namespace OmniMount
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.DatabaseVersion table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "DatabaseVersion", "Id")]
    public class DatabaseVersion : DataRecord<DatabaseVersion>
    {
        private int _id;
        private DateTime _modifiedDate;
        private int _version;
        private DateTime _versionDate;

        public DatabaseVersion() { }
        protected DatabaseVersion(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Id", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int Id
        {
            get => _id;
            set
            {
                if (value == _id && IsPropertyDirty("Id"))
                    return;

                _id = value;
                MarkDirty("Id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Version", DbType.Int32, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
        public int Version
        {
            get => _version;
            set
            {
                if (value == _version && IsPropertyDirty("Version"))
                    return;

                _version = value;
                MarkDirty("Version");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("VersionDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime VersionDate
        {
            get => _versionDate;
            set
            {
                if (value == _versionDate && IsPropertyDirty("VersionDate"))
                    return;

                _versionDate = value;
                MarkDirty("VersionDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                if (value == _modifiedDate && IsPropertyDirty("ModifiedDate"))
                    return;

                _modifiedDate = value;
                MarkDirty("ModifiedDate");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Id => FetchColumn("Id");

            public static QueryColumn Version => FetchColumn("Version");

            public static QueryColumn VersionDate => FetchColumn("VersionDate");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}