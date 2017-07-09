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
    ///     This is an DataRecord class which wraps the Portal.Embed table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Embed", "EmbedId", CreatedOnColumn = "CreatedOn", CreatedByColumn = "CreatedBy")]
    public class Embed : DataRecord<Embed>
    {
        private string _code;
        private string _createdBy;
        private DateTime _createdOn;
        private string _description;

        private int _embedId;
        private string _name;

        public Embed() { }
        protected Embed(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmbedId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int EmbedId
        {
            get => _embedId;
            set
            {
                if (value == _embedId && IsPropertyDirty("EmbedId"))
                    return;

                _embedId = value;
                MarkDirty("EmbedId");
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
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 200)]
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
        [ActiveColumn("Code", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 800)]
        public string Code
        {
            get => _code;
            set
            {
                if (value == _code && IsPropertyDirty("Code"))
                    return;

                _code = value;
                MarkDirty("Code");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime CreatedOn
        {
            get => _createdOn;
            set
            {
                if (value == _createdOn && IsPropertyDirty("CreatedOn"))
                    return;

                _createdOn = value;
                MarkDirty("CreatedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedBy", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 30)]
        public string CreatedBy
        {
            get => _createdBy;
            set
            {
                if (value == _createdBy && IsPropertyDirty("CreatedBy"))
                    return;

                _createdBy = value;
                MarkDirty("CreatedBy");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn EmbedId => FetchColumn("EmbedId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn Code => FetchColumn("Code");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn CreatedBy => FetchColumn("CreatedBy");
        }

        #endregion
    }
}