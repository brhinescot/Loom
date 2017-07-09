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

namespace AdventureWorks.HumanResources
{
    /// <summary>
    ///     This is an DataRecord class which wraps the HumanResources.Shift table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("HumanResources", "Shift", "ShiftID", ModifiedOnColumn = "ModifiedDate")]
    public class Shift : DataRecord<Shift>
    {
        private string _endTime;
        private DateTime _modifiedDate;
        private string _name;

        private short _shiftId;
        private string _startTime;

        public Shift() { }
        protected Shift(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Shift records.
        /// </summary>
        [ActiveColumn("ShiftID", DbType.Int16, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public short ShiftId
        {
            get => _shiftId;
            set
            {
                if (value == _shiftId && IsPropertyDirty("ShiftID"))
                    return;

                _shiftId = value;
                MarkDirty("ShiftID");
            }
        }

        /// <summary>
        ///     Shift description.
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
        ///     Shift start time.
        /// </summary>
        [ActiveColumn("StartTime", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value == _startTime && IsPropertyDirty("StartTime"))
                    return;

                _startTime = value;
                MarkDirty("StartTime");
            }
        }

        /// <summary>
        ///     Shift end time.
        /// </summary>
        [ActiveColumn("EndTime", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public string EndTime
        {
            get => _endTime;
            set
            {
                if (value == _endTime && IsPropertyDirty("EndTime"))
                    return;

                _endTime = value;
                MarkDirty("EndTime");
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
            public static QueryColumn ShiftId => FetchColumn("ShiftID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn StartTime => FetchColumn("StartTime");

            public static QueryColumn EndTime => FetchColumn("EndTime");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}