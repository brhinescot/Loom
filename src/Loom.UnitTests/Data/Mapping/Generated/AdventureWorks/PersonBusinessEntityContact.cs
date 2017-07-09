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
    ///     This is an DataRecord class which wraps the Person.BusinessEntityContact table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Person", "BusinessEntityContact", "PersonID", ModifiedOnColumn = "ModifiedDate")]
    public class BusinessEntityContact : DataRecord<BusinessEntityContact>
    {
        private int _businessEntityId;
        private int _contactTypeId;
        private DateTime _modifiedDate;
        private int _personId;
        private Guid _rowguid;

        public BusinessEntityContact() { }
        protected BusinessEntityContact(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to BusinessEntity.BusinessEntityID.
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
        ///     Primary key. Foreign key to Person.BusinessEntityID.
        /// </summary>
        [ActiveColumn("PersonID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Person), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int PersonId
        {
            get => _personId;
            set
            {
                if (value == _personId && IsPropertyDirty("PersonID"))
                    return;

                _personId = value;
                MarkDirty("PersonID");
            }
        }

        /// <summary>
        ///     Primary key.  Foreign key to ContactType.ContactTypeID.
        /// </summary>
        [ActiveColumn("ContactTypeID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 3, MaxLength = 0)]
        [ForeignColumn("ContactTypeID", typeof(ContactType), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ContactTypeId
        {
            get => _contactTypeId;
            set
            {
                if (value == _contactTypeId && IsPropertyDirty("ContactTypeID"))
                    return;

                _contactTypeId = value;
                MarkDirty("ContactTypeID");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn PersonId => FetchColumn("PersonID");

            public static QueryColumn ContactTypeId => FetchColumn("ContactTypeID");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}