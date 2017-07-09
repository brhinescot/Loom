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
    ///     This is an DataRecord class which wraps the Sales.Address table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "Address", "AddressId")]
    public class Address : DataRecord<Address>
    {
        private int _addressId;
        private string _city;
        private string _country;
        private string _emailAddress;
        private string _fax;
        private decimal? _latitude;
        private string _line1;
        private string _line2;
        private string _line3;
        private decimal? _longitude;
        private string _notes;
        private string _phone;
        private string _postcode;
        private string _stateProvince;

        public Address() { }
        protected Address(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("AddressId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int AddressId
        {
            get => _addressId;
            set
            {
                if (value == _addressId && IsPropertyDirty("AddressId"))
                    return;

                _addressId = value;
                MarkDirty("AddressId");
            }
        }

        /// <summary>
        ///     The street/building number.
        /// </summary>
        [ActiveColumn("Line1", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 150)]
        public string Line1
        {
            get => _line1;
            set
            {
                if (value == _line1 && IsPropertyDirty("Line1"))
                    return;

                _line1 = value;
                MarkDirty("Line1");
            }
        }

        /// <summary>
        ///     The suite/apartment number.
        /// </summary>
        [ActiveColumn("Line2", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 150)]
        public string Line2
        {
            get => _line2;
            set
            {
                if (value == _line2 && IsPropertyDirty("Line2"))
                    return;

                _line2 = value;
                MarkDirty("Line2");
            }
        }

        /// <summary>
        ///     The area/locality.
        /// </summary>
        [ActiveColumn("Line3", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 150)]
        public string Line3
        {
            get => _line3;
            set
            {
                if (value == _line3 && IsPropertyDirty("Line3"))
                    return;

                _line3 = value;
                MarkDirty("Line3");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
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
        /// </summary>
        [ActiveColumn("Postcode", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 12)]
        public string Postcode
        {
            get => _postcode;
            set
            {
                if (value == _postcode && IsPropertyDirty("Postcode"))
                    return;

                _postcode = value;
                MarkDirty("Postcode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StateProvince", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 50)]
        public string StateProvince
        {
            get => _stateProvince;
            set
            {
                if (value == _stateProvince && IsPropertyDirty("StateProvince"))
                    return;

                _stateProvince = value;
                MarkDirty("StateProvince");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Country", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 100)]
        public string Country
        {
            get => _country;
            set
            {
                if (value == _country && IsPropertyDirty("Country"))
                    return;

                _country = value;
                MarkDirty("Country");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Notes", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 100)]
        public string Notes
        {
            get => _notes;
            set
            {
                if (value == _notes && IsPropertyDirty("Notes"))
                    return;

                _notes = value;
                MarkDirty("Notes");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Latitude", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 0)]
        public decimal? Latitude
        {
            get => _latitude;
            set
            {
                if (value == _latitude && IsPropertyDirty("Latitude"))
                    return;

                _latitude = value;
                MarkDirty("Latitude");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Longitude", DbType.Decimal, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 0)]
        public decimal? Longitude
        {
            get => _longitude;
            set
            {
                if (value == _longitude && IsPropertyDirty("Longitude"))
                    return;

                _longitude = value;
                MarkDirty("Longitude");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Phone", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 20)]
        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone && IsPropertyDirty("Phone"))
                    return;

                _phone = value;
                MarkDirty("Phone");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Fax", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 20)]
        public string Fax
        {
            get => _fax;
            set
            {
                if (value == _fax && IsPropertyDirty("Fax"))
                    return;

                _fax = value;
                MarkDirty("Fax");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmailAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 14, MaxLength = 150)]
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                if (value == _emailAddress && IsPropertyDirty("EmailAddress"))
                    return;

                _emailAddress = value;
                MarkDirty("EmailAddress");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn AddressId => FetchColumn("AddressId");

            public static QueryColumn Line1 => FetchColumn("Line1");

            public static QueryColumn Line2 => FetchColumn("Line2");

            public static QueryColumn Line3 => FetchColumn("Line3");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn Postcode => FetchColumn("Postcode");

            public static QueryColumn StateProvince => FetchColumn("StateProvince");

            public static QueryColumn Country => FetchColumn("Country");

            public static QueryColumn Notes => FetchColumn("Notes");

            public static QueryColumn Latitude => FetchColumn("Latitude");

            public static QueryColumn Longitude => FetchColumn("Longitude");

            public static QueryColumn Phone => FetchColumn("Phone");

            public static QueryColumn Fax => FetchColumn("Fax");

            public static QueryColumn EmailAddress => FetchColumn("EmailAddress");
        }

        #endregion
    }
}