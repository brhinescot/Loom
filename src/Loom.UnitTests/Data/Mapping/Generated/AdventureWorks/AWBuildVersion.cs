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

namespace AdventureWorks
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.AWBuildVersion table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "AWBuildVersion", "SystemInformationID", ModifiedOnColumn = "ModifiedDate")]
    public class AWBuildVersion : DataRecord<AWBuildVersion>
    {
        private string _databaseVersion;
        private DateTime _modifiedDate;

        private short _systemInformationId;
        private DateTime _versionDate;

        public AWBuildVersion() { }
        protected AWBuildVersion(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for AWBuildVersion records.
        /// </summary>
        [ActiveColumn("SystemInformationID", DbType.Int16, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public short SystemInformationId
        {
            get => _systemInformationId;
            set
            {
                if (value == _systemInformationId && IsPropertyDirty("SystemInformationID"))
                    return;

                _systemInformationId = value;
                MarkDirty("SystemInformationID");
            }
        }

        /// <summary>
        ///     Version number of the database in 9.yy.mm.dd.00 format.
        /// </summary>
        [ActiveColumn("Database Version", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 25)]
        public string DatabaseVersion
        {
            get => _databaseVersion;
            set
            {
                if (value == _databaseVersion && IsPropertyDirty("Database Version"))
                    return;

                _databaseVersion = value;
                MarkDirty("Database Version");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("VersionDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
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
        ///     Date and time the record was last updated.
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
            public static QueryColumn SystemInformationId => FetchColumn("SystemInformationID");

            public static QueryColumn DatabaseVersion => FetchColumn("Database Version");

            public static QueryColumn VersionDate => FetchColumn("VersionDate");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}