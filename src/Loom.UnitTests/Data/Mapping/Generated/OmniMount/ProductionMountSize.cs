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
    ///     This is an DataRecord class which wraps the Production.MountSize table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "MountSize", "MountSizeId")]
    public class MountSize : DataRecord<MountSize>
    {
        private string _description;
        private string _friendly;
        private decimal _maxSize;
        private decimal _minSize;

        private int _mountSizeId;
        private string _name;

        public MountSize() { }
        protected MountSize(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MountSizeId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int MountSizeId
        {
            get => _mountSizeId;
            set
            {
                if (value == _mountSizeId && IsPropertyDirty("MountSizeId"))
                    return;

                _mountSizeId = value;
                MarkDirty("MountSizeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 11)]
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
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 150)]
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
        [ActiveColumn("MinSize", DbType.Decimal, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public decimal MinSize
        {
            get => _minSize;
            set
            {
                if (value == _minSize && IsPropertyDirty("MinSize"))
                    return;

                _minSize = value;
                MarkDirty("MinSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MaxSize", DbType.Decimal, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public decimal MaxSize
        {
            get => _maxSize;
            set
            {
                if (value == _maxSize && IsPropertyDirty("MaxSize"))
                    return;

                _maxSize = value;
                MarkDirty("MaxSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Friendly", DbType.String, ColumnProperties.Computed | ColumnProperties.Nullable, Ordinal = 6, MaxLength = 78)]
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
            public static QueryColumn MountSizeId => FetchColumn("MountSizeId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn MinSize => FetchColumn("MinSize");

            public static QueryColumn MaxSize => FetchColumn("MaxSize");

            public static QueryColumn Friendly => FetchColumn("Friendly");
        }

        #endregion
    }
}