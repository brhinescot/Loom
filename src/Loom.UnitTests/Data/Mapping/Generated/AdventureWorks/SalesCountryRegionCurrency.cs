#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Person;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.CountryRegionCurrency table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "CountryRegionCurrency", "CurrencyCode", ModifiedOnColumn = "ModifiedDate")]
    public class CountryRegionCurrency : DataRecord<CountryRegionCurrency>
    {
        private string _countryRegionCode;
        private string _currencyCode;
        private DateTime _modifiedDate;

        public CountryRegionCurrency() { }
        protected CountryRegionCurrency(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode.
        /// </summary>
        [ActiveColumn("CountryRegionCode", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3)]
        [ForeignColumn("CountryRegionCode", typeof(CountryRegion), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string CountryRegionCode
        {
            get => _countryRegionCode;
            set
            {
                if (value == _countryRegionCode && IsPropertyDirty("CountryRegionCode"))
                    return;

                _countryRegionCode = value;
                MarkDirty("CountryRegionCode");
            }
        }

        /// <summary>
        ///     ISO standard currency code. Foreign key to Currency.CurrencyCode.
        /// </summary>
        [ActiveColumn("CurrencyCode", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 3)]
        [ForeignColumn("CurrencyCode", typeof(Currency), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
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
            public static QueryColumn CountryRegionCode => FetchColumn("CountryRegionCode");

            public static QueryColumn CurrencyCode => FetchColumn("CurrencyCode");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}