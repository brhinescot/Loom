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
    ///     This is an DataRecord class which wraps the Person.vAdditionalContactInfo table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "vAdditionalContactInfo", ReadOnly = true, ModifiedOnColumn = "ModifiedDate")]
    public class VAdditionalContactInfo : DataRecord<VAdditionalContactInfo>
    {
        private int _businessEntityId;
        private string _city;
        private string _countryRegion;
        private string _eMailAddress;
        private string _eMailSpecialInstructions;
        private string _eMailTelephoneNumber;

        private string _firstName;
        private string _homeAddressSpecialInstructions;
        private string _lastName;
        private string _middleName;
        private DateTime _modifiedDate;
        private string _postalCode;
        private Guid _rowguid;
        private string _stateProvince;
        private string _street;
        private string _telephoneNumber;
        private string _telephoneSpecialInstructions;

        public VAdditionalContactInfo() { }
        protected VAdditionalContactInfo(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName && IsPropertyDirty("FirstName"))
                    return;

                _firstName = value;
                MarkDirty("FirstName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 50)]
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
        [ActiveColumn("TelephoneNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
        public string TelephoneNumber
        {
            get => _telephoneNumber;
            set
            {
                if (value == _telephoneNumber && IsPropertyDirty("TelephoneNumber"))
                    return;

                _telephoneNumber = value;
                MarkDirty("TelephoneNumber");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MiddleName", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 50)]
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (value == _middleName && IsPropertyDirty("MiddleName"))
                    return;

                _middleName = value;
                MarkDirty("MiddleName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TelephoneSpecialInstructions", DbType.String, ColumnProperties.Nullable, Ordinal = 6)]
        public string TelephoneSpecialInstructions
        {
            get => _telephoneSpecialInstructions;
            set
            {
                if (value == _telephoneSpecialInstructions && IsPropertyDirty("TelephoneSpecialInstructions"))
                    return;

                _telephoneSpecialInstructions = value;
                MarkDirty("TelephoneSpecialInstructions");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CountryRegion", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 50)]
        public string CountryRegion
        {
            get => _countryRegion;
            set
            {
                if (value == _countryRegion && IsPropertyDirty("CountryRegion"))
                    return;

                _countryRegion = value;
                MarkDirty("CountryRegion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 17, MaxLength = 0)]
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
        [ActiveColumn("EMailSpecialInstructions", DbType.String, ColumnProperties.Nullable, Ordinal = 14)]
        public string EMailSpecialInstructions
        {
            get => _eMailSpecialInstructions;
            set
            {
                if (value == _eMailSpecialInstructions && IsPropertyDirty("EMailSpecialInstructions"))
                    return;

                _eMailSpecialInstructions = value;
                MarkDirty("EMailSpecialInstructions");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EMailTelephoneNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 15, MaxLength = 50)]
        public string EMailTelephoneNumber
        {
            get => _eMailTelephoneNumber;
            set
            {
                if (value == _eMailTelephoneNumber && IsPropertyDirty("EMailTelephoneNumber"))
                    return;

                _eMailTelephoneNumber = value;
                MarkDirty("EMailTelephoneNumber");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("LastName", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName && IsPropertyDirty("LastName"))
                    return;

                _lastName = value;
                MarkDirty("LastName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.None, Ordinal = 1, MaxLength = 0)]
        public int BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StateProvince", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 50)]
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
        [ActiveColumn("HomeAddressSpecialInstructions", DbType.String, ColumnProperties.Nullable, Ordinal = 12)]
        public string HomeAddressSpecialInstructions
        {
            get => _homeAddressSpecialInstructions;
            set
            {
                if (value == _homeAddressSpecialInstructions && IsPropertyDirty("HomeAddressSpecialInstructions"))
                    return;

                _homeAddressSpecialInstructions = value;
                MarkDirty("HomeAddressSpecialInstructions");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 16, MaxLength = 0)]
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
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 50)]
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
        /// </summary>
        [ActiveColumn("EMailAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 128)]
        public string EMailAddress
        {
            get => _eMailAddress;
            set
            {
                if (value == _eMailAddress && IsPropertyDirty("EMailAddress"))
                    return;

                _eMailAddress = value;
                MarkDirty("EMailAddress");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Street", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 50)]
        public string Street
        {
            get => _street;
            set
            {
                if (value == _street && IsPropertyDirty("Street"))
                    return;

                _street = value;
                MarkDirty("Street");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn TelephoneNumber => FetchColumn("TelephoneNumber");

            public static QueryColumn MiddleName => FetchColumn("MiddleName");

            public static QueryColumn TelephoneSpecialInstructions => FetchColumn("TelephoneSpecialInstructions");

            public static QueryColumn CountryRegion => FetchColumn("CountryRegion");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");

            public static QueryColumn EMailSpecialInstructions => FetchColumn("EMailSpecialInstructions");

            public static QueryColumn EMailTelephoneNumber => FetchColumn("EMailTelephoneNumber");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn StateProvince => FetchColumn("StateProvince");

            public static QueryColumn HomeAddressSpecialInstructions => FetchColumn("HomeAddressSpecialInstructions");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn EMailAddress => FetchColumn("EMailAddress");

            public static QueryColumn Street => FetchColumn("Street");
        }

        #endregion
    }
}