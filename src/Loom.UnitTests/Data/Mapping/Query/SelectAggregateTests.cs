#region Using Directives

using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class SelectAggregateTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void SelectWithOneFunction()
        {
            const string expected = "SELECT MAX(_t0.[AccountNumber]), _t0.[CustomerID] "
                                    + "FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[CustomerID]";

            EntitySet<Customer> query = Session.Customers
                .Select(Customer.Columns.AccountNumber.Max(), Customer.Columns.CustomerId)
                .GroupBy(Customer.Columns.CustomerId);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }

        [Test]
        public void SelectWithNestedFunctions()
        {
            const string expected = "SELECT MAX(COUNT(_t0.[AccountNumber])), _t0.[CustomerID] "
                                    + "FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[CustomerID]";

            EntitySet<Customer> query = Session.Customers
                .Select(Customer.Columns.AccountNumber.Count().Max(), Customer.Columns.CustomerId)
                .GroupBy(Customer.Columns.CustomerId);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }
    }
}