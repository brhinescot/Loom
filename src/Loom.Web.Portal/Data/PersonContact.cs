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
using Loom.Web.Portal.Data.Portal;

#endregion

namespace Loom.Web.Portal.Data.Person
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Person.Contact table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "Contact", "ContactId")]
    public class Contact : DataRecord<Contact>
    {
        private int _contactId;
        private string _email;
        private int _entityId;
        private string _firstName;
        private string _lastName;

        public Contact() { }
        protected Contact(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     The record's primary key.
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
        ///     The contact's first name.
        /// </summary>
        [ActiveColumn("FirstName", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 50)]
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
        ///     The contact's last name.
        /// </summary>
        [ActiveColumn("LastName", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 50)]
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
        [ActiveColumn("EntityId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
        [ForeignColumn("EntityId", typeof(EntityBase), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int EntityId
        {
            get => _entityId;
            set
            {
                if (value == _entityId && IsPropertyDirty("EntityId"))
                    return;

                _entityId = value;
                MarkDirty("EntityId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Email", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
        public string Email
        {
            get => _email;
            set
            {
                if (value == _email && IsPropertyDirty("Email"))
                    return;

                _email = value;
                MarkDirty("Email");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ContactId => FetchColumn("ContactId");

            public static QueryColumn FirstName => FetchColumn("FirstName");

            public static QueryColumn LastName => FetchColumn("LastName");

            public static QueryColumn EntityId => FetchColumn("EntityId");

            public static QueryColumn Email => FetchColumn("Email");
        }

        #endregion
    }
}