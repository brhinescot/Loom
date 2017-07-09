#region Using Directives

using System.Data.Common;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Providers;

#endregion

namespace Northwind
{
    public class NorthwindDataSession : DataSession
    {
        public NorthwindDataSession(string sessionProviderName) : base(sessionProviderName) { }
        public NorthwindDataSession(IActiveDataProvider provider, string connectionString) : base(provider, connectionString) { }
        public NorthwindDataSession(IActiveDataProvider provider, DbConnection connection) : base(provider, connection) { }

        public EntitySet<Categories> Categoriess => EntitySet<Categories>();

        public EntitySet<CustomerCustomerDemo> CustomerCustomerDemos => EntitySet<CustomerCustomerDemo>();

        public EntitySet<CustomerDemographics> CustomerDemographicss => EntitySet<CustomerDemographics>();

        public EntitySet<Customers> Customerss => EntitySet<Customers>();

        public EntitySet<Employees> Employeess => EntitySet<Employees>();

        public EntitySet<EmployeeTerritories> EmployeeTerritoriess => EntitySet<EmployeeTerritories>();

        public EntitySet<OrderDetails> OrderDetailss => EntitySet<OrderDetails>();

        public EntitySet<Orders> Orderss => EntitySet<Orders>();

        public EntitySet<Products> Productss => EntitySet<Products>();

        public EntitySet<Region> Regions => EntitySet<Region>();

        public EntitySet<Shippers> Shipperss => EntitySet<Shippers>();

        public EntitySet<Suppliers> Supplierss => EntitySet<Suppliers>();

        public EntitySet<Territories> Territoriess => EntitySet<Territories>();

        public EntitySet<AlphabeticalListOfProducts> AlphabeticalListOfProductss => EntitySet<AlphabeticalListOfProducts>();

        public EntitySet<CategorySalesFor1997> CategorySalesFor1997s => EntitySet<CategorySalesFor1997>();

        public EntitySet<CurrentProductList> CurrentProductLists => EntitySet<CurrentProductList>();

        public EntitySet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCitys => EntitySet<CustomerAndSuppliersByCity>();

        public EntitySet<Invoices> Invoicess => EntitySet<Invoices>();

        public EntitySet<OrderDetailsExtended> OrderDetailsExtendeds => EntitySet<OrderDetailsExtended>();

        public EntitySet<OrderSubtotals> OrderSubtotalss => EntitySet<OrderSubtotals>();

        public EntitySet<OrdersQry> OrdersQrys => EntitySet<OrdersQry>();

        public EntitySet<ProductSalesFor1997> ProductSalesFor1997s => EntitySet<ProductSalesFor1997>();

        public EntitySet<ProductsAboveAveragePrice> ProductsAboveAveragePrices => EntitySet<ProductsAboveAveragePrice>();

        public EntitySet<ProductsByCategory> ProductsByCategorys => EntitySet<ProductsByCategory>();

        public EntitySet<QuarterlyOrders> QuarterlyOrderss => EntitySet<QuarterlyOrders>();

        public EntitySet<SalesByCategory> SalesByCategorys => EntitySet<SalesByCategory>();

        public EntitySet<SalesTotalsByAmount> SalesTotalsByAmounts => EntitySet<SalesTotalsByAmount>();

        public EntitySet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters => EntitySet<SummaryOfSalesByQuarter>();

        public EntitySet<SummaryOfSalesByYear> SummaryOfSalesByYears => EntitySet<SummaryOfSalesByYear>();
    }
}