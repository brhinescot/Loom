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
    ///     This is an DataRecord class which wraps the Sales.vSalesPerson table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "vSalesPerson", ReadOnly = true)]
    public class VSalesPerson : DataRecord<VSalesPerson>
    {
        private string _addressLine1;
        private string _addressLine2;
        private int _businessEntityId;
        private string _city;
        private string _countryRegionName;
        private string _emailAddress;
        private int _emailPromotion;
        private string _firstName;

        private string _jobTitle;
        private string _lastName;
        private string _middleName;
        private string _phoneNumber;
        private string _phoneNumberType;
        private string _postalCode;
        private decimal _salesLastYear;
        private decimal? _salesQuota;
        private decimal _salesYTD;
        private string _stateProvinceName;
        private string _suffix;
        private string _territoryGroup;
        private string _territoryName;
        private string _title;

        public VSalesPerson() { }
        protected VSalesPerson(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("JobTitle", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 50)]
        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                if (value == _jobTitle && IsPropertyDirty("JobTitle"))
                    return;

                _jobTitle = value;
                MarkDirty("JobTitle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmailAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 50)]
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
        [ActiveColumn("PhoneNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 25)]
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
        [ActiveColumn("SalesQuota", DbType.Currency, ColumnProperties.Nullable, Ordinal = 20, MaxLength = 0)]
        public decimal? SalesQuota
        {
            get => _salesQuota;
            set
            {
                if (value == _salesQuota && IsPropertyDirty("SalesQuota"))
                    return;

                _salesQuota = value;
                MarkDirty("SalesQuota");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PhoneNumberType", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 50)]
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
        [ActiveColumn("EmailPromotion", DbType.Int32, ColumnProperties.None, Ordinal = 11, MaxLength = 0)]
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
        [ActiveColumn("SalesLastYear", DbType.Currency, ColumnProperties.None, Ordinal = 22, MaxLength = 0)]
        public decimal SalesLastYear
        {
            get => _salesLastYear;
            set
            {
                if (value == _salesLastYear && IsPropertyDirty("SalesLastYear"))
                    return;

                _salesLastYear = value;
                MarkDirty("SalesLastYear");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TerritoryGroup", DbType.String, ColumnProperties.Nullable, Ordinal = 19, MaxLength = 50)]
        public string TerritoryGroup
        {
            get => _territoryGroup;
            set
            {
                if (value == _territoryGroup && IsPropertyDirty("TerritoryGroup"))
                    return;

                _territoryGroup = value;
                MarkDirty("TerritoryGroup");
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
        [ActiveColumn("SalesYTD", DbType.Currency, ColumnProperties.None, Ordinal = 21, MaxLength = 0)]
        public decimal SalesYTD
        {
            get => _salesYTD;
            set
            {
                if (value == _salesYTD && IsPropertyDirty("SalesYTD"))
                    return;

                _salesYTD = value;
                MarkDirty("SalesYTD");
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

        /// <summary>
        /// </summary>
        [ActiveColumn("TerritoryName", DbType.String, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 50)]
        public string TerritoryName
        {
            get => _territoryName;
            set
            {
                if (value == _territoryName && IsPropertyDirty("TerritoryName"))
                    return;

                _territoryName = value;
                MarkDirty("TerritoryName");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn JobTitle => FetchColumn("JobTitle");

            public static QueryColumn EmailAddress => FetchColumn("EmailAddress");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn CountryRegionName => FetchColumn("CountryRegionName");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn PhoneNumber => FetchColumn("PhoneNumber");

            public static QueryColumn SalesQuota => FetchColumn("SalesQuota");

            public static QueryColumn PhoneNumberType => FetchColumn("PhoneNumberType");

            public static QueryColumn Suffix => FetchColumn("Suffix");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn EmailPromotion => FetchColumn("EmailPromotion");

            public static QueryColumn SalesLastYear => FetchColumn("SalesLastYear");

            public static QueryColumn TerritoryGroup => FetchColumn("TerritoryGroup");

            public static QueryColumn AddressLine2 => FetchColumn("AddressLine2");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn AddressLine1 => FetchColumn("AddressLine1");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn StateProvinceName => FetchColumn("StateProvinceName");

            public static QueryColumn SalesYTD => FetchColumn("SalesYTD");

            public static QueryColumn MiddleName => FetchColumn("MiddleName");

            public static QueryColumn TerritoryName => FetchColumn("TerritoryName");
        }

        #endregion
    }
}