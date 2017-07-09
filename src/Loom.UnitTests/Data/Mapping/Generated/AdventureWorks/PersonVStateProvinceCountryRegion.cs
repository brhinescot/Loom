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

namespace AdventureWorks.Person
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Person.vStateProvinceCountryRegion table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "vStateProvinceCountryRegion", ReadOnly = true)]
    public class VStateProvinceCountryRegion : DataRecord<VStateProvinceCountryRegion>
    {
        private string _countryRegionCode;
        private string _countryRegionName;

        private bool _isOnlyStateProvinceFlag;
        private string _stateProvinceCode;
        private string _stateProvinceName;
        private int _territoryId;

        public VStateProvinceCountryRegion() { }
        protected VStateProvinceCountryRegion(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("IsOnlyStateProvinceFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public bool IsOnlyStateProvinceFlag
        {
            get => _isOnlyStateProvinceFlag;
            set
            {
                if (value == _isOnlyStateProvinceFlag && IsPropertyDirty("IsOnlyStateProvinceFlag"))
                    return;

                _isOnlyStateProvinceFlag = value;
                MarkDirty("IsOnlyStateProvinceFlag");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CountryRegionCode", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 3)]
        public string CountryRegionCode
        {
            get => _countryRegionCode;
            set
            {
                if (value == _countryRegionCode && IsPropertyDirty("CountryRegionCode"))
                    return;

                _countryRegionCode = value;
                MarkDirty("CountryRegionCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CountryRegionName", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 50)]
        public string CountryRegionName
        {
            get => _countryRegionName;
            set
            {
                if (value == _countryRegionName && IsPropertyDirty("CountryRegionName"))
                    return;

                _countryRegionName = value;
                MarkDirty("CountryRegionName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StateProvinceName", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
        public string StateProvinceName
        {
            get => _stateProvinceName;
            set
            {
                if (value == _stateProvinceName && IsPropertyDirty("StateProvinceName"))
                    return;

                _stateProvinceName = value;
                MarkDirty("StateProvinceName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StateProvinceCode", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 3)]
        public string StateProvinceCode
        {
            get => _stateProvinceCode;
            set
            {
                if (value == _stateProvinceCode && IsPropertyDirty("StateProvinceCode"))
                    return;

                _stateProvinceCode = value;
                MarkDirty("StateProvinceCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.Int32, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public int TerritoryId
        {
            get => _territoryId;
            set
            {
                if (value == _territoryId && IsPropertyDirty("TerritoryID"))
                    return;

                _territoryId = value;
                MarkDirty("TerritoryID");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn IsOnlyStateProvinceFlag => FetchColumn("IsOnlyStateProvinceFlag");

            public static QueryColumn CountryRegionCode => FetchColumn("CountryRegionCode");

            public static QueryColumn CountryRegionName => FetchColumn("CountryRegionName");

            public static QueryColumn StateProvinceName => FetchColumn("StateProvinceName");

            public static QueryColumn StateProvinceCode => FetchColumn("StateProvinceCode");

            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");
        }

        #endregion
    }
}