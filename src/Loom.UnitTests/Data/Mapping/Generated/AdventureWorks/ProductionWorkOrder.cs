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

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.WorkOrder table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "WorkOrder", "WorkOrderID", ModifiedOnColumn = "ModifiedDate")]
    public class WorkOrder : DataRecord<WorkOrder>
    {
        private DateTime _dueDate;
        private DateTime? _endDate;
        private DateTime _modifiedDate;
        private int _orderQty;
        private int _productId;
        private short _scrappedQty;
        private ScrapReason _scrapReasonId;
        private DateTime _startDate;
        private int _stockedQty;

        private int _workOrderId;

        public WorkOrder() { }
        protected WorkOrder(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for WorkOrder records.
        /// </summary>
        [ActiveColumn("WorkOrderID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int WorkOrderId
        {
            get => _workOrderId;
            set
            {
                if (value == _workOrderId && IsPropertyDirty("WorkOrderID"))
                    return;

                _workOrderId = value;
                MarkDirty("WorkOrderID");
            }
        }

        /// <summary>
        ///     Product identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId && IsPropertyDirty("ProductID"))
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        ///     Product quantity to build.
        /// </summary>
        [ActiveColumn("OrderQty", DbType.Int32, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public int OrderQty
        {
            get => _orderQty;
            set
            {
                if (value == _orderQty && IsPropertyDirty("OrderQty"))
                    return;

                _orderQty = value;
                MarkDirty("OrderQty");
            }
        }

        /// <summary>
        ///     Quantity built and put in inventory.
        /// </summary>
        [ActiveColumn("StockedQty", DbType.Int32, ColumnProperties.Computed, Ordinal = 4, MaxLength = 0)]
        public int StockedQty
        {
            get => _stockedQty;
            set
            {
                if (value == _stockedQty && IsPropertyDirty("StockedQty"))
                    return;

                _stockedQty = value;
                MarkDirty("StockedQty");
            }
        }

        /// <summary>
        ///     Quantity that failed inspection.
        /// </summary>
        [ActiveColumn("ScrappedQty", DbType.Int16, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public short ScrappedQty
        {
            get => _scrappedQty;
            set
            {
                if (value == _scrappedQty && IsPropertyDirty("ScrappedQty"))
                    return;

                _scrappedQty = value;
                MarkDirty("ScrappedQty");
            }
        }

        /// <summary>
        ///     Work order start date.
        /// </summary>
        [ActiveColumn("StartDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value == _startDate && IsPropertyDirty("StartDate"))
                    return;

                _startDate = value;
                MarkDirty("StartDate");
            }
        }

        /// <summary>
        ///     Work order end date.
        /// </summary>
        [ActiveColumn("EndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (value == _endDate && IsPropertyDirty("EndDate"))
                    return;

                _endDate = value;
                MarkDirty("EndDate");
            }
        }

        /// <summary>
        ///     Work order due date.
        /// </summary>
        [ActiveColumn("DueDate", DbType.DateTime, ColumnProperties.None, Ordinal = 8, MaxLength = 0)]
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                if (value == _dueDate && IsPropertyDirty("DueDate"))
                    return;

                _dueDate = value;
                MarkDirty("DueDate");
            }
        }

        /// <summary>
        ///     Reason for inspection failure.
        /// </summary>
        [ActiveColumn("ScrapReasonID", DbType.Int16, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        [ForeignColumn("ScrapReasonID", typeof(ScrapReason), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int16)]
        public ScrapReason ScrapReason
        {
            get => _scrapReasonId;
            set
            {
                if (value == _scrapReasonId)
                    return;

                _scrapReasonId = value;
                MarkDirty("ScrapReasonID");
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
            public static QueryColumn WorkOrderId => FetchColumn("WorkOrderID");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn OrderQty => FetchColumn("OrderQty");

            public static QueryColumn StockedQty => FetchColumn("StockedQty");

            public static QueryColumn ScrappedQty => FetchColumn("ScrappedQty");

            public static QueryColumn StartDate => FetchColumn("StartDate");

            public static QueryColumn EndDate => FetchColumn("EndDate");

            public static QueryColumn DueDate => FetchColumn("DueDate");

            public static QueryColumn ScrapReason => FetchColumn("ScrapReasonID");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}