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
    ///     This is an DataRecord class which wraps the Portal.Menu table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Menu", "MenuId")]
    public class Menu : DataRecord<Menu>
    {
        private int _applicationId;

        private int _menuId;
        private string _navigateUrl;
        private int _ordinal;
        private int? _parentMenuId;
        private string _text;

        public Menu() { }
        protected Menu(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MenuId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int MenuId
        {
            get => _menuId;
            set
            {
                if (value == _menuId && IsPropertyDirty("MenuId"))
                    return;

                _menuId = value;
                MarkDirty("MenuId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Text", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 40)]
        public string Text
        {
            get => _text;
            set
            {
                if (value == _text && IsPropertyDirty("Text"))
                    return;

                _text = value;
                MarkDirty("Text");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NavigateUrl", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 200)]
        public string NavigateUrl
        {
            get => _navigateUrl;
            set
            {
                if (value == _navigateUrl && IsPropertyDirty("NavigateUrl"))
                    return;

                _navigateUrl = value;
                MarkDirty("NavigateUrl");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ParentMenuId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("MenuId", typeof(Menu), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ParentMenuId
        {
            get => _parentMenuId;
            set
            {
                if (value == _parentMenuId && IsPropertyDirty("ParentMenuId"))
                    return;

                _parentMenuId = value;
                MarkDirty("ParentMenuId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Ordinal", DbType.Int32, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
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
        [ActiveColumn("ApplicationId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0, DefaultValue = "((2))")]
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
            public static QueryColumn MenuId => FetchColumn("MenuId");

            public static QueryColumn Text => FetchColumn("Text");

            public static QueryColumn NavigateUrl => FetchColumn("NavigateUrl");

            public static QueryColumn ParentMenuId => FetchColumn("ParentMenuId");

            public static QueryColumn Ordinal => FetchColumn("Ordinal");

            public static QueryColumn ApplicationId => FetchColumn("ApplicationId");
        }

        #endregion
    }
}