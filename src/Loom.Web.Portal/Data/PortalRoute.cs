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
    ///     This is an DataRecord class which wraps the Portal.Route table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Route", "RouteId", CreatedOnColumn = "CreatedOn", CreatedByColumn = "CreatedBy", ModifiedByColumn = "ModifiedBy", DeletedColumn = "Deleted")]
    public class Route : DataRecord<Route>
    {
        private string _controller;
        private int _createdBy;
        private DateTime _createdOn;
        private bool _deleted;
        private int? _deletedBy;
        private DateTime? _deletedOn;
        private string _expression;
        private bool _locked;
        private int _modifiedBy;
        private DateTime _modifiedOn;
        private string _name;
        private int _ordinal;
        private string _pageTitle;
        private bool _primary;

        private int _routeId;
        private string _section;
        private int _tenantId;

        public Route() { }
        protected Route(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Expression", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string Expression
        {
            get => _expression;
            set
            {
                if (value == _expression && IsPropertyDirty("Expression"))
                    return;

                _expression = value;
                MarkDirty("Expression");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PageTitle", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 60)]
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                if (value == _pageTitle && IsPropertyDirty("PageTitle"))
                    return;

                _pageTitle = value;
                MarkDirty("PageTitle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Primary", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((1))")]
        public bool Primary
        {
            get => _primary;
            set
            {
                if (value == _primary && IsPropertyDirty("Primary"))
                    return;

                _primary = value;
                MarkDirty("Primary");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Locked", DbType.Boolean, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((0))")]
        public bool Locked
        {
            get => _locked;
            set
            {
                if (value == _locked && IsPropertyDirty("Locked"))
                    return;

                _locked = value;
                MarkDirty("Locked");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Section", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 50)]
        public string Section
        {
            get => _section;
            set
            {
                if (value == _section && IsPropertyDirty("Section"))
                    return;

                _section = value;
                MarkDirty("Section");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Controller", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 50, DefaultValue = "(N'PortalController')")]
        public string Controller
        {
            get => _controller;
            set
            {
                if (value == _controller && IsPropertyDirty("Controller"))
                    return;

                _controller = value;
                MarkDirty("Controller");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        public int Ordinal
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

        /// <summary>
        /// </summary>
        [ActiveColumn("TenantId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 10, MaxLength = 0)]
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
        [ActiveColumn("CreatedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("CreatedBy", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 12, MaxLength = 0)]
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
        [ActiveColumn("ModifiedOn", DbType.DateTime, ColumnProperties.None, Ordinal = 13, MaxLength = 0, DefaultValue = "(getdate())")]
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
        [ActiveColumn("ModifiedBy", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 14, MaxLength = 0)]
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
        [ActiveColumn("DeletedOn", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 15, MaxLength = 0)]
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
        [ActiveColumn("DeletedBy", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 16, MaxLength = 0)]
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
        [ActiveColumn("Deleted", DbType.Boolean, ColumnProperties.None, Ordinal = 17, MaxLength = 0, DefaultValue = "((0))")]
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
            public static QueryColumn RouteId => FetchColumn("RouteId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Expression => FetchColumn("Expression");

            public static QueryColumn PageTitle => FetchColumn("PageTitle");

            public static QueryColumn Primary => FetchColumn("Primary");

            public static QueryColumn Locked => FetchColumn("Locked");

            public static QueryColumn Section => FetchColumn("Section");

            public static QueryColumn Controller => FetchColumn("Controller");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn TenantId => FetchColumn("TenantId");

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