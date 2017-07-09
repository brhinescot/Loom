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
    ///     This is an DataRecord class which wraps the Sales.SalesOrderHeaderSalesReason table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesOrderHeaderSalesReason", "SalesReasonID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesOrderHeaderSalesReason : DataRecord<SalesOrderHeaderSalesReason>
    {
        private DateTime _modifiedDate;

        private int _salesOrderId;
        private int _salesReasonId;

        public SalesOrderHeaderSalesReason() { }
        protected SalesOrderHeaderSalesReason(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to SalesOrderHeader.SalesOrderID.
        /// </summary>
        [ActiveColumn("SalesOrderID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("SalesOrderID", typeof(SalesOrderHeader), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int SalesOrderId
        {
            get => _salesOrderId;
            set
            {
                if (value == _salesOrderId && IsPropertyDirty("SalesOrderID"))
                    return;

                _salesOrderId = value;
                MarkDirty("SalesOrderID");
            }
        }

        /// <summary>
        ///     Primary key. Foreign key to SalesReason.SalesReasonID.
        /// </summary>
        [ActiveColumn("SalesReasonID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("SalesReasonID", typeof(SalesReason), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int SalesReasonId
        {
            get => _salesReasonId;
            set
            {
                if (value == _salesReasonId && IsPropertyDirty("SalesReasonID"))
                    return;

                _salesReasonId = value;
                MarkDirty("SalesReasonID");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn SalesOrderId => FetchColumn("SalesOrderID");

            public static QueryColumn SalesReasonId => FetchColumn("SalesReasonID");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}