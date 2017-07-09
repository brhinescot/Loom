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
    ///     This is an DataRecord class which wraps the Sales.vIndividualCustomer table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "vIndividualCustomer", ReadOnly = true)]
    public class VIndividualCustomer : DataRecord<VIndividualCustomer>
    {
        private string _addressLine1;
        private string _addressLine2;
        private string _addressType;
        private int _businessEntityId;
        private string _city;
        private string _countryRegionName;
        private string _demographics;
        private string _emailAddress;
        private int _emailPromotion;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _phoneNumber;
        private string _phoneNumberType;
        private string _postalCode;
        private string _stateProvinceName;

        private string _suffix;
        private string _title;

        public VIndividualCustomer() { }
        protected VIndividualCustomer(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Suffix", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 10)]
        public string Suffix
        {
            get => _suffix;
            set
            {
                if (value == _suffix && IsPropertyDirty("Suffix"))
                    return;

                _suffix = value;
                MarkDirty("Suffix");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Demographics", DbType.String, ColumnProperties.Nullable, Ordinal = 18)]
        public string Demographics
        {
            get => _demographics;
            set
            {
                if (value == _demographics && IsPropertyDirty("Demographics"))
                    return;

                _demographics = value;
                MarkDirty("Demographics");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AddressLine1", DbType.String, ColumnProperties.None, Ordinal = 12, MaxLength = 60)]
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
        /// </summary>
        [ActiveColumn("EmailAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 50)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.None, Ordinal = 16, MaxLength = 15)]
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
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
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
        [ActiveColumn("Title", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 8)]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title && IsPropertyDirty("Title"))
                    return;

                _title = value;
                MarkDirty("Title");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PhoneNumberType", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 50)]
        public string PhoneNumberType
        {
            get => _phoneNumberType;
            set
            {
                if (value == _phoneNumberType && IsPropertyDirty("PhoneNumberType"))
                    return;

                _phoneNumberType = value;
                MarkDirty("PhoneNumberType");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("LastName", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
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
        [ActiveColumn("AddressType", DbType.String, ColumnProperties.None, Ordinal = 11, MaxLength = 50)]
        public string AddressType
        {
            get => _addressType;
            set
            {
                if (value == _addressType && IsPropertyDirty("AddressType"))
                    return;

                _addressType = value;
                MarkDirty("AddressType");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CountryRegionName", DbType.String, ColumnProperties.None, Ordinal = 17, MaxLength = 50)]
        public string CountryRegionName
        {
            get => _countryRegionName;
            set
            {
                if (value == _countryRegionName && IsPropertyDirty("CountryRegionName"))
                    return;

                _countryRegionName = value;
                MarkDirty("CountryRegionName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StateProvinceName", DbType.String, ColumnProperties.None, Ordinal = 15, MaxLength = 50)]
        public string StateProvinceName
        {
            get => _stateProvinceName;
            set
            {
                if (value == _stateProvinceName && IsPropertyDirty("StateProvinceName"))
                    return;

                _stateProvinceName = value;
                MarkDirty("StateProvinceName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.None, Ordinal = 14, MaxLength = 30)]
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
        [ActiveColumn("PhoneNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 25)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value == _phoneNumber && IsPropertyDirty("PhoneNumber"))
                    return;

                _phoneNumber = value;
                MarkDirty("PhoneNumber");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AddressLine2", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 60)]
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
        [ActiveColumn("EmailPromotion", DbType.Int32, ColumnProperties.None, Ordinal = 10, MaxLength = 0)]
        public int EmailPromotion
        {
            get => _emailPromotion;
            set
            {
                if (value == _emailPromotion && IsPropertyDirty("EmailPromotion"))
                    return;

                _emailPromotion = value;
                MarkDirty("EmailPromotion");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MiddleName", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 50)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Suffix => FetchColumn("Suffix");

            public static QueryColumn Demographics => FetchColumn("Demographics");

            public static QueryColumn AddressLine1 => FetchColumn("AddressLine1");

            public static QueryColumn EmailAddress => FetchColumn("EmailAddress");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn PhoneNumberType => FetchColumn("PhoneNumberType");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn AddressType => FetchColumn("AddressType");

            public static QueryColumn CountryRegionName => FetchColumn("CountryRegionName");

            public static QueryColumn StateProvinceName => FetchColumn("StateProvinceName");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn PhoneNumber => FetchColumn("PhoneNumber");

            public static QueryColumn AddressLine2 => FetchColumn("AddressLine2");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn EmailPromotion => FetchColumn("EmailPromotion");

            public static QueryColumn MiddleName => FetchColumn("MiddleName");
        }

        #endregion
    }
}