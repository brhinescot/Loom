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
    ///     This is an DataRecord class which wraps the Production.Location table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "Location", "LocationID", ModifiedOnColumn = "ModifiedDate")]
    public class Location : DataRecord<Location>
    {
        private decimal _availability;
        private decimal _costRate;

        private short _locationId;
        private DateTime _modifiedDate;
        private string _name;

        public Location() { }
        protected Location(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Location records.
        /// </summary>
        [ActiveColumn("LocationID", DbType.Int16, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Location description.
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
        ///     Standard hourly cost of the manufacturing location.
        /// </summary>
        [ActiveColumn("CostRate", DbType.Currency, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal CostRate
        {
            get => _costRate;
            set
            {
                if (value == _costRate && IsPropertyDirty("CostRate"))
                    return;

                _costRate = value;
                MarkDirty("CostRate");
            }
        }

        /// <summary>
        ///     Work capacity (in hours) of the manufacturing location.
        /// </summary>
        [ActiveColumn("Availability", DbType.Decimal, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal Availability
        {
            get => _availability;
            set
            {
                if (value == _availability && IsPropertyDirty("Availability"))
                    return;

                _availability = value;
                MarkDirty("Availability");
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
            public static QueryColumn LocationId => FetchColumn("LocationID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn CostRate => FetchColumn("CostRate");

            public static QueryColumn Availability => FetchColumn("Availability");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}