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
    ///     This is an DataRecord class which wraps the Portal.Route table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Route", "RouteId")]
    public class Route : DataRecord<Route>
    {
        private int? _applicationId;
        private string _expression;
        private string _name;
        private string _pageTitle;

        private int _routeId;
        private int _routeTypeId;
        private bool _system;
        private string _template;

        public Route() { }
        protected Route(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        [ActiveColumn("Name", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 40)]
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
        [ActiveColumn("PageTitle", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
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
        [ActiveColumn("Template", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
        public string Template
        {
            get => _template;
            set
            {
                if (value == _template && IsPropertyDirty("Template"))
                    return;

                _template = value;
                MarkDirty("Template");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteTypeId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0, DefaultValue = "((0))")]
        [ForeignColumn("RouteTypeId", typeof(RouteType), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int RouteTypeId
        {
            get => _routeTypeId;
            set
            {
                if (value == _routeTypeId && IsPropertyDirty("RouteTypeId"))
                    return;

                _routeTypeId = value;
                MarkDirty("RouteTypeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("System", DbType.Boolean, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0))")]
        public bool System
        {
            get => _system;
            set
            {
                if (value == _system && IsPropertyDirty("System"))
                    return;

                _system = value;
                MarkDirty("System");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        [ForeignColumn("ApplicationId", typeof(Application), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ApplicationId
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
            public static QueryColumn RouteId => FetchColumn("RouteId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Expression => FetchColumn("Expression");

            public static QueryColumn PageTitle => FetchColumn("PageTitle");

            public static QueryColumn Template => FetchColumn("Template");

            public static QueryColumn RouteTypeId => FetchColumn("RouteTypeId");

            public static QueryColumn System => FetchColumn("System");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}