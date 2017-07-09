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
using OmniMount.Production;

#endregion

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.ManufacturerProductVesaPattern table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "ManufacturerProductVesaPattern", "VesaPatternId")]
    public class ManufacturerProductVesaPattern : DataRecord<ManufacturerProductVesaPattern>
    {
        private int _manufacturerProductId;
        private int _vesaPatternId;

        public ManufacturerProductVesaPattern() { }
        protected ManufacturerProductVesaPattern(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ManufacturerProductId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ManufacturerProductId", typeof(ManufacturerProduct), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ManufacturerProductId
        {
            get => _manufacturerProductId;
            set
            {
                if (value == _manufacturerProductId && IsPropertyDirty("ManufacturerProductId"))
                    return;

                _manufacturerProductId = value;
                MarkDirty("ManufacturerProductId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("VesaPatternId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("VesaPatternId", typeof(VesaPattern), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ManufacturerProductId => FetchColumn("ManufacturerProductId");

            public static QueryColumn VesaPatternId => FetchColumn("VesaPatternId");
        }

        #endregion
    }
}