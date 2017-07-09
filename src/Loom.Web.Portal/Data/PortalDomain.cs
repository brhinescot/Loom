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
using Loom.Web.Portal.Data.Security;

#endregion

namespace Loom.Web.Portal.Data.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.Domain table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Domain", "DomainId", CreatedOnColumn = "CreatedOn", CreatedByColumn = "CreatedBy", ModifiedByColumn = "ModifiedBy", DeletedColumn = "Deleted")]
    public class Domain : DataRecord<Domain>
    {
        private int _applicationId;
        private int _createdBy;
        private DateTime _createdOn;
        private bool _deleted;
        private int? _deletedBy;
        private DateTime? _deletedOn;

        private int _domainId;
        private int _modifiedBy;
        private DateTime _modifiedOn;
        private string _name;

        public Domain() { }
        protected Domain(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("DomainId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int DomainId
        {
            get => _domainId;
            set
            {
                if (value == _domainId && IsPropertyDirty("DomainId"))
                    return;

                _domainId = value;
                MarkDirty("DomainId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 256)]
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
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("CreatedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("CreatedBy", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int CreatedBy
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
        [ActiveColumn("ModifiedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("ModifiedBy", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 7, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ModifiedBy
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
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
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
        [ActiveColumn("DeletedBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? DeletedBy
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "((0))")]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn DomainId => FetchColumn("DomainId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn CreatedBy => FetchColumn("CreatedBy");

            public static QueryColumn ModifiedOn => FetchColumn("ModifiedOn");

            public static QueryColumn ModifiedBy => FetchColumn("ModifiedBy");

            public static QueryColumn DeletedOn => FetchColumn("DeletedOn");

            public static QueryColumn DeletedBy => FetchColumn("DeletedBy");

            public static QueryColumn Deleted => FetchColumn("Deleted");
        }

        #endregion
    }
}