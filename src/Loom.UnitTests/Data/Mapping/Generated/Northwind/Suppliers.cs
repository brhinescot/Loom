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

namespace Northwind
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.Suppliers table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Suppliers", "SupplierID")]
    public class Suppliers : DataRecord<Suppliers>
    {
        private string _address;
        private string _city;
        private string _companyName;
        private string _contactName;
        private string _contactTitle;
        private string _country;
        private string _fax;
        private string _homePage;
        private string _phone;
        private string _postalCode;
        private string _region;

        private int _supplierId;

        public Suppliers() { }
        protected Suppliers(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("SupplierID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int SupplierId
        {
            get => _supplierId;
            set
            {
                if (value == _supplierId)
                    return;

                _supplierId = value;
                MarkDirty("SupplierID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 40)]
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (value == _companyName)
                    return;

                _companyName = value;
                MarkDirty("CompanyName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactName", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 30)]
        public string ContactName
        {
            get => _contactName;
            set
            {
                if (value == _contactName)
                    return;

                _contactName = value;
                MarkDirty("ContactName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactTitle", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 30)]
        public string ContactTitle
        {
            get => _contactTitle;
            set
            {
                if (value == _contactTitle)
                    return;

                _contactTitle = value;
                MarkDirty("ContactTitle");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Address", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 60)]
        public string Address
        {
            get => _address;
            set
            {
                if (value == _address)
                    return;

                _address = value;
                MarkDirty("Address");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 15)]
        public string City
        {
            get => _city;
            set
            {
                if (value == _city)
                    return;

                _city = value;
                MarkDirty("City");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Region", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 15)]
        public string Region
        {
            get => _region;
            set
            {
                if (value == _region)
                    return;

                _region = value;
                MarkDirty("Region");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PostalCode", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 10)]
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (value == _postalCode)
                    return;

                _postalCode = value;
                MarkDirty("PostalCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Country", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 15)]
        public string Country
        {
            get => _country;
            set
            {
                if (value == _country)
                    return;

                _country = value;
                MarkDirty("Country");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Phone", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 24)]
        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone)
                    return;

                _phone = value;
                MarkDirty("Phone");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Fax", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 24)]
        public string Fax
        {
            get => _fax;
            set
            {
                if (value == _fax)
                    return;

                _fax = value;
                MarkDirty("Fax");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HomePage", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 1073741823)]
        public string HomePage
        {
            get => _homePage;
            set
            {
                if (value == _homePage)
                    return;

                _homePage = value;
                MarkDirty("HomePage");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn SupplierId => FetchColumn("SupplierID");

            public static QueryColumn CompanyName => FetchColumn("CompanyName");

            public static QueryColumn ContactName => FetchColumn("ContactName");

            public static QueryColumn ContactTitle => FetchColumn("ContactTitle");

            public static QueryColumn Address => FetchColumn("Address");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn Region => FetchColumn("Region");

            public static QueryColumn PostalCode => FetchColumn("PostalCode");

            public static QueryColumn Country => FetchColumn("Country");

            public static QueryColumn Phone => FetchColumn("Phone");

            public static QueryColumn Fax => FetchColumn("Fax");

            public static QueryColumn HomePage => FetchColumn("HomePage");
        }

        #endregion
    }
}