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

namespace OmniMount.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.Attachment table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Attachment", "AttachmentId", CreatedOnColumn = "CreatedOn", ModifiedOnColumn = "ModifiedOn", CreatedByColumn = "CreatedBy", ModifiedByColumn = "ModifiedBy")]
    public class Attachment : DataRecord<Attachment>
    {
        private int _attachmentId;
        private string _createdBy;
        private DateTime _createdOn;
        private string _deletedBy;
        private DateTime? _deletedOn;
        private string _description;
        private string _mimeType;
        private string _modifiedBy;
        private DateTime _modifiedOn;
        private string _name;
        private string _path;

        public Attachment() { }
        protected Attachment(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("AttachmentId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int AttachmentId
        {
            get => _attachmentId;
            set
            {
                if (value == _attachmentId && IsPropertyDirty("AttachmentId"))
                    return;

                _attachmentId = value;
                MarkDirty("AttachmentId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 30)]
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
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 400)]
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
        [ActiveColumn("Path", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 200)]
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
        [ActiveColumn("MimeType", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 40)]
        public string MimeType
        {
            get => _mimeType;
            set
            {
                if (value == _mimeType && IsPropertyDirty("MimeType"))
                    return;

                _mimeType = value;
                MarkDirty("MimeType");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("ModifiedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedOn
        {
            get => _modifiedOn;
            set
            {
                if (value == _modifiedOn && IsPropertyDirty("ModifiedOn"))
                    return;

                _modifiedOn = value;
                MarkDirty("ModifiedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedBy", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 30)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedBy", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 30)]
        public string ModifiedBy
        {
            get => _modifiedBy;
            set
            {
                if (value == _modifiedBy && IsPropertyDirty("ModifiedBy"))
                    return;

                _modifiedBy = value;
                MarkDirty("ModifiedBy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 0)]
        public DateTime? DeletedOn
        {
            get => _deletedOn;
            set
            {
                if (value == _deletedOn && IsPropertyDirty("DeletedOn"))
                    return;

                _deletedOn = value;
                MarkDirty("DeletedOn");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DeletedBy", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 30)]
        public string DeletedBy
        {
            get => _deletedBy;
            set
            {
                if (value == _deletedBy && IsPropertyDirty("DeletedBy"))
                    return;

                _deletedBy = value;
                MarkDirty("DeletedBy");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn AttachmentId => FetchColumn("AttachmentId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn Path => FetchColumn("Path");

            public static QueryColumn MimeType => FetchColumn("MimeType");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn ModifiedOn => FetchColumn("ModifiedOn");

            public static QueryColumn CreatedBy => FetchColumn("CreatedBy");

            public static QueryColumn ModifiedBy => FetchColumn("ModifiedBy");

            public static QueryColumn DeletedOn => FetchColumn("DeletedOn");

            public static QueryColumn DeletedBy => FetchColumn("DeletedBy");
        }

        #endregion
    }
}