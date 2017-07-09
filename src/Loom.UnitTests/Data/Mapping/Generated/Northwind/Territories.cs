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

namespace Northwind
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.Territories table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Territories", "TerritoryID")]
    public class Territories : DataRecord<Territories>
    {
        private int _regionId;
        private string _territoryDescription;

        private string _territoryId;

        public Territories() { }
        protected Territories(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 20)]
        public string TerritoryId
        {
            get => _territoryId;
            set
            {
                if (value == _territoryId)
                    return;

                _territoryId = value;
                MarkDirty("TerritoryID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TerritoryDescription", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string TerritoryDescription
        {
            get => _territoryDescription;
            set
            {
                if (value == _territoryDescription)
                    return;

                _territoryDescription = value;
                MarkDirty("TerritoryDescription");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("RegionID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("RegionID", typeof(Region), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int RegionId
        {
            get => _regionId;
            set
            {
                if (value == _regionId)
                    return;

                _regionId = value;
                MarkDirty("RegionID");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");

            public static QueryColumn TerritoryDescription => FetchColumn("TerritoryDescription");

            public static QueryColumn RegionId => FetchColumn("RegionID");
        }

        #endregion
    }
}