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
    ///     This is an DataRecord class which wraps the Sales.PersonCreditCard table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "PersonCreditCard", "CreditCardID", ModifiedOnColumn = "ModifiedDate")]
    public class PersonCreditCard : DataRecord<PersonCreditCard>
    {
        private int _businessEntityId;
        private int _creditCardId;
        private DateTime _modifiedDate;

        public PersonCreditCard() { }
        protected PersonCreditCard(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Business entity identification number. Foreign key to Person.BusinessEntityID.
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(Person.Person), ColumnProperties = ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        ///     Credit card identification number. Foreign key to CreditCard.CreditCardID.
        /// </summary>
        [ActiveColumn("CreditCardID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("CreditCardID", typeof(CreditCard), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int CreditCardId
        {
            get => _creditCardId;
            set
            {
                if (value == _creditCardId && IsPropertyDirty("CreditCardID"))
                    return;

                _creditCardId = value;
                MarkDirty("CreditCardID");
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
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn CreditCardId => FetchColumn("CreditCardID");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}