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
    ///     This is an DataRecord class which wraps the Production.vProductModelInstructions table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "vProductModelInstructions", ReadOnly = true, ModifiedOnColumn = "ModifiedDate")]
    public class VProductModelInstructions : DataRecord<VProductModelInstructions>
    {
        private string _instructions;
        private decimal? _laborHours;

        private int? _locationId;
        private int? _lotSize;
        private decimal? _machineHours;
        private DateTime _modifiedDate;
        private string _name;
        private int _productModelId;
        private Guid _rowguid;
        private decimal? _setupHours;
        private string _step;

        public VProductModelInstructions() { }
        protected VProductModelInstructions(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("LocationID", DbType.Int32, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public int? LocationId
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
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("LaborHours", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public decimal? LaborHours
        {
            get => _laborHours;
            set
            {
                if (value == _laborHours && IsPropertyDirty("LaborHours"))
                    return;

                _laborHours = value;
                MarkDirty("LaborHours");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("LotSize", DbType.Int32, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public int? LotSize
        {
            get => _lotSize;
            set
            {
                if (value == _lotSize && IsPropertyDirty("LotSize"))
                    return;

                _lotSize = value;
                MarkDirty("LotSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("SetupHours", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public decimal? SetupHours
        {
            get => _setupHours;
            set
            {
                if (value == _setupHours && IsPropertyDirty("SetupHours"))
                    return;

                _setupHours = value;
                MarkDirty("SetupHours");
            }
        }

        /// <summary>
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
        /// </summary>
        [ActiveColumn("Instructions", DbType.String, ColumnProperties.Nullable, Ordinal = 3)]
        public string Instructions
        {
            get => _instructions;
            set
            {
                if (value == _instructions && IsPropertyDirty("Instructions"))
                    return;

                _instructions = value;
                MarkDirty("Instructions");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MachineHours", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public decimal? MachineHours
        {
            get => _machineHours;
            set
            {
                if (value == _machineHours && IsPropertyDirty("MachineHours"))
                    return;

                _machineHours = value;
                MarkDirty("MachineHours");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 11, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Step", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 1024)]
        public string Step
        {
            get => _step;
            set
            {
                if (value == _step && IsPropertyDirty("Step"))
                    return;

                _step = value;
                MarkDirty("Step");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductModelID", DbType.Int32, ColumnProperties.Identity, Ordinal = 1, MaxLength = 0)]
        public int ProductModelId
        {
            get => _productModelId;
            set
            {
                if (value == _productModelId && IsPropertyDirty("ProductModelID"))
                    return;

                _productModelId = value;
                MarkDirty("ProductModelID");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn LocationId => FetchColumn("LocationID");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn LaborHours => FetchColumn("LaborHours");

            public static QueryColumn LotSize => FetchColumn("LotSize");

            public static QueryColumn SetupHours => FetchColumn("SetupHours");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Instructions => FetchColumn("Instructions");

            public static QueryColumn MachineHours => FetchColumn("MachineHours");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");

            public static QueryColumn Step => FetchColumn("Step");

            public static QueryColumn ProductModelId => FetchColumn("ProductModelID");
        }

        #endregion
    }
}