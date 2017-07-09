#region Using Directives

using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class SelectDistinctTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void SelectDistinctAllColumns()
        {
            const string expected = "SELECT DISTINCT * FROM [Sales].[Customer]";

            EntitySet<Customer> entitySet = Session.Customers.Distinct();

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectDistinctTwoColumns()
        {
            const string expected = "SELECT DISTINCT _t0.[AccountNumber], _t0.[CustomerID] FROM [Sales].[Customer] _t0";

            EntitySet<Customer> query = Session.Customers.Select(Customer.Columns.AccountNumber, Customer.Columns.CustomerId).Distinct();

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }

        [Test]
        public void SelectDistinctTwoColumnsWithAlias()
        {
            const string expected = "SELECT DISTINCT _t0.[AccountNumber] AS AcctNum, _t0.[CustomerID] AS CustNum FROM [Sales].[Customer] _t0";

            EntitySet<Customer> query = Session.Customers
                .Select(Customer.Columns.AccountNumber.As("AcctNum"), Customer.Columns.CustomerId.As("CustNum"))
                .Distinct();

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }

        [Test]
        public void WhereOneColumnEqualDistinct()
        {
            const string expected = "SELECT DISTINCT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] = @_p0";

            EntitySet<Customer> entitySet = Session.Customers.Distinct();
            entitySet.Where(Customer.Columns.AccountNumber == "1234");

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234");
        }

        [Test]
        public void WhereOneColumnGreaterDistinct()
        {
            const string expected = "SELECT DISTINCT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] > @_p0";

            EntitySet<Customer> entitySet = Session.Customers.Distinct().Where(Customer.Columns.AccountNumber > "1234").End();

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234");
        }
    }
}