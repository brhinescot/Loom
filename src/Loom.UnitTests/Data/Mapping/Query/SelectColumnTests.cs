#region Using Directives

using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class SelectColumnTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void SelectAllColumns()
        {
            const string expected = "SELECT * FROM [Sales].[Customer]";

            EntitySet<Customer> entitySet = Session.Customers;

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectTwoColumns()
        {
            const string expected = "SELECT _t0.[AccountNumber], _t0.[CustomerID] FROM [Sales].[Customer] _t0";

            EntitySet<Customer> query = Session.Customers.Select(Customer.Columns.AccountNumber, Customer.Columns.CustomerId);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }

        [Test]
        public void SelectTwoColumnsWithAlias()
        {
            const string expected = "SELECT _t0.[AccountNumber] AS AcctNum, _t0.[CustomerID] AS CustNum FROM [Sales].[Customer] _t0";

            EntitySet<Customer> query = Session.Customers.Select(Customer.Columns.AccountNumber.As("AcctNum"), Customer.Columns.CustomerId.As("CustNum"));

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }
    }
}