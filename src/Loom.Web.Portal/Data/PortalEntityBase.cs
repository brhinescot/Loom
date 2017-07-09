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
    ///     This is an DataRecord class which wraps the Portal.EntityBase table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "EntityBase", "EntityId", CreatedOnColumn = "CreatedOn", ModifiedByColumn = "ModifiedBy", DeletedColumn = "Deleted")]
    public class EntityBase : DataRecord<EntityBase>
    {
        private int? _createBy;
        private DateTime _createdOn;
        private bool _deleted;
        private int? _deletedBy;
        private DateTime? _deletedOn;
        private string _displayName;

        private int _entityId;
        private decimal? _latitude;
        private decimal? _longitude;
        private int? _modifiedBy;
        private DateTime _modifiedOn;
        private int _tenantId;

        public EntityBase() { }
        protected EntityBase(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("EntityId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int EntityId
        {
            get => _entityId;
            set
            {
                if (value == _entityId && IsPropertyDirty("EntityId"))
                    return;

                _entityId = value;
                MarkDirty("EntityId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TenantId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("TenantId", typeof(Tenant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int TenantId
        {
            get => _tenantId;
            set
            {
                if (value == _tenantId && IsPropertyDirty("TenantId"))
                    return;

                _tenantId = value;
                MarkDirty("TenantId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("DisplayName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 30)]
        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (value == _displayName && IsPropertyDirty("DisplayName"))
                    return;

                _displayName = value;
                MarkDirty("DisplayName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Latitude", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public decimal? Latitude
        {
            get => _latitude;
            set
            {
                if (value == _latitude && IsPropertyDirty("Latitude"))
                    return;

                _latitude = value;
                MarkDirty("Latitude");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Longitude", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public decimal? Longitude
        {
            get => _longitude;
            set
            {
                if (value == _longitude && IsPropertyDirty("Longitude"))
                    return;

                _longitude = value;
                MarkDirty("Longitude");
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
        [ActiveColumn("CreateBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? CreateBy
        {
            get => _createBy;
            set
            {
                if (value == _createBy && IsPropertyDirty("CreateBy"))
                    return;

                _createBy = value;
                MarkDirty("CreateBy");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("ModifiedBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        [ForeignColumn("UserId", typeof(User), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ModifiedBy
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
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("DeletedBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 11, MaxLength = 0)]
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
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 12, MaxLength = 0, DefaultValue = "((0))")]
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
            public static QueryColumn EntityId => FetchColumn("EntityId");

            public static QueryColumn TenantId => FetchColumn("TenantId");

            public static QueryColumn DisplayName => FetchColumn("DisplayName");

            public static QueryColumn Latitude => FetchColumn("Latitude");

            public static QueryColumn Longitude => FetchColumn("Longitude");

            public static QueryColumn CreatedOn => FetchColumn("CreatedOn");

            public static QueryColumn CreateBy => FetchColumn("CreateBy");

            public static QueryColumn ModifiedOn => FetchColumn("ModifiedOn");

            public static QueryColumn ModifiedBy => FetchColumn("ModifiedBy");

            public static QueryColumn DeletedOn => FetchColumn("DeletedOn");

            public static QueryColumn DeletedBy => FetchColumn("DeletedBy");

            public static QueryColumn Deleted => FetchColumn("Deleted");
        }

        #endregion
    }
}