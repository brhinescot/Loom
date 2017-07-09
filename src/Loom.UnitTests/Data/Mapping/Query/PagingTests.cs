#region Using Directives

using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class PagingTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void PageSelectAll()
        {
            const string expected =
                "SELECT * FROM (" +
                "SELECT TOP (@_pi + @_ps) ROW_NUMBER() " +
                "OVER ( ORDER BY _t0.[CustomerID] ASC) AS _prr, * " +
                "FROM [Sales].[Customer] _t0" +
                ") AS _prt " +
                "WHERE (_prr BETWEEN @_pi + 1 AND (@_pi  + @_ps))";

            EntitySet<Customer> customers = Session.Customers.Page(0, 25);

            AssertCommandTextSame(expected, customers);
            AssertParamsSame(customers, 0, 25);
        }

        [Test]
        public void PageSelectColumns()
        {
            const string expected =
                "SELECT [CustomerID], [AccountNumber] FROM (" +
                "SELECT TOP (@_pi + @_ps) ROW_NUMBER() " +
                "OVER ( ORDER BY _t0.[CustomerID] ASC) AS _prr, _t0.[CustomerID], _t0.[AccountNumber] " +
                "FROM [Sales].[Customer] _t0" +
                ") AS _prt " +
                "WHERE (_prr BETWEEN @_pi + 1 AND (@_pi  + @_ps))";

            EntitySet<Customer> customers = Session.EntitySet<Customer>();
            customers.Select(Customer.Columns.CustomerId, Customer.Columns.AccountNumber);
            customers.Page(0, 25);

            AssertCommandTextSame(expected, customers);
            AssertParamsSame(customers, 0, 25);
        }

        [Test]
        public void PageSelectColumnsWhere()
        {
            const string expected =
                "SELECT [CustomerID], [AccountNumber] FROM (" +
                "SELECT TOP (@_pi + @_ps) ROW_NUMBER() " +
                "OVER ( ORDER BY _t0.[CustomerID] ASC) AS _prr, _t0.[CustomerID], _t0.[AccountNumber] " +
                "FROM [Sales].[Customer] _t0 " +
                "WHERE _t0.[CustomerID] > @_p0" +
                ") AS _prt " +
                "WHERE (_prr BETWEEN @_pi + 1 AND (@_pi  + @_ps))";

            EntitySet<Customer> customers = Session.EntitySet<Customer>();
            customers.Select(Customer.Columns.CustomerId, Customer.Columns.AccountNumber);
            customers.Where(Customer.Columns.CustomerId > 100);
            customers.Page(0, 25);

            AssertCommandTextSame(expected, customers);
            AssertParamsSame(customers, 100, 0, 25);
        }

        [Test]
        public void PageSelectColumnsOrderByWhere()
        {
            const string expected =
                "SELECT [CustomerID], [AccountNumber] FROM (" +
                "SELECT TOP (@_pi + @_ps) ROW_NUMBER() " +
                "OVER ( ORDER BY _t0.[AccountNumber] ASC, _t0.[PersonID] ASC) AS _prr, _t0.[CustomerID], _t0.[AccountNumber] " +
                "FROM [Sales].[Customer] _t0 " +
                "WHERE _t0.[CustomerID] > @_p0" +
                ") AS _prt " +
                "WHERE (_prr BETWEEN @_pi + 1 AND (@_pi  + @_ps))";

            EntitySet<Customer> customers = Session.EntitySet<Customer>();
            customers.Select(Customer.Columns.CustomerId, Customer.Columns.AccountNumber);
            customers.Where(Customer.Columns.CustomerId > 100);
            customers.OrderBy(Customer.Columns.AccountNumber, OrderByDirection.Asc);
            customers.OrderBy(Customer.Columns.PersonId, OrderByDirection.Asc);
            customers.Page(0, 25);

            AssertCommandTextSame(expected, customers);
            AssertParamsSame(customers, 100, 0, 25);
        }
    }
}