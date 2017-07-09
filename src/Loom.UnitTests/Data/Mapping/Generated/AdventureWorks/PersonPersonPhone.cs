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
    ///     This is an DataRecord class which wraps the Person.PersonPhone table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "PersonPhone", "PhoneNumberTypeID", ModifiedOnColumn = "ModifiedDate")]
    public class PersonPhone : DataRecord<PersonPhone>
    {
        private int _businessEntityId;
        private DateTime _modifiedDate;
        private string _phoneNumber;
        private int _phoneNumberTypeId;

        public PersonPhone() { }
        protected PersonPhone(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Business entity identification number. Foreign key to Person.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Person), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Telephone number identification number.
        /// </summary>
        [ActiveColumn("PhoneNumber", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 25)]
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
        ///     Kind of phone number. Foreign key to PhoneNumberType.PhoneNumberTypeID.
        /// </summary>
        [ActiveColumn("PhoneNumberTypeID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("PhoneNumberTypeID", typeof(PhoneNumberType), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int PhoneNumberTypeId
        {
            get => _phoneNumberTypeId;
            set
            {
                if (value == _phoneNumberTypeId && IsPropertyDirty("PhoneNumberTypeID"))
                    return;

                _phoneNumberTypeId = value;
                MarkDirty("PhoneNumberTypeID");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn PhoneNumber => FetchColumn("PhoneNumber");

            public static QueryColumn PhoneNumberTypeId => FetchColumn("PhoneNumberTypeID");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}