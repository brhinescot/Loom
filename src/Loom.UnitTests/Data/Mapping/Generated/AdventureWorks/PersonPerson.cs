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
    ///     This is an DataRecord class which wraps the Person.Person table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "Person", "BusinessEntityID", ModifiedOnColumn = "ModifiedDate")]
    public class Person : DataRecord<Person>
    {
        private string _additionalContactInfo;

        private int _businessEntityId;
        private string _demographics;
        private int _emailPromotion;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private DateTime _modifiedDate;
        private bool _nameStyle;
        private string _personType;
        private Guid _rowguid;
        private string _suffix;
        private string _title;

        public Person() { }
        protected Person(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Person records.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(BusinessEntity), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Primary type of person: SC = Store Contact, IN = Individual (retail) customer, SP = Sales person, EM = Employee
        ///     (non-sales), VC = Vendor contact, GC = General contact
        /// </summary>
        [ActiveColumn("PersonType", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 2)]
        public string PersonType
        {
            get => _personType;
            set
            {
                if (value == _personType && IsPropertyDirty("PersonType"))
                    return;

                _personType = value;
                MarkDirty("PersonType");
            }
        }

        /// <summary>
        ///     0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern
        ///     style (last name, first name) order.
        /// </summary>
        [ActiveColumn("NameStyle", DbType.Boolean, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((0))")]
        public bool NameStyle
        {
            get => _nameStyle;
            set
            {
                if (value == _nameStyle && IsPropertyDirty("NameStyle"))
                    return;

                _nameStyle = value;
                MarkDirty("NameStyle");
            }
        }

        /// <summary>
        ///     A courtesy title. For example, Mr. or Ms.
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
        ///     First name of the person.
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
        ///     Middle name or middle initial of the person.
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
        ///     Last name of the person.
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
        ///     Surname suffix. For example, Sr. or Jr.
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
        ///     0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from
        ///     AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners.
        /// </summary>
        [ActiveColumn("EmailPromotion", DbType.Int32, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
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
        ///     Additional contact information about the person stored in xml format.
        /// </summary>
        [ActiveColumn("AdditionalContactInfo", DbType.String, ColumnProperties.Nullable, Ordinal = 10)]
        public string AdditionalContactInfo
        {
            get => _additionalContactInfo;
            set
            {
                if (value == _additionalContactInfo && IsPropertyDirty("AdditionalContactInfo"))
                    return;

                _additionalContactInfo = value;
                MarkDirty("AdditionalContactInfo");
            }
        }

        /// <summary>
        ///     Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis.
        /// </summary>
        [ActiveColumn("Demographics", DbType.String, ColumnProperties.Nullable, Ordinal = 11)]
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
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 12, MaxLength = 0, DefaultValue = "(newid())")]
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
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 13, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn PersonType => FetchColumn("PersonType");

            public static QueryColumn NameStyle => FetchColumn("NameStyle");

            public static QueryColumn Title => FetchColumn("Title");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn MiddleName => FetchColumn("MiddleName");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn Suffix => FetchColumn("Suffix");

            public static QueryColumn EmailPromotion => FetchColumn("EmailPromotion");

            public static QueryColumn AdditionalContactInfo => FetchColumn("AdditionalContactInfo");

            public static QueryColumn Demographics => FetchColumn("Demographics");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}