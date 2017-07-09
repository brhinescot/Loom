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
    ///     This is an DataRecord class which wraps the Sales.vPersonDemographics table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "vPersonDemographics", ReadOnly = true)]
    public class VPersonDemographics : DataRecord<VPersonDemographics>
    {
        private DateTime? _birthDate;
        private int _businessEntityId;
        private DateTime? _dateFirstPurchase;
        private string _education;
        private string _gender;
        private bool? _homeOwnerFlag;

        private string _maritalStatus;
        private int? _numberCarsOwned;
        private int? _numberChildrenAtHome;
        private string _occupation;
        private int? _totalChildren;
        private decimal? _totalPurchaseYTD;
        private string _yearlyIncome;

        public VPersonDemographics() { }
        protected VPersonDemographics(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("MaritalStatus", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 1)]
        public string MaritalStatus
        {
            get => _maritalStatus;
            set
            {
                if (value == _maritalStatus && IsPropertyDirty("MaritalStatus"))
                    return;

                _maritalStatus = value;
                MarkDirty("MaritalStatus");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TotalPurchaseYTD", DbType.Currency, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public decimal? TotalPurchaseYTD
        {
            get => _totalPurchaseYTD;
            set
            {
                if (value == _totalPurchaseYTD && IsPropertyDirty("TotalPurchaseYTD"))
                    return;

                _totalPurchaseYTD = value;
                MarkDirty("TotalPurchaseYTD");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Occupation", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 30)]
        public string Occupation
        {
            get => _occupation;
            set
            {
                if (value == _occupation && IsPropertyDirty("Occupation"))
                    return;

                _occupation = value;
                MarkDirty("Occupation");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TotalChildren", DbType.Int32, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public int? TotalChildren
        {
            get => _totalChildren;
            set
            {
                if (value == _totalChildren && IsPropertyDirty("TotalChildren"))
                    return;

                _totalChildren = value;
                MarkDirty("TotalChildren");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BirthDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (value == _birthDate && IsPropertyDirty("BirthDate"))
                    return;

                _birthDate = value;
                MarkDirty("BirthDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("HomeOwnerFlag", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 0)]
        public bool? HomeOwnerFlag
        {
            get => _homeOwnerFlag;
            set
            {
                if (value == _homeOwnerFlag && IsPropertyDirty("HomeOwnerFlag"))
                    return;

                _homeOwnerFlag = value;
                MarkDirty("HomeOwnerFlag");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("YearlyIncome", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 30)]
        public string YearlyIncome
        {
            get => _yearlyIncome;
            set
            {
                if (value == _yearlyIncome && IsPropertyDirty("YearlyIncome"))
                    return;

                _yearlyIncome = value;
                MarkDirty("YearlyIncome");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NumberChildrenAtHome", DbType.Int32, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        public int? NumberChildrenAtHome
        {
            get => _numberChildrenAtHome;
            set
            {
                if (value == _numberChildrenAtHome && IsPropertyDirty("NumberChildrenAtHome"))
                    return;

                _numberChildrenAtHome = value;
                MarkDirty("NumberChildrenAtHome");
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
        [ActiveColumn("DateFirstPurchase", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public DateTime? DateFirstPurchase
        {
            get => _dateFirstPurchase;
            set
            {
                if (value == _dateFirstPurchase && IsPropertyDirty("DateFirstPurchase"))
                    return;

                _dateFirstPurchase = value;
                MarkDirty("DateFirstPurchase");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Education", DbType.String, ColumnProperties.Nullable, Ordinal = 10, MaxLength = 30)]
        public string Education
        {
            get => _education;
            set
            {
                if (value == _education && IsPropertyDirty("Education"))
                    return;

                _education = value;
                MarkDirty("Education");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Gender", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 1)]
        public string Gender
        {
            get => _gender;
            set
            {
                if (value == _gender && IsPropertyDirty("Gender"))
                    return;

                _gender = value;
                MarkDirty("Gender");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NumberCarsOwned", DbType.Int32, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 0)]
        public int? NumberCarsOwned
        {
            get => _numberCarsOwned;
            set
            {
                if (value == _numberCarsOwned && IsPropertyDirty("NumberCarsOwned"))
                    return;

                _numberCarsOwned = value;
                MarkDirty("NumberCarsOwned");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn MaritalStatus => FetchColumn("MaritalStatus");

            public static QueryColumn TotalPurchaseYTD => FetchColumn("TotalPurchaseYTD");

            public static QueryColumn Occupation => FetchColumn("Occupation");

            public static QueryColumn TotalChildren => FetchColumn("TotalChildren");

            public static QueryColumn BirthDate => FetchColumn("BirthDate");

            public static QueryColumn HomeOwnerFlag => FetchColumn("HomeOwnerFlag");

            public static QueryColumn YearlyIncome => FetchColumn("YearlyIncome");

            public static QueryColumn NumberChildrenAtHome => FetchColumn("NumberChildrenAtHome");

            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn DateFirstPurchase => FetchColumn("DateFirstPurchase");

            public static QueryColumn Education => FetchColumn("Education");

            public static QueryColumn Gender => FetchColumn("Gender");

            public static QueryColumn NumberCarsOwned => FetchColumn("NumberCarsOwned");
        }

        #endregion
    }
}