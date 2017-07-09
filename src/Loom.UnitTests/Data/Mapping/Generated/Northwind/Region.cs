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
    ///     This is an DataRecord class which wraps the dbo.Region table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Region", "RegionID")]
    public class Region : DataRecord<Region>
    {
        private string _regionDescription;

        private int _regionId;

        public Region() { }
        protected Region(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("RegionID", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("RegionDescription", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string RegionDescription
        {
            get => _regionDescription;
            set
            {
                if (value == _regionDescription)
                    return;

                _regionDescription = value;
                MarkDirty("RegionDescription");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn RegionId => FetchColumn("RegionID");

            public static QueryColumn RegionDescription => FetchColumn("RegionDescription");
        }

        #endregion
    }
}