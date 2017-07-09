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

namespace AdventureWorks.Person
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Person.Address table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "Address", "AddressID", ModifiedOnColumn = "ModifiedDate")]
    public class Address : DataRecord<Address>
    {
        private int _addressId;
        private string _addressLine1;
        private string _addressLine2;
        private string _city;
        private DateTime _modifiedDate;
        private string _postalCode;
        private Guid _rowguid;
        private string _spatialLocation;
        private int _stateProvinceId;

        public Address() { }
        protected Address(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Address records.
        /// </summary>
        [ActiveColumn("AddressID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int AddressId
        {
            get => _addressId;
            set
            {
                if (value == _addressId && IsPropertyDirty("AddressID"))
                    return;

                _addressId = value;
                MarkDirty("AddressID");
            }
        }

        /// <summary>
        ///     First street address line.
        /// </summary>
        [ActiveColumn("AddressLine1", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 60)]
        public string AddressLine1
        {
            get => _addressLine1;
            set
            {
                if (value == _addressLine1 && IsPropertyDirty("AddressLine1"))
                    return;

                _addressLine1 = value;
                MarkDirty("AddressLine1");
            }
        }

        /// <summary>
        ///     Second street address line.
        /// </summary>
        [ActiveColumn("AddressLine2", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 60)]
        public string AddressLine2
        {
            get => _addressLine2;
            set
            {
                if (value == _addressLine2 && IsPropertyDirty("AddressLine2"))
                    return;

                _addressLine2 = value;
                MarkDirty("AddressLine2");
            }
        }

        /// <summary>
        ///     Name of the city.
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 30)]
        public string City
        {
            get => _city;
            set
            {
                if (value == _city && IsPropertyDirty("City"))
                    return;

                _city = value;
                MarkDirty("City");
            }
        }

        /// <summary>
        ///     Unique identification number for the state or province. Foreign key to StateProvince table.
        /// </summary>
        [ActiveColumn("StateProvinceID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("StateProvinceID", typeof(StateProvince), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int StateProvinceId
        {
            get => _stateProvinceId;
            set
            {
                if (value == _stateProvinceId && IsPropertyDirty("StateProvinceID"))
                    return;

                _stateProvinceId = value;
                MarkDirty("StateProvinceID");
            }
        }

        /// <summary>
        ///     Postal code for the street address.
        /// </summary>
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 15)]
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (value == _postalCode && IsPropertyDirty("PostalCode"))
                    return;

                _postalCode = value;
                MarkDirty("PostalCode");
            }
        }

        /// <summary>
        ///     Latitude and longitude of this address.
        /// </summary>
        [ActiveColumn("SpatialLocation", DbType.String, ColumnProperties.Nullable, Ordinal = 7)]
        public string SpatialLocation
        {
            get => _spatialLocation;
            set
            {
                if (value == _spatialLocation && IsPropertyDirty("SpatialLocation"))
                    return;

                _spatialLocation = value;
                MarkDirty("SpatialLocation");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(newid())")]
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
            public static QueryColumn AddressId => FetchColumn("AddressID");

            public static QueryColumn AddressLine1 => FetchColumn("AddressLine1");

            public static QueryColumn AddressLine2 => FetchColumn("AddressLine2");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn StateProvinceId => FetchColumn("StateProvinceID");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn SpatialLocation => FetchColumn("SpatialLocation");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}