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
    ///     This is an DataRecord class which wraps the Portal.PageDefinition table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "PageDefinition", "PageDefinitionId")]
    public class PageDefinition : DataRecord<PageDefinition>
    {
        private int _moduleDefinitionId;
        private string _moduleTitle;
        private int _pageColumnId;

        private int _pageDefinitionId;
        private string _panelName;
        private string _properties;
        private bool _remove;
        private string _resourceKey;
        private int _routeId;
        private int _sortOrder;

        public PageDefinition() { }
        protected PageDefinition(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("PageDefinitionId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int PageDefinitionId
        {
            get => _pageDefinitionId;
            set
            {
                if (value == _pageDefinitionId && IsPropertyDirty("PageDefinitionId"))
                    return;

                _pageDefinitionId = value;
                MarkDirty("PageDefinitionId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RouteId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0, DefaultValue = "((1))")]
        [ForeignColumn("RouteId", typeof(Route), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        [ActiveColumn("ModuleDefinitionId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ModuleDefinitionId", typeof(ModuleDefinition), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        [ActiveColumn("SortOrder", DbType.Int32, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public int SortOrder
        {
            get => _sortOrder;
            set
            {
                if (value == _sortOrder && IsPropertyDirty("SortOrder"))
                    return;

                _sortOrder = value;
                MarkDirty("SortOrder");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PageColumnId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("PageColumId", typeof(PageColumn), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int PageColumnId
        {
            get => _pageColumnId;
            set
            {
                if (value == _pageColumnId && IsPropertyDirty("PageColumnId"))
                    return;

                _pageColumnId = value;
                MarkDirty("PageColumnId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModuleTitle", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 50)]
        public string ModuleTitle
        {
            get => _moduleTitle;
            set
            {
                if (value == _moduleTitle && IsPropertyDirty("ModuleTitle"))
                    return;

                _moduleTitle = value;
                MarkDirty("ModuleTitle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Properties", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 100)]
// ReSharper disable once UnusedMember.Global
        public new string Properties
        {
            get => _properties;
            set
            {
                if (value == _properties && IsPropertyDirty("Properties"))
                    return;

                _properties = value;
                MarkDirty("Properties");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ResourceKey", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 30)]
        public string ResourceKey
        {
            get => _resourceKey;
            set
            {
                if (value == _resourceKey && IsPropertyDirty("ResourceKey"))
                    return;

                _resourceKey = value;
                MarkDirty("ResourceKey");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PanelName", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 15, DefaultValue = "('')")]
        public string PanelName
        {
            get => _panelName;
            set
            {
                if (value == _panelName && IsPropertyDirty("PanelName"))
                    return;

                _panelName = value;
                MarkDirty("PanelName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Remove", DbType.Boolean, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "((0))")]
        public bool Remove
        {
            get => _remove;
            set
            {
                if (value == _remove && IsPropertyDirty("Remove"))
                    return;

                _remove = value;
                MarkDirty("Remove");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn PageDefinitionId => FetchColumn("PageDefinitionId");

            public static QueryColumn RouteId => FetchColumn("RouteId");

            public static QueryColumn ModuleDefinitionId => FetchColumn("ModuleDefinitionId");

            public static QueryColumn SortOrder => FetchColumn("SortOrder");

            public static QueryColumn PageColumnId => FetchColumn("PageColumnId");

            public static QueryColumn ModuleTitle => FetchColumn("ModuleTitle");

            public static QueryColumn Properties => FetchColumn("Properties");

            public static QueryColumn ResourceKey => FetchColumn("ResourceKey");

            public static QueryColumn PanelName => FetchColumn("PanelName");

            public static QueryColumn Remove => FetchColumn("Remove");
        }

        #endregion
    }
}