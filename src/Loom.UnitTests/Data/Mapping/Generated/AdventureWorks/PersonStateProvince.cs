#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Sales;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Person
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Person.StateProvince table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "StateProvince", "StateProvinceID", ModifiedOnColumn = "ModifiedDate")]
    public class StateProvince : DataRecord<StateProvince>
    {
        private string _countryRegionCode;
        private bool _isOnlyStateProvinceFlag;
        private DateTime _modifiedDate;
        private string _name;
        private Guid _rowguid;
        private string _stateProvinceCode;

        private int _stateProvinceId;
        private int _territoryId;

        public StateProvince() { }
        protected StateProvince(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for StateProvince records.
        /// </summary>
        [ActiveColumn("StateProvinceID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int StateProvinceId
        {
            get => _stateProvinceId;
            set
            {
                if (value == _stateProvinceId && IsPropertyDirty("StateProvinceID"))
                    return;

                _stateProvinceId = value;
                MarkDirty("StateProvinceID");
            }
        }

        /// <summary>
        ///     ISO standard state or province code.
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
        ///     ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode.
        /// </summary>
        [ActiveColumn("CountryRegionCode", DbType.String, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 3)]
        [ForeignColumn("CountryRegionCode", typeof(CountryRegion), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
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
        ///     0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.
        /// </summary>
        [ActiveColumn("IsOnlyStateProvinceFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((1))")]
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
        ///     State or province description.
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
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
        ///     ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID.
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("TerritoryID", typeof(SalesTerritory), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(newid())")]
        public Guid Rowguid
        {
            get => _rowguid;
            set
            {
                if (value == _rowguid && IsPropertyDirty("rowguid"))
                    return;

                _rowguid = value;
                MarkDirty("rowguid");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                if (value == _modifiedDate && IsPropertyDirty("ModifiedDate"))
                    return;

                _modifiedDate = value;
                MarkDirty("ModifiedDate");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn StateProvinceId => FetchColumn("StateProvinceID");

            public static QueryColumn StateProvinceCode => FetchColumn("StateProvinceCode");

            public static QueryColumn CountryRegionCode => FetchColumn("CountryRegionCode");

            public static QueryColumn IsOnlyStateProvinceFlag => FetchColumn("IsOnlyStateProvinceFlag");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}