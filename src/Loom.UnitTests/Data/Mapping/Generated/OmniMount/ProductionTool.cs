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
    ///     This is an DataRecord class which wraps the Production.Tool table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Tool", "ToolId")]
    public class Tool : DataRecord<Tool>
    {
        private string _description;
        private string _imageUrl;
        private string _name;

        private int _toolId;

        public Tool() { }
        protected Tool(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ToolId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ToolId
        {
            get => _toolId;
            set
            {
                if (value == _toolId && IsPropertyDirty("ToolId"))
                    return;

                _toolId = value;
                MarkDirty("ToolId");
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
        [ActiveColumn("ImageUrl", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 20)]
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value == _imageUrl && IsPropertyDirty("ImageUrl"))
                    return;

                _imageUrl = value;
                MarkDirty("ImageUrl");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ToolId => FetchColumn("ToolId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn ImageUrl => FetchColumn("ImageUrl");
        }

        #endregion
    }
}