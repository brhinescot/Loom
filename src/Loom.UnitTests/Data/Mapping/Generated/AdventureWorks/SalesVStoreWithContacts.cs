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
    ///     This is an DataRecord class which wraps the Sales.vStoreWithContacts table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "vStoreWithContacts", ReadOnly = true)]
    public class VStoreWithContacts : DataRecord<VStoreWithContacts>
    {
        private int _businessEntityId;

        private string _contactType;
        private string _emailAddress;
        private int _emailPromotion;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _name;
        private string _phoneNumber;
        private string _phoneNumberType;
        private string _suffix;
        private string _title;

        public VStoreWithContacts() { }
        protected VStoreWithContacts(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactType", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string ContactType
        {
            get => _contactType;
            set
            {
                if (value == _contactType && IsPropertyDirty("ContactType"))
                    return;

                _contactType = value;
                MarkDirty("ContactType");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("EmailAddress", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 50)]
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
        [ActiveColumn("Title", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 8)]
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
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
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
        [ActiveColumn("PhoneNumberType", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 50)]
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
        [ActiveColumn("LastName", DbType.String, ColumnProperties.None, Ordinal = 7, MaxLength = 50)]
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
        [ActiveColumn("PhoneNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 25)]
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
        [ActiveColumn("EmailPromotion", DbType.Int32, ColumnProperties.None, Ordinal = 12, MaxLength = 0)]
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
        [ActiveColumn("MiddleName", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 50)]
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
        [ActiveColumn("Suffix", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 10)]
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
            public static QueryColumn ContactType => FetchColumn("ContactType");

            public static QueryColumn EmailAddress => FetchColumn("EmailAddress");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn PhoneNumberType => FetchColumn("PhoneNumberType");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn PhoneNumber => FetchColumn("PhoneNumber");

            public static QueryColumn EmailPromotion => FetchColumn("EmailPromotion");

            public static QueryColumn MiddleName => FetchColumn("MiddleName");

            public static QueryColumn Suffix => FetchColumn("Suffix");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}