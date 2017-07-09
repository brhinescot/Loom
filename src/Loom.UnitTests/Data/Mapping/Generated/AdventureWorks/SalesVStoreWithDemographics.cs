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
    ///     This is an DataRecord class which wraps the Sales.vStoreWithDemographics table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "vStoreWithDemographics", ReadOnly = true)]
    public class VStoreWithDemographics : DataRecord<VStoreWithDemographics>
    {
        private decimal? _annualRevenue;
        private decimal? _annualSales;
        private string _bankName;
        private string _brands;
        private int _businessEntityId;
        private string _businessType;
        private string _internet;
        private string _name;
        private int? _numberEmployees;

        private string _specialty;
        private int? _squareFeet;
        private int? _yearOpened;

        public VStoreWithDemographics() { }
        protected VStoreWithDemographics(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Specialty", DbType.String, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 50)]
        public string Specialty
        {
            get => _specialty;
            set
            {
                if (value == _specialty && IsPropertyDirty("Specialty"))
                    return;

                _specialty = value;
                MarkDirty("Specialty");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Internet", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 30)]
        public string Internet
        {
            get => _internet;
            set
            {
                if (value == _internet && IsPropertyDirty("Internet"))
                    return;

                _internet = value;
                MarkDirty("Internet");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.None, Ordinal = 1, MaxLength = 0)]
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
        /// </summary>
        [ActiveColumn("YearOpened", DbType.Int32, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
        public int? YearOpened
        {
            get => _yearOpened;
            set
            {
                if (value == _yearOpened && IsPropertyDirty("YearOpened"))
                    return;

                _yearOpened = value;
                MarkDirty("YearOpened");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AnnualRevenue", DbType.Currency, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public decimal? AnnualRevenue
        {
            get => _annualRevenue;
            set
            {
                if (value == _annualRevenue && IsPropertyDirty("AnnualRevenue"))
                    return;

                _annualRevenue = value;
                MarkDirty("AnnualRevenue");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Brands", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 30)]
        public string Brands
        {
            get => _brands;
            set
            {
                if (value == _brands && IsPropertyDirty("Brands"))
                    return;

                _brands = value;
                MarkDirty("Brands");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BankName", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
        public string BankName
        {
            get => _bankName;
            set
            {
                if (value == _bankName && IsPropertyDirty("BankName"))
                    return;

                _bankName = value;
                MarkDirty("BankName");
            }
        }

        /// <summary>
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
        /// </summary>
        [ActiveColumn("AnnualSales", DbType.Currency, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public decimal? AnnualSales
        {
            get => _annualSales;
            set
            {
                if (value == _annualSales && IsPropertyDirty("AnnualSales"))
                    return;

                _annualSales = value;
                MarkDirty("AnnualSales");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("SquareFeet", DbType.Int32, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        public int? SquareFeet
        {
            get => _squareFeet;
            set
            {
                if (value == _squareFeet && IsPropertyDirty("SquareFeet"))
                    return;

                _squareFeet = value;
                MarkDirty("SquareFeet");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BusinessType", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 5)]
        public string BusinessType
        {
            get => _businessType;
            set
            {
                if (value == _businessType && IsPropertyDirty("BusinessType"))
                    return;

                _businessType = value;
                MarkDirty("BusinessType");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NumberEmployees", DbType.Int32, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 0)]
        public int? NumberEmployees
        {
            get => _numberEmployees;
            set
            {
                if (value == _numberEmployees && IsPropertyDirty("NumberEmployees"))
                    return;

                _numberEmployees = value;
                MarkDirty("NumberEmployees");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Specialty => FetchColumn("Specialty");

            public static QueryColumn Internet => FetchColumn("Internet");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn YearOpened => FetchColumn("YearOpened");

            public static QueryColumn AnnualRevenue => FetchColumn("AnnualRevenue");

            public static QueryColumn Brands => FetchColumn("Brands");

            public static QueryColumn BankName => FetchColumn("BankName");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn AnnualSales => FetchColumn("AnnualSales");

            public static QueryColumn SquareFeet => FetchColumn("SquareFeet");

            public static QueryColumn BusinessType => FetchColumn("BusinessType");

            public static QueryColumn NumberEmployees => FetchColumn("NumberEmployees");
        }

        #endregion
    }
}