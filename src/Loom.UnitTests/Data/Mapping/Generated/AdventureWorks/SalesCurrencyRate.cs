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
    ///     This is an DataRecord class which wraps the Sales.CurrencyRate table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "CurrencyRate", "CurrencyRateID", ModifiedOnColumn = "ModifiedDate")]
    public class CurrencyRate : DataRecord<CurrencyRate>
    {
        private decimal _averageRate;
        private DateTime _currencyRateDate;

        private int _currencyRateId;
        private decimal _endOfDayRate;
        private string _fromCurrencyCode;
        private DateTime _modifiedDate;
        private string _toCurrencyCode;

        public CurrencyRate() { }
        protected CurrencyRate(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for CurrencyRate records.
        /// </summary>
        [ActiveColumn("CurrencyRateID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int CurrencyRateId
        {
            get => _currencyRateId;
            set
            {
                if (value == _currencyRateId && IsPropertyDirty("CurrencyRateID"))
                    return;

                _currencyRateId = value;
                MarkDirty("CurrencyRateID");
            }
        }

        /// <summary>
        ///     Date and time the exchange rate was obtained.
        /// </summary>
        [ActiveColumn("CurrencyRateDate", DbType.DateTime, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
        public DateTime CurrencyRateDate
        {
            get => _currencyRateDate;
            set
            {
                if (value == _currencyRateDate && IsPropertyDirty("CurrencyRateDate"))
                    return;

                _currencyRateDate = value;
                MarkDirty("CurrencyRateDate");
            }
        }

        /// <summary>
        ///     Exchange rate was converted from this currency code.
        /// </summary>
        [ActiveColumn("FromCurrencyCode", DbType.String, ColumnProperties.ForeignKey, Ordinal = 3, MaxLength = 3)]
        [ForeignColumn("CurrencyCode", typeof(Currency), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string FromCurrencyCode
        {
            get => _fromCurrencyCode;
            set
            {
                if (value == _fromCurrencyCode && IsPropertyDirty("FromCurrencyCode"))
                    return;

                _fromCurrencyCode = value;
                MarkDirty("FromCurrencyCode");
            }
        }

        /// <summary>
        ///     Exchange rate was converted to this currency code.
        /// </summary>
        [ActiveColumn("ToCurrencyCode", DbType.String, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 3)]
        [ForeignColumn("CurrencyCode", typeof(Currency), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 3, DbType = DbType.String)]
        public string ToCurrencyCode
        {
            get => _toCurrencyCode;
            set
            {
                if (value == _toCurrencyCode && IsPropertyDirty("ToCurrencyCode"))
                    return;

                _toCurrencyCode = value;
                MarkDirty("ToCurrencyCode");
            }
        }

        /// <summary>
        ///     Average exchange rate for the day.
        /// </summary>
        [ActiveColumn("AverageRate", DbType.Currency, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public decimal AverageRate
        {
            get => _averageRate;
            set
            {
                if (value == _averageRate && IsPropertyDirty("AverageRate"))
                    return;

                _averageRate = value;
                MarkDirty("AverageRate");
            }
        }

        /// <summary>
        ///     Final exchange rate for the day.
        /// </summary>
        [ActiveColumn("EndOfDayRate", DbType.Currency, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
        public decimal EndOfDayRate
        {
            get => _endOfDayRate;
            set
            {
                if (value == _endOfDayRate && IsPropertyDirty("EndOfDayRate"))
                    return;

                _endOfDayRate = value;
                MarkDirty("EndOfDayRate");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn CurrencyRateId => FetchColumn("CurrencyRateID");

            public static QueryColumn CurrencyRateDate => FetchColumn("CurrencyRateDate");

            public static QueryColumn FromCurrencyCode => FetchColumn("FromCurrencyCode");

            public static QueryColumn ToCurrencyCode => FetchColumn("ToCurrencyCode");

            public static QueryColumn AverageRate => FetchColumn("AverageRate");

            public static QueryColumn EndOfDayRate => FetchColumn("EndOfDayRate");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}