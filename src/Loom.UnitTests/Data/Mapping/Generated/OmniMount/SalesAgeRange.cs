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
    ///     This is an DataRecord class which wraps the Sales.AgeRange table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "AgeRange", "AgeRangeId")]
    public class AgeRange : DataRecord<AgeRange>
    {
        private int _ageRangeId;
        private string _friendly;
        private int? _max;
        private int _min;

        public AgeRange() { }
        protected AgeRange(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("AgeRangeId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int AgeRangeId
        {
            get => _ageRangeId;
            set
            {
                if (value == _ageRangeId && IsPropertyDirty("AgeRangeId"))
                    return;

                _ageRangeId = value;
                MarkDirty("AgeRangeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Min", DbType.Int32, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
        public int Min
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

        /// <summary>
        /// </summary>
        [ActiveColumn("Friendly", DbType.String, ColumnProperties.Computed | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 5)]
        public string Friendly
        {
            get => _friendly;
            set
            {
                if (value == _friendly && IsPropertyDirty("Friendly"))
                    return;

                _friendly = value;
                MarkDirty("Friendly");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn AgeRangeId => FetchColumn("AgeRangeId");

            public static QueryColumn Min => FetchColumn("Min");

            public static QueryColumn Max => FetchColumn("Max");

            public static QueryColumn Friendly => FetchColumn("Friendly");
        }

        #endregion
    }
}