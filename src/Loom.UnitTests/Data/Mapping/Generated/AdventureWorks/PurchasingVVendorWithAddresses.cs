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

namespace AdventureWorks.Purchasing
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Purchasing.vVendorWithAddresses table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Purchasing", "vVendorWithAddresses", ReadOnly = true)]
    public class VVendorWithAddresses : DataRecord<VVendorWithAddresses>
    {
        private string _addressLine1;
        private string _addressLine2;

        private string _addressType;
        private int _businessEntityId;
        private string _city;
        private string _countryRegionName;
        private string _name;
        private string _postalCode;
        private string _stateProvinceName;

        public VVendorWithAddresses() { }
        protected VVendorWithAddresses(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("AddressType", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
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
        [ActiveColumn("StateProvinceName", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 50)]
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
        [ActiveColumn("AddressLine1", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 60)]
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
        [ActiveColumn("City", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 30)]
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
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.None, Ordinal = 8, MaxLength = 15)]
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
        [ActiveColumn("CountryRegionName", DbType.String, ColumnProperties.None, Ordinal = 9, MaxLength = 50)]
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
        [ActiveColumn("AddressLine2", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 60)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn AddressType => FetchColumn("AddressType");

            public static QueryColumn StateProvinceName => FetchColumn("StateProvinceName");

            public static QueryColumn AddressLine1 => FetchColumn("AddressLine1");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn CountryRegionName => FetchColumn("CountryRegionName");

            public static QueryColumn AddressLine2 => FetchColumn("AddressLine2");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}