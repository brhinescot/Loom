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

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.IncomeRange table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "IncomeRange", "IncomeRangeId")]
    public class IncomeRange : DataRecord<IncomeRange>
    {
        private int _incomeRangeId;
        private int? _max;
        private int? _min;

        public IncomeRange() { }
        protected IncomeRange(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("IncomeRangeId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int IncomeRangeId
        {
            get => _incomeRangeId;
            set
            {
                if (value == _incomeRangeId && IsPropertyDirty("IncomeRangeId"))
                    return;

                _incomeRangeId = value;
                MarkDirty("IncomeRangeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Min", DbType.Int32, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public int? Min
        {
            get => _min;
            set
            {
                if (value == _min && IsPropertyDirty("Min"))
                    return;

                _min = value;
                MarkDirty("Min");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Max", DbType.Int32, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public int? Max
        {
            get => _max;
            set
            {
                if (value == _max && IsPropertyDirty("Max"))
                    return;

                _max = value;
                MarkDirty("Max");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn IncomeRangeId => FetchColumn("IncomeRangeId");

            public static QueryColumn Min => FetchColumn("Min");

            public static QueryColumn Max => FetchColumn("Max");
        }

        #endregion
    }
}