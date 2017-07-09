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
    ///     This is an DataRecord class which wraps the Production.WorkOrderRouting table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "WorkOrderRouting", "WorkOrderID", ModifiedOnColumn = "ModifiedDate")]
    public class WorkOrderRouting : DataRecord<WorkOrderRouting>
    {
        private decimal? _actualCost;
        private DateTime? _actualEndDate;
        private decimal? _actualResourceHrs;
        private DateTime? _actualStartDate;
        private short _locationId;
        private DateTime _modifiedDate;
        private short _operationSequence;
        private decimal _plannedCost;
        private int _productId;
        private DateTime _scheduledEndDate;
        private DateTime _scheduledStartDate;

        private int _workOrderId;

        public WorkOrderRouting() { }
        protected WorkOrderRouting(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to WorkOrder.WorkOrderID.
        /// </summary>
        [ActiveColumn("WorkOrderID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("WorkOrderID", typeof(WorkOrder), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Primary key. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
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
        ///     Primary key. Indicates the manufacturing process sequence.
        /// </summary>
        [ActiveColumn("OperationSequence", DbType.Int16, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 3, MaxLength = 0)]
        public short OperationSequence
        {
            get => _operationSequence;
            set
            {
                if (value == _operationSequence && IsPropertyDirty("OperationSequence"))
                    return;

                _operationSequence = value;
                MarkDirty("OperationSequence");
            }
        }

        /// <summary>
        ///     Manufacturing location where the part is processed. Foreign key to Location.LocationID.
        /// </summary>
        [ActiveColumn("LocationID", DbType.Int16, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("LocationID", typeof(Location), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int16)]
        public short LocationId
        {
            get => _locationId;
            set
            {
                if (value == _locationId && IsPropertyDirty("LocationID"))
                    return;

                _locationId = value;
                MarkDirty("LocationID");
            }
        }

        /// <summary>
        ///     Planned manufacturing start date.
        /// </summary>
        [ActiveColumn("ScheduledStartDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public DateTime ScheduledStartDate
        {
            get => _scheduledStartDate;
            set
            {
                if (value == _scheduledStartDate && IsPropertyDirty("ScheduledStartDate"))
                    return;

                _scheduledStartDate = value;
                MarkDirty("ScheduledStartDate");
            }
        }

        /// <summary>
        ///     Planned manufacturing end date.
        /// </summary>
        [ActiveColumn("ScheduledEndDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
        public DateTime ScheduledEndDate
        {
            get => _scheduledEndDate;
            set
            {
                if (value == _scheduledEndDate && IsPropertyDirty("ScheduledEndDate"))
                    return;

                _scheduledEndDate = value;
                MarkDirty("ScheduledEndDate");
            }
        }

        /// <summary>
        ///     Actual start date.
        /// </summary>
        [ActiveColumn("ActualStartDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public DateTime? ActualStartDate
        {
            get => _actualStartDate;
            set
            {
                if (value == _actualStartDate && IsPropertyDirty("ActualStartDate"))
                    return;

                _actualStartDate = value;
                MarkDirty("ActualStartDate");
            }
        }

        /// <summary>
        ///     Actual end date.
        /// </summary>
        [ActiveColumn("ActualEndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public DateTime? ActualEndDate
        {
            get => _actualEndDate;
            set
            {
                if (value == _actualEndDate && IsPropertyDirty("ActualEndDate"))
                    return;

                _actualEndDate = value;
                MarkDirty("ActualEndDate");
            }
        }

        /// <summary>
        ///     Number of manufacturing hours used.
        /// </summary>
        [ActiveColumn("ActualResourceHrs", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        public decimal? ActualResourceHrs
        {
            get => _actualResourceHrs;
            set
            {
                if (value == _actualResourceHrs && IsPropertyDirty("ActualResourceHrs"))
                    return;

                _actualResourceHrs = value;
                MarkDirty("ActualResourceHrs");
            }
        }

        /// <summary>
        ///     Estimated manufacturing cost.
        /// </summary>
        [ActiveColumn("PlannedCost", DbType.Currency, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
        public decimal PlannedCost
        {
            get => _plannedCost;
            set
            {
                if (value == _plannedCost && IsPropertyDirty("PlannedCost"))
                    return;

                _plannedCost = value;
                MarkDirty("PlannedCost");
            }
        }

        /// <summary>
        ///     Actual manufacturing cost.
        /// </summary>
        [ActiveColumn("ActualCost", DbType.Currency, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 0)]
        public decimal? ActualCost
        {
            get => _actualCost;
            set
            {
                if (value == _actualCost && IsPropertyDirty("ActualCost"))
                    return;

                _actualCost = value;
                MarkDirty("ActualCost");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 12, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn OperationSequence => FetchColumn("OperationSequence");

            public static QueryColumn LocationId => FetchColumn("LocationID");

            public static QueryColumn ScheduledStartDate => FetchColumn("ScheduledStartDate");

            public static QueryColumn ScheduledEndDate => FetchColumn("ScheduledEndDate");

            public static QueryColumn ActualStartDate => FetchColumn("ActualStartDate");

            public static QueryColumn ActualEndDate => FetchColumn("ActualEndDate");

            public static QueryColumn ActualResourceHrs => FetchColumn("ActualResourceHrs");

            public static QueryColumn PlannedCost => FetchColumn("PlannedCost");

            public static QueryColumn ActualCost => FetchColumn("ActualCost");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}