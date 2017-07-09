#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Person;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.SalesTerritory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesTerritory", "TerritoryID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesTerritory : DataRecord<SalesTerritory>
    {
        private decimal _costLastYear;
        private decimal _costYTD;
        private string _countryRegionCode;
        private string _group;
        private DateTime _modifiedDate;
        private string _name;
        private Guid _rowguid;
        private decimal _salesLastYear;
        private decimal _salesYTD;

        private int _territoryId;

        public SalesTerritory() { }
        protected SalesTerritory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for SalesTerritory records.
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Sales territory description
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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
        ///     Geographic area to which the sales territory belong.
        /// </summary>
        [ActiveColumn("Group", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
        public string Group
        {
            get => _group;
            set
            {
                if (value == _group && IsPropertyDirty("Group"))
                    return;

                _group = value;
                MarkDirty("Group");
            }
        }

        /// <summary>
        ///     Sales in the territory year to date.
        /// </summary>
        [ActiveColumn("SalesYTD", DbType.Currency, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal SalesYTD
        {
            get => _salesYTD;
            set
            {
                if (value == _salesYTD && IsPropertyDirty("SalesYTD"))
                    return;

                _salesYTD = value;
                MarkDirty("SalesYTD");
            }
        }

        /// <summary>
        ///     Sales in the territory the previous year.
        /// </summary>
        [ActiveColumn("SalesLastYear", DbType.Currency, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal SalesLastYear
        {
            get => _salesLastYear;
            set
            {
                if (value == _salesLastYear && IsPropertyDirty("SalesLastYear"))
                    return;

                _salesLastYear = value;
                MarkDirty("SalesLastYear");
            }
        }

        /// <summary>
        ///     Business costs in the territory year to date.
        /// </summary>
        [ActiveColumn("CostYTD", DbType.Currency, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal CostYTD
        {
            get => _costYTD;
            set
            {
                if (value == _costYTD && IsPropertyDirty("CostYTD"))
                    return;

                _costYTD = value;
                MarkDirty("CostYTD");
            }
        }

        /// <summary>
        ///     Business costs in the territory the previous year.
        /// </summary>
        [ActiveColumn("CostLastYear", DbType.Currency, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal CostLastYear
        {
            get => _costLastYear;
            set
            {
                if (value == _costLastYear && IsPropertyDirty("CostLastYear"))
                    return;

                _costLastYear = value;
                MarkDirty("CostLastYear");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn CountryRegionCode => FetchColumn("CountryRegionCode");

            public static QueryColumn Group => FetchColumn("Group");

            public static QueryColumn SalesYTD => FetchColumn("SalesYTD");

            public static QueryColumn SalesLastYear => FetchColumn("SalesLastYear");

            public static QueryColumn CostYTD => FetchColumn("CostYTD");

            public static QueryColumn CostLastYear => FetchColumn("CostLastYear");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}