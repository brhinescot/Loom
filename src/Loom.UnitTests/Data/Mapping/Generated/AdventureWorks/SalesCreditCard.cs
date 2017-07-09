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
    ///     This is an DataRecord class which wraps the Sales.CreditCard table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "CreditCard", "CreditCardID", ModifiedOnColumn = "ModifiedDate")]
    public class CreditCard : DataRecord<CreditCard>
    {
        private string _cardNumber;
        private string _cardType;

        private int _creditCardId;
        private short _expMonth;
        private short _expYear;
        private DateTime _modifiedDate;

        public CreditCard() { }
        protected CreditCard(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for CreditCard records.
        /// </summary>
        [ActiveColumn("CreditCardID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Credit card name.
        /// </summary>
        [ActiveColumn("CardType", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string CardType
        {
            get => _cardType;
            set
            {
                if (value == _cardType && IsPropertyDirty("CardType"))
                    return;

                _cardType = value;
                MarkDirty("CardType");
            }
        }

        /// <summary>
        ///     Credit card number.
        /// </summary>
        [ActiveColumn("CardNumber", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 25)]
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                if (value == _cardNumber && IsPropertyDirty("CardNumber"))
                    return;

                _cardNumber = value;
                MarkDirty("CardNumber");
            }
        }

        /// <summary>
        ///     Credit card expiration month.
        /// </summary>
        [ActiveColumn("ExpMonth", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short ExpMonth
        {
            get => _expMonth;
            set
            {
                if (value == _expMonth && IsPropertyDirty("ExpMonth"))
                    return;

                _expMonth = value;
                MarkDirty("ExpMonth");
            }
        }

        /// <summary>
        ///     Credit card expiration year.
        /// </summary>
        [ActiveColumn("ExpYear", DbType.Int16, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public short ExpYear
        {
            get => _expYear;
            set
            {
                if (value == _expYear && IsPropertyDirty("ExpYear"))
                    return;

                _expYear = value;
                MarkDirty("ExpYear");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn CreditCardId => FetchColumn("CreditCardID");

            public static QueryColumn CardType => FetchColumn("CardType");

            public static QueryColumn CardNumber => FetchColumn("CardNumber");

            public static QueryColumn ExpMonth => FetchColumn("ExpMonth");

            public static QueryColumn ExpYear => FetchColumn("ExpYear");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}