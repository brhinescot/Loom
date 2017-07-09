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
using OmniMount.Sales;

#endregion

namespace OmniMount.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.Contact table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Contact", "ContactId")]
    public class Contact : DataRecord<Contact>
    {
        private int? _addressId;
        private int? _companyId;

        private int _contactId;
        private string _emailAddress;
        private string _firstName;
        private string _lastName;

        public Contact() { }
        protected Contact(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ContactId
        {
            get => _contactId;
            set
            {
                if (value == _contactId && IsPropertyDirty("ContactId"))
                    return;

                _contactId = value;
                MarkDirty("ContactId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 30)]
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
        [ActiveColumn("LastName", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 30)]
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
        [ActiveColumn("EmailAddress", DbType.String, ColumnProperties.Unique, Ordinal = 4, MaxLength = 150)]
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
        [ActiveColumn("CompanyId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        [ForeignColumn("CompanyId", typeof(Company), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? CompanyId
        {
            get => _companyId;
            set
            {
                if (value == _companyId && IsPropertyDirty("CompanyId"))
                    return;

                _companyId = value;
                MarkDirty("CompanyId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AddressId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        [ForeignColumn("AddressId", typeof(Address), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? AddressId
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ContactId => FetchColumn("ContactId");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn EmailAddress => FetchColumn("EmailAddress");

            public static QueryColumn CompanyId => FetchColumn("CompanyId");

            public static QueryColumn AddressId => FetchColumn("AddressId");
        }

        #endregion
    }
}