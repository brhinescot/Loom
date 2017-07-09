#region Using Directives

using System.Collections.Generic;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Schema;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class OrderByTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void SelectAllOrderByOne()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "ORDER BY _t0.[CustomerID] ASC";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.OrderBy(Customer.Columns.CustomerId);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectAllOrderByTwoSingleMethod()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "ORDER BY _t0.[CustomerID] ASC, _t0.[AccountNumber] ASC";

            EntitySet<Customer> query = Session.EntitySet<Customer>();
            query.OrderBy(Customer.Columns.CustomerId, Customer.Columns.AccountNumber);

            AssertCommandTextSame(expected, query);
            AssertParamCountSame(query, 0);
        }

        [Test]
        public void SelectAllOrderByThreeSingleMethod()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "ORDER BY _t0.[CustomerID] ASC, _t0.[AccountNumber] ASC, _t0.[PersonID] ASC";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.OrderBy(Customer.Columns.CustomerId, Customer.Columns.AccountNumber, Customer.Columns.PersonId);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectAllOrderByFourParams()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "ORDER BY _t0.[CustomerID] ASC, _t0.[AccountNumber] ASC, _t0.[PersonID] ASC, _t0.[TerritoryID] ASC";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.OrderBy(Customer.Columns.CustomerId, Customer.Columns.AccountNumber, Customer.Columns.PersonId, Customer.Columns.TerritoryId);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectAllOrderByFourList()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 " +
                                    "ORDER BY _t0.[CustomerID] ASC, _t0.[AccountNumber] ASC, _t0.[PersonID] ASC, _t0.[TerritoryID] ASC";

            List<IQueryableColumn> columns = new List<IQueryableColumn>();
            columns.Add(Customer.Columns.CustomerId);
            columns.Add(Customer.Columns.AccountNumber);
            columns.Add(Customer.Columns.PersonId);
            columns.Add(Customer.Columns.TerritoryId);

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.OrderBy(columns);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectOneColumnOrderBy()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] = @_p0 "
                                    + "ORDER BY _t0.[AccountNumber] DESC";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.AccountNumber == "1234");
            entitySet.OrderBy(Customer.Columns.AccountNumber, OrderByDirection.Desc);

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234");
        }

        [Test]
        public void SelectOneColumnOrderByTwo()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] = @_p0 "
                                    + "ORDER BY _t0.[AccountNumber] DESC, _t0.[ModifiedDate] ASC";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.AccountNumber == "1234");
            entitySet.OrderBy(Customer.Columns.AccountNumber, OrderByDirection.Desc);
            entitySet.OrderBy(Customer.Columns.ModifiedDate, OrderByDirection.Asc);

            AssertCommandTextSame(expected, entitySet);
            AssertParamsSame(entitySet, "1234");
        }
    }
}