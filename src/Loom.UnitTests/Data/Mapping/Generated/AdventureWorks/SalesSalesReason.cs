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
    ///     This is an DataRecord class which wraps the Sales.SalesReason table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesReason", "SalesReasonID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesReason : DataRecord<SalesReason>
    {
        private DateTime _modifiedDate;
        private string _name;
        private string _reasonType;

        private int _salesReasonId;

        public SalesReason() { }
        protected SalesReason(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for SalesReason records.
        /// </summary>
        [ActiveColumn("SalesReasonID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Sales reason description.
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
        ///     Category the sales reason belongs to.
        /// </summary>
        [ActiveColumn("ReasonType", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string ReasonType
        {
            get => _reasonType;
            set
            {
                if (value == _reasonType && IsPropertyDirty("ReasonType"))
                    return;

                _reasonType = value;
                MarkDirty("ReasonType");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn SalesReasonId => FetchColumn("SalesReasonID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ReasonType => FetchColumn("ReasonType");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}