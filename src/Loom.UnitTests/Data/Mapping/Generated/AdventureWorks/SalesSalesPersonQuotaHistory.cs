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

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.SalesPersonQuotaHistory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesPersonQuotaHistory", "QuotaDate", ModifiedOnColumn = "ModifiedDate")]
    public class SalesPersonQuotaHistory : DataRecord<SalesPersonQuotaHistory>
    {
        private int _businessEntityId;
        private DateTime _modifiedDate;
        private DateTime _quotaDate;
        private Guid _rowguid;
        private decimal _salesQuota;

        public SalesPersonQuotaHistory() { }
        protected SalesPersonQuotaHistory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(SalesPerson), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Sales quota date.
        /// </summary>
        [ActiveColumn("QuotaDate", DbType.DateTime, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        public DateTime QuotaDate
        {
            get => _quotaDate;
            set
            {
                if (value == _quotaDate && IsPropertyDirty("QuotaDate"))
                    return;

                _quotaDate = value;
                MarkDirty("QuotaDate");
            }
        }

        /// <summary>
        ///     Sales quota amount.
        /// </summary>
        [ActiveColumn("SalesQuota", DbType.Currency, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public decimal SalesQuota
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
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn QuotaDate => FetchColumn("QuotaDate");

            public static QueryColumn SalesQuota => FetchColumn("SalesQuota");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}