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
    ///     This is an DataRecord class which wraps the Person.PhoneNumberType table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "PhoneNumberType", "PhoneNumberTypeID", ModifiedOnColumn = "ModifiedDate")]
    public class PhoneNumberType : DataRecord<PhoneNumberType>
    {
        private DateTime _modifiedDate;
        private string _name;

        private int _phoneNumberTypeId;

        public PhoneNumberType() { }
        protected PhoneNumberType(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for telephone number type records.
        /// </summary>
        [ActiveColumn("PhoneNumberTypeID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Name of the telephone number type
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

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn PhoneNumberTypeId => FetchColumn("PhoneNumberTypeID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}