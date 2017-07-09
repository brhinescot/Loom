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
    ///     This is an DataRecord class which wraps the Production.VesaPattern table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "VesaPattern", "VesaPatternId")]
    public class VesaPattern : DataRecord<VesaPattern>
    {
        private string _friendly;
        private short _height;

        private int _vesaPatternId;
        private short _width;

        public VesaPattern() { }
        protected VesaPattern(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("VesaPatternId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int VesaPatternId
        {
            get => _vesaPatternId;
            set
            {
                if (value == _vesaPatternId && IsPropertyDirty("VesaPatternId"))
                    return;

                _vesaPatternId = value;
                MarkDirty("VesaPatternId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Height", DbType.Int16, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
        public short Height
        {
            get => _height;
            set
            {
                if (value == _height && IsPropertyDirty("Height"))
                    return;

                _height = value;
                MarkDirty("Height");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Width", DbType.Int16, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public short Width
        {
            get => _width;
            set
            {
                if (value == _width && IsPropertyDirty("Width"))
                    return;

                _width = value;
                MarkDirty("Width");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Friendly", DbType.String, ColumnProperties.Computed | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 7)]
        public string Friendly
        {
            get => _friendly;
            set
            {
                if (value == _friendly && IsPropertyDirty("Friendly"))
                    return;

                _friendly = value;
                MarkDirty("Friendly");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn VesaPatternId => FetchColumn("VesaPatternId");

            public static QueryColumn Height => FetchColumn("Height");

            public static QueryColumn Width => FetchColumn("Width");

            public static QueryColumn Friendly => FetchColumn("Friendly");
        }

        #endregion
    }
}