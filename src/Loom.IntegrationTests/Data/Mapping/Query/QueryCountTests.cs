#region Using Directives

using System.Collections.Generic;
using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class QueryCountTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void CountThenQuery()
        {
            const string expected = "SELECT _t0.[AccountNumber] FROM [Sales].[Customer] _t0 WHERE _t0.[CustomerID] > @_p0";

            EntitySet<Customer> query = Session.EntitySet<Customer>();
            query.Select(Customer.Columns.AccountNumber);
            query.Where(Customer.Columns.CustomerId > 100);

            int count = query.Count;
            List<Customer> list = query.ToList();

            AssertCommandTextSame(expected, query);
            Assert.AreEqual(count, list.Count);
        }
    }
}