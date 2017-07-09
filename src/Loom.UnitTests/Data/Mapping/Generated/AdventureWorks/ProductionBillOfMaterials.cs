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
    ///     This is an DataRecord class which wraps the Production.BillOfMaterials table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "BillOfMaterials", "BillOfMaterialsID", ModifiedOnColumn = "ModifiedDate")]
    public class BillOfMaterials : DataRecord<BillOfMaterials>
    {
        private int _billOfMaterialsId;
        private short _bOMLevel;
        private int _componentId;
        private DateTime? _endDate;
        private DateTime _modifiedDate;
        private decimal _perAssemblyQty;
        private int? _productAssemblyId;
        private DateTime _startDate;
        private string _unitMeasureCode;

        public BillOfMaterials() { }
        protected BillOfMaterials(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for BillOfMaterials records.
        /// </summary>
        [ActiveColumn("BillOfMaterialsID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int BillOfMaterialsId
        {
            get => _billOfMaterialsId;
            set
            {
                if (value == _billOfMaterialsId && IsPropertyDirty("BillOfMaterialsID"))
                    return;

                _billOfMaterialsId = value;
                MarkDirty("BillOfMaterialsID");
            }
        }

        /// <summary>
        ///     Parent product identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductAssemblyID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? ProductAssemblyId
        {
            get => _productAssemblyId;
            set
            {
                if (value == _productAssemblyId && IsPropertyDirty("ProductAssemblyID"))
                    return;

                _productAssemblyId = value;
                MarkDirty("ProductAssemblyID");
            }
        }

        /// <summary>
        ///     Component identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ComponentID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ComponentId
        {
            get => _componentId;
            set
            {
                if (value == _componentId && IsPropertyDirty("ComponentID"))
                    return;

                _componentId = value;
                MarkDirty("ComponentID");
            }
        }

        /// <summary>
        ///     Date the component started being used in the assembly item.
        /// </summary>
        [ActiveColumn("StartDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
        ///     Date the component stopped being used in the assembly item.
        /// </summary>
        [ActiveColumn("EndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
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
        ///     Standard code identifying the unit of measure for the quantity.
        /// </summary>
        [ActiveColumn("UnitMeasureCode", DbType.String, ColumnProperties.ForeignKey, Ordinal = 6, MaxLength = 3)]
        [ForeignColumn("UnitMeasureCode", typeof(UnitMeasure), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string UnitMeasureCode
        {
            get => _unitMeasureCode;
            set
            {
                if (value == _unitMeasureCode && IsPropertyDirty("UnitMeasureCode"))
                    return;

                _unitMeasureCode = value;
                MarkDirty("UnitMeasureCode");
            }
        }

        /// <summary>
        ///     Indicates the depth the component is from its parent (AssemblyID).
        /// </summary>
        [ActiveColumn("BOMLevel", DbType.Int16, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
        public short BOMLevel
        {
            get => _bOMLevel;
            set
            {
                if (value == _bOMLevel && IsPropertyDirty("BOMLevel"))
                    return;

                _bOMLevel = value;
                MarkDirty("BOMLevel");
            }
        }

        /// <summary>
        ///     Quantity of the component needed to create the assembly.
        /// </summary>
        [ActiveColumn("PerAssemblyQty", DbType.Decimal, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((1.00))")]
        public decimal PerAssemblyQty
        {
            get => _perAssemblyQty;
            set
            {
                if (value == _perAssemblyQty && IsPropertyDirty("PerAssemblyQty"))
                    return;

                _perAssemblyQty = value;
                MarkDirty("PerAssemblyQty");
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
            public static QueryColumn BillOfMaterialsId => FetchColumn("BillOfMaterialsID");

            public static QueryColumn ProductAssemblyId => FetchColumn("ProductAssemblyID");

            public static QueryColumn ComponentId => FetchColumn("ComponentID");

            public static QueryColumn StartDate => FetchColumn("StartDate");

            public static QueryColumn EndDate => FetchColumn("EndDate");

            public static QueryColumn UnitMeasureCode => FetchColumn("UnitMeasureCode");

            public static QueryColumn BOMLevel => FetchColumn("BOMLevel");

            public static QueryColumn PerAssemblyQty => FetchColumn("PerAssemblyQty");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}