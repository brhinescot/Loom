#region Using Directives

using System.Data.Common;
using AdventureWorks.HumanResources;
using AdventureWorks.Person;
using AdventureWorks.Production;
using AdventureWorks.Purchasing;
using AdventureWorks.Sales;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Providers;

#endregion

namespace AdventureWorks
{
    public class AdventureWorksDataSession : DataSession
    {
        public AdventureWorksDataSession(string sessionProviderName = null) : base(sessionProviderName) { }
        public AdventureWorksDataSession(IActiveDataProvider provider, string connectionString) : base(provider, connectionString) { }
        public AdventureWorksDataSession(IActiveDataProvider provider, DbConnection connection) : base(provider, connection) { }

        public EntitySet<AWBuildVersion> AWBuildVersions => EntitySet<AWBuildVersion>();

        public EntitySet<DatabaseLog> DatabaseLogs => EntitySet<DatabaseLog>();

        public EntitySet<ErrorLog> ErrorLogs => EntitySet<ErrorLog>();

        public EntitySet<Department> Departments => EntitySet<Department>();

        public EntitySet<Employee> Employees => EntitySet<Employee>();

        public EntitySet<EmployeeDepartmentHistory> EmployeeDepartmentHistorys => EntitySet<EmployeeDepartmentHistory>();

        public EntitySet<EmployeePayHistory> EmployeePayHistorys => EntitySet<EmployeePayHistory>();

        public EntitySet<JobCandidate> JobCandidates => EntitySet<JobCandidate>();

        public EntitySet<Shift> Shifts => EntitySet<Shift>();

        public EntitySet<Address> Addresss => EntitySet<Address>();

        public EntitySet<AddressType> AddressTypes => EntitySet<AddressType>();

        public EntitySet<BusinessEntity> BusinessEntitys => EntitySet<BusinessEntity>();

        public EntitySet<BusinessEntityAddress> BusinessEntityAddresss => EntitySet<BusinessEntityAddress>();

        public EntitySet<BusinessEntityContact> BusinessEntityContacts => EntitySet<BusinessEntityContact>();

        public EntitySet<ContactType> ContactTypes => EntitySet<ContactType>();

        public EntitySet<CountryRegion> CountryRegions => EntitySet<CountryRegion>();

        public EntitySet<EmailAddress> EmailAddresss => EntitySet<EmailAddress>();

        public EntitySet<Password> Passwords => EntitySet<Password>();

        public EntitySet<Person.Person> Persons => EntitySet<Person.Person>();

        public EntitySet<PersonPhone> PersonPhones => EntitySet<PersonPhone>();

        public EntitySet<PhoneNumberType> PhoneNumberTypes => EntitySet<PhoneNumberType>();

        public EntitySet<StateProvince> StateProvinces => EntitySet<StateProvince>();

        public EntitySet<BillOfMaterials> BillOfMaterialss => EntitySet<BillOfMaterials>();

        public EntitySet<Culture> Cultures => EntitySet<Culture>();

        public EntitySet<Document> Documents => EntitySet<Document>();

        public EntitySet<Illustration> Illustrations => EntitySet<Illustration>();

        public EntitySet<Location> Locations => EntitySet<Location>();

        public EntitySet<Product> Products => EntitySet<Product>();

        public EntitySet<ProductCategory> ProductCategorys => EntitySet<ProductCategory>();

        public EntitySet<ProductCostHistory> ProductCostHistorys => EntitySet<ProductCostHistory>();

        public EntitySet<ProductDescription> ProductDescriptions => EntitySet<ProductDescription>();

        public EntitySet<ProductDocument> ProductDocuments => EntitySet<ProductDocument>();

        public EntitySet<ProductInventory> ProductInventorys => EntitySet<ProductInventory>();

        public EntitySet<ProductListPriceHistory> ProductListPriceHistorys => EntitySet<ProductListPriceHistory>();

        public EntitySet<ProductModel> ProductModels => EntitySet<ProductModel>();

        public EntitySet<ProductModelIllustration> ProductModelIllustrations => EntitySet<ProductModelIllustration>();

        public EntitySet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures => EntitySet<ProductModelProductDescriptionCulture>();

        public EntitySet<ProductPhoto> ProductPhotos => EntitySet<ProductPhoto>();

        public EntitySet<ProductProductPhoto> ProductProductPhotos => EntitySet<ProductProductPhoto>();

        public EntitySet<ProductReview> ProductReviews => EntitySet<ProductReview>();

        public EntitySet<ProductSubcategory> ProductSubcategorys => EntitySet<ProductSubcategory>();

        public EntitySet<TransactionHistory> TransactionHistorys => EntitySet<TransactionHistory>();

        public EntitySet<TransactionHistoryArchive> TransactionHistoryArchives => EntitySet<TransactionHistoryArchive>();

        public EntitySet<UnitMeasure> UnitMeasures => EntitySet<UnitMeasure>();

        public EntitySet<WorkOrder> WorkOrders => EntitySet<WorkOrder>();

        public EntitySet<WorkOrderRouting> WorkOrderRoutings => EntitySet<WorkOrderRouting>();

        public EntitySet<ProductVendor> ProductVendors => EntitySet<ProductVendor>();

        public EntitySet<PurchaseOrderDetail> PurchaseOrderDetails => EntitySet<PurchaseOrderDetail>();

        public EntitySet<PurchaseOrderHeader> PurchaseOrderHeaders => EntitySet<PurchaseOrderHeader>();

        public EntitySet<Vendor> Vendors => EntitySet<Vendor>();

        public EntitySet<CountryRegionCurrency> CountryRegionCurrencys => EntitySet<CountryRegionCurrency>();

        public EntitySet<CreditCard> CreditCards => EntitySet<CreditCard>();

        public EntitySet<Currency> Currencys => EntitySet<Currency>();

        public EntitySet<CurrencyRate> CurrencyRates => EntitySet<CurrencyRate>();

        public EntitySet<Customer> Customers => EntitySet<Customer>();

        public EntitySet<PersonCreditCard> PersonCreditCards => EntitySet<PersonCreditCard>();

        public EntitySet<SalesOrderDetail> SalesOrderDetails => EntitySet<SalesOrderDetail>();

        public EntitySet<SalesOrderHeader> SalesOrderHeaders => EntitySet<SalesOrderHeader>();

        public EntitySet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons => EntitySet<SalesOrderHeaderSalesReason>();

        public EntitySet<SalesPerson> SalesPersons => EntitySet<SalesPerson>();

        public EntitySet<SalesPersonQuotaHistory> SalesPersonQuotaHistorys => EntitySet<SalesPersonQuotaHistory>();

        public EntitySet<SalesReason> SalesReasons => EntitySet<SalesReason>();

        public EntitySet<SalesTaxRate> SalesTaxRates => EntitySet<SalesTaxRate>();

        public EntitySet<SalesTerritory> SalesTerritorys => EntitySet<SalesTerritory>();

        public EntitySet<SalesTerritoryHistory> SalesTerritoryHistorys => EntitySet<SalesTerritoryHistory>();

        public EntitySet<ShoppingCartItem> ShoppingCartItems => EntitySet<ShoppingCartItem>();

        public EntitySet<SpecialOffer> SpecialOffers => EntitySet<SpecialOffer>();

        public EntitySet<SpecialOfferProduct> SpecialOfferProducts => EntitySet<SpecialOfferProduct>();

        public EntitySet<Store> Stores => EntitySet<Store>();

        public EntitySet<VEmployee> VEmployees => EntitySet<VEmployee>();

        public EntitySet<VEmployeeDepartment> VEmployeeDepartments => EntitySet<VEmployeeDepartment>();

        public EntitySet<VEmployeeDepartmentHistory> VEmployeeDepartmentHistorys => EntitySet<VEmployeeDepartmentHistory>();

        public EntitySet<VJobCandidate> VJobCandidates => EntitySet<VJobCandidate>();

        public EntitySet<VJobCandidateEducation> VJobCandidateEducations => EntitySet<VJobCandidateEducation>();

        public EntitySet<VJobCandidateEmployment> VJobCandidateEmployments => EntitySet<VJobCandidateEmployment>();

        public EntitySet<VAdditionalContactInfo> VAdditionalContactInfos => EntitySet<VAdditionalContactInfo>();

        public EntitySet<VStateProvinceCountryRegion> VStateProvinceCountryRegions => EntitySet<VStateProvinceCountryRegion>();

        public EntitySet<VProductAndDescription> VProductAndDescriptions => EntitySet<VProductAndDescription>();

        public EntitySet<VProductModelCatalogDescription> VProductModelCatalogDescriptions => EntitySet<VProductModelCatalogDescription>();

        public EntitySet<VProductModelInstructions> VProductModelInstructionss => EntitySet<VProductModelInstructions>();

        public EntitySet<VVendorWithAddresses> VVendorWithAddressess => EntitySet<VVendorWithAddresses>();

        public EntitySet<VVendorWithContacts> VVendorWithContactss => EntitySet<VVendorWithContacts>();

        public EntitySet<VIndividualCustomer> VIndividualCustomers => EntitySet<VIndividualCustomer>();

        public EntitySet<VPersonDemographics> VPersonDemographicss => EntitySet<VPersonDemographics>();

        public EntitySet<VSalesPerson> VSalesPersons => EntitySet<VSalesPerson>();

        public EntitySet<VSalesPersonSalesByFiscalYears> VSalesPersonSalesByFiscalYearss => EntitySet<VSalesPersonSalesByFiscalYears>();

        public EntitySet<VStoreWithAddresses> VStoreWithAddressess => EntitySet<VStoreWithAddresses>();

        public EntitySet<VStoreWithContacts> VStoreWithContactss => EntitySet<VStoreWithContacts>();

        public EntitySet<VStoreWithDemographics> VStoreWithDemographicss => EntitySet<VStoreWithDemographics>();
    }
}