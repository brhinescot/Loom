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
    ///     This is an DataRecord class which wraps the Sales.Currency table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "Currency", "CurrencyCode", ModifiedOnColumn = "ModifiedDate")]
    public class Currency : DataRecord<Currency>
    {
        private string _currencyCode;
        private DateTime _modifiedDate;
        private string _name;

        public Currency() { }
        protected Currency(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     The ISO code for the Currency.
        /// </summary>
        [ActiveColumn("CurrencyCode", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3)]
        public string CurrencyCode
        {
            get => _currencyCode;
            set
            {
                if (value == _currencyCode && IsPropertyDirty("CurrencyCode"))
                    return;

                _currencyCode = value;
                MarkDirty("CurrencyCode");
            }
        }

        /// <summary>
        ///     Currency name.
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
            public static QueryColumn CurrencyCode => FetchColumn("CurrencyCode");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}