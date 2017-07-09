#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.HumanResources;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.SalesPerson table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesPerson", "BusinessEntityID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesPerson : DataRecord<SalesPerson>
    {
        private decimal _bonus;

        private int _businessEntityId;
        private decimal _commissionPct;
        private DateTime _modifiedDate;
        private Guid _rowguid;
        private decimal _salesLastYear;
        private decimal? _salesQuota;
        private decimal _salesYTD;
        private int? _territoryId;

        public SalesPerson() { }
        protected SalesPerson(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Employee), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        ///     Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.
        /// </summary>
        [ActiveColumn("TerritoryID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("TerritoryID", typeof(SalesTerritory), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? TerritoryId
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
        ///     Projected yearly sales.
        /// </summary>
        [ActiveColumn("SalesQuota", DbType.Currency, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public decimal? SalesQuota
        {
            get => _salesQuota;
            set
            {
                if (value == _salesQuota && IsPropertyDirty("SalesQuota"))
                    return;

                _salesQuota = value;
                MarkDirty("SalesQuota");
            }
        }

        /// <summary>
        ///     Bonus due if quota is met.
        /// </summary>
        [ActiveColumn("Bonus", DbType.Currency, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal Bonus
        {
            get => _bonus;
            set
            {
                if (value == _bonus && IsPropertyDirty("Bonus"))
                    return;

                _bonus = value;
                MarkDirty("Bonus");
            }
        }

        /// <summary>
        ///     Commision percent received per sale.
        /// </summary>
        [ActiveColumn("CommissionPct", DbType.Currency, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal CommissionPct
        {
            get => _commissionPct;
            set
            {
                if (value == _commissionPct && IsPropertyDirty("CommissionPct"))
                    return;

                _commissionPct = value;
                MarkDirty("CommissionPct");
            }
        }

        /// <summary>
        ///     Sales total year to date.
        /// </summary>
        [ActiveColumn("SalesYTD", DbType.Currency, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((0.00))")]
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
        ///     Sales total of previous year.
        /// </summary>
        [ActiveColumn("SalesLastYear", DbType.Currency, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0.00))")]
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
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn TerritoryId => FetchColumn("TerritoryID");

            public static QueryColumn SalesQuota => FetchColumn("SalesQuota");

            public static QueryColumn Bonus => FetchColumn("Bonus");

            public static QueryColumn CommissionPct => FetchColumn("CommissionPct");

            public static QueryColumn SalesYTD => FetchColumn("SalesYTD");

            public static QueryColumn SalesLastYear => FetchColumn("SalesLastYear");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}