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
using OmniMount.Portal;

#endregion

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.ProductRegistration table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "ProductRegistration", "ProductRegistrationId")]
    public class ProductRegistration : DataRecord<ProductRegistration>
    {
        private int? _ageRangeId;
        private string _assistedWith;
        private string _boxSatisfaction;
        private bool? _calledForCustomerService;
        private int _contactId;
        private bool? _customerServiceHelped;
        private bool? _firstFlatPanelMounted;
        private bool? _firstFlatPanelPurchased;
        private char _gender;
        private int? _incomeRangeId;
        private string _informationImprovement;
        private string _installMethod;
        private string _instructionSatisfaction;
        private string _modelNumber;
        private string _numberFlatPanelsOwned;
        private string _overallExperience;
        private string _productCode;

        private int _productRegistrationId;
        private string _purchaseDate;
        private string _purchaseLocation;
        private string _stockCode;
        private string _templateSatisfaction;
        private string _tVBrand;
        private string _tVSize;
        private string _uPCCode;
        private bool? _visitedWebsite;
        private string _whereMounted;

        public ProductRegistration() { }
        protected ProductRegistration(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductRegistrationId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ProductRegistrationId
        {
            get => _productRegistrationId;
            set
            {
                if (value == _productRegistrationId && IsPropertyDirty("ProductRegistrationId"))
                    return;

                _productRegistrationId = value;
                MarkDirty("ProductRegistrationId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactId", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ContactId", typeof(Contact), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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
        /// </summary>
        [ActiveColumn("PurchaseDate", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 12)]
        public string PurchaseDate
        {
            get => _purchaseDate;
            set
            {
                if (value == _purchaseDate && IsPropertyDirty("PurchaseDate"))
                    return;

                _purchaseDate = value;
                MarkDirty("PurchaseDate");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ModelNumber", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 50)]
        public string ModelNumber
        {
            get => _modelNumber;
            set
            {
                if (value == _modelNumber && IsPropertyDirty("ModelNumber"))
                    return;

                _modelNumber = value;
                MarkDirty("ModelNumber");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("UPCCode", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
        public string UPCCode
        {
            get => _uPCCode;
            set
            {
                if (value == _uPCCode && IsPropertyDirty("UPCCode"))
                    return;

                _uPCCode = value;
                MarkDirty("UPCCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ProductCode", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 50)]
        public string ProductCode
        {
            get => _productCode;
            set
            {
                if (value == _productCode && IsPropertyDirty("ProductCode"))
                    return;

                _productCode = value;
                MarkDirty("ProductCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("StockCode", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 50)]
        public string StockCode
        {
            get => _stockCode;
            set
            {
                if (value == _stockCode && IsPropertyDirty("StockCode"))
                    return;

                _stockCode = value;
                MarkDirty("StockCode");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Gender", DbType.AnsiStringFixedLength, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 1)]
        public char Gender
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
        [ActiveColumn("AgeRangeId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        [ForeignColumn("AgeRangeId", typeof(AgeRange), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? AgeRangeId
        {
            get => _ageRangeId;
            set
            {
                if (value == _ageRangeId && IsPropertyDirty("AgeRangeId"))
                    return;

                _ageRangeId = value;
                MarkDirty("AgeRangeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("IncomeRangeId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 10, MaxLength = 0)]
        [ForeignColumn("IncomeRangeId", typeof(IncomeRange), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int? IncomeRangeId
        {
            get => _incomeRangeId;
            set
            {
                if (value == _incomeRangeId && IsPropertyDirty("IncomeRangeId"))
                    return;

                _incomeRangeId = value;
                MarkDirty("IncomeRangeId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TVBrand", DbType.String, ColumnProperties.Nullable, Ordinal = 11, MaxLength = 30)]
        public string TVBrand
        {
            get => _tVBrand;
            set
            {
                if (value == _tVBrand && IsPropertyDirty("TVBrand"))
                    return;

                _tVBrand = value;
                MarkDirty("TVBrand");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TVSize", DbType.String, ColumnProperties.Nullable, Ordinal = 12, MaxLength = 30)]
        public string TVSize
        {
            get => _tVSize;
            set
            {
                if (value == _tVSize && IsPropertyDirty("TVSize"))
                    return;

                _tVSize = value;
                MarkDirty("TVSize");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("PurchaseLocation", DbType.String, ColumnProperties.Nullable, Ordinal = 13, MaxLength = 30)]
        public string PurchaseLocation
        {
            get => _purchaseLocation;
            set
            {
                if (value == _purchaseLocation && IsPropertyDirty("PurchaseLocation"))
                    return;

                _purchaseLocation = value;
                MarkDirty("PurchaseLocation");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FirstFlatPanelPurchased", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 14, MaxLength = 0)]
        public bool? FirstFlatPanelPurchased
        {
            get => _firstFlatPanelPurchased;
            set
            {
                if (value == _firstFlatPanelPurchased && IsPropertyDirty("FirstFlatPanelPurchased"))
                    return;

                _firstFlatPanelPurchased = value;
                MarkDirty("FirstFlatPanelPurchased");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("NumberFlatPanelsOwned", DbType.String, ColumnProperties.Nullable, Ordinal = 15, MaxLength = 3)]
        public string NumberFlatPanelsOwned
        {
            get => _numberFlatPanelsOwned;
            set
            {
                if (value == _numberFlatPanelsOwned && IsPropertyDirty("NumberFlatPanelsOwned"))
                    return;

                _numberFlatPanelsOwned = value;
                MarkDirty("NumberFlatPanelsOwned");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("WhereMounted", DbType.String, ColumnProperties.Nullable, Ordinal = 16, MaxLength = 30)]
        public string WhereMounted
        {
            get => _whereMounted;
            set
            {
                if (value == _whereMounted && IsPropertyDirty("WhereMounted"))
                    return;

                _whereMounted = value;
                MarkDirty("WhereMounted");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("FirstFlatPanelMounted", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 17, MaxLength = 0)]
        public bool? FirstFlatPanelMounted
        {
            get => _firstFlatPanelMounted;
            set
            {
                if (value == _firstFlatPanelMounted && IsPropertyDirty("FirstFlatPanelMounted"))
                    return;

                _firstFlatPanelMounted = value;
                MarkDirty("FirstFlatPanelMounted");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("InstallMethod", DbType.String, ColumnProperties.Nullable, Ordinal = 18, MaxLength = 20)]
        public string InstallMethod
        {
            get => _installMethod;
            set
            {
                if (value == _installMethod && IsPropertyDirty("InstallMethod"))
                    return;

                _installMethod = value;
                MarkDirty("InstallMethod");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("BoxSatisfaction", DbType.String, ColumnProperties.Nullable, Ordinal = 19, MaxLength = 20)]
        public string BoxSatisfaction
        {
            get => _boxSatisfaction;
            set
            {
                if (value == _boxSatisfaction && IsPropertyDirty("BoxSatisfaction"))
                    return;

                _boxSatisfaction = value;
                MarkDirty("BoxSatisfaction");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TemplateSatisfaction", DbType.String, ColumnProperties.Nullable, Ordinal = 20, MaxLength = 20)]
        public string TemplateSatisfaction
        {
            get => _templateSatisfaction;
            set
            {
                if (value == _templateSatisfaction && IsPropertyDirty("TemplateSatisfaction"))
                    return;

                _templateSatisfaction = value;
                MarkDirty("TemplateSatisfaction");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("InstructionSatisfaction", DbType.String, ColumnProperties.Nullable, Ordinal = 21, MaxLength = 20)]
        public string InstructionSatisfaction
        {
            get => _instructionSatisfaction;
            set
            {
                if (value == _instructionSatisfaction && IsPropertyDirty("InstructionSatisfaction"))
                    return;

                _instructionSatisfaction = value;
                MarkDirty("InstructionSatisfaction");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("InformationImprovement", DbType.String, ColumnProperties.Nullable, Ordinal = 22, MaxLength = 200)]
        public string InformationImprovement
        {
            get => _informationImprovement;
            set
            {
                if (value == _informationImprovement && IsPropertyDirty("InformationImprovement"))
                    return;

                _informationImprovement = value;
                MarkDirty("InformationImprovement");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CalledForCustomerService", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 23, MaxLength = 0)]
        public bool? CalledForCustomerService
        {
            get => _calledForCustomerService;
            set
            {
                if (value == _calledForCustomerService && IsPropertyDirty("CalledForCustomerService"))
                    return;

                _calledForCustomerService = value;
                MarkDirty("CalledForCustomerService");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerServiceHelped", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 24, MaxLength = 0)]
        public bool? CustomerServiceHelped
        {
            get => _customerServiceHelped;
            set
            {
                if (value == _customerServiceHelped && IsPropertyDirty("CustomerServiceHelped"))
                    return;

                _customerServiceHelped = value;
                MarkDirty("CustomerServiceHelped");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AssistedWith", DbType.String, ColumnProperties.Nullable, Ordinal = 25, MaxLength = 30)]
        public string AssistedWith
        {
            get => _assistedWith;
            set
            {
                if (value == _assistedWith && IsPropertyDirty("AssistedWith"))
                    return;

                _assistedWith = value;
                MarkDirty("AssistedWith");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("VisitedWebsite", DbType.Boolean, ColumnProperties.Nullable, Ordinal = 26, MaxLength = 0)]
        public bool? VisitedWebsite
        {
            get => _visitedWebsite;
            set
            {
                if (value == _visitedWebsite && IsPropertyDirty("VisitedWebsite"))
                    return;

                _visitedWebsite = value;
                MarkDirty("VisitedWebsite");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("OverallExperience", DbType.String, ColumnProperties.Nullable, Ordinal = 27, MaxLength = 20)]
        public string OverallExperience
        {
            get => _overallExperience;
            set
            {
                if (value == _overallExperience && IsPropertyDirty("OverallExperience"))
                    return;

                _overallExperience = value;
                MarkDirty("OverallExperience");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductRegistrationId => FetchColumn("ProductRegistrationId");

            public static QueryColumn ContactId => FetchColumn("ContactId");

            public static QueryColumn PurchaseDate => FetchColumn("PurchaseDate");

            public static QueryColumn ModelNumber => FetchColumn("ModelNumber");

            public static QueryColumn UPCCode => FetchColumn("UPCCode");

            public static QueryColumn ProductCode => FetchColumn("ProductCode");

            public static QueryColumn StockCode => FetchColumn("StockCode");

            public static QueryColumn Gender => FetchColumn("Gender");

            public static QueryColumn AgeRangeId => FetchColumn("AgeRangeId");

            public static QueryColumn IncomeRangeId => FetchColumn("IncomeRangeId");

            public static QueryColumn TVBrand => FetchColumn("TVBrand");

            public static QueryColumn TVSize => FetchColumn("TVSize");

            public static QueryColumn PurchaseLocation => FetchColumn("PurchaseLocation");

            public static QueryColumn FirstFlatPanelPurchased => FetchColumn("FirstFlatPanelPurchased");

            public static QueryColumn NumberFlatPanelsOwned => FetchColumn("NumberFlatPanelsOwned");

            public static QueryColumn WhereMounted => FetchColumn("WhereMounted");

            public static QueryColumn FirstFlatPanelMounted => FetchColumn("FirstFlatPanelMounted");

            public static QueryColumn InstallMethod => FetchColumn("InstallMethod");

            public static QueryColumn BoxSatisfaction => FetchColumn("BoxSatisfaction");

            public static QueryColumn TemplateSatisfaction => FetchColumn("TemplateSatisfaction");

            public static QueryColumn InstructionSatisfaction => FetchColumn("InstructionSatisfaction");

            public static QueryColumn InformationImprovement => FetchColumn("InformationImprovement");

            public static QueryColumn CalledForCustomerService => FetchColumn("CalledForCustomerService");

            public static QueryColumn CustomerServiceHelped => FetchColumn("CustomerServiceHelped");

            public static QueryColumn AssistedWith => FetchColumn("AssistedWith");

            public static QueryColumn VisitedWebsite => FetchColumn("VisitedWebsite");

            public static QueryColumn OverallExperience => FetchColumn("OverallExperience");
        }

        #endregion
    }
}