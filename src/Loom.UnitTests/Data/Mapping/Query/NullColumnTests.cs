#region Using Directives

using System;
using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class NullColumnTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void WhereOneNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] IS NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.AccountNumber == null);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void WhereOneNotNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] IS NOT NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.AccountNumber != null);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void WhereOneNotNullOneNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[ModifiedDate] IS NULL "
                                    + "AND _t0.[AccountNumber] IS NOT NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.ModifiedDate == null);
            entitySet.Where(Customer.Columns.AccountNumber != null);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void WhereOneDBNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] IS NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.AccountNumber == DBNull.Value);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void WhereOneNotDBNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[AccountNumber] IS NOT NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.AccountNumber != DBNull.Value);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void WhereOneNotDBNullOneDBNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "WHERE _t0.[ModifiedDate] IS NULL "
                                    + "AND _t0.[AccountNumber] IS NOT NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Where(Customer.Columns.ModifiedDate == DBNull.Value);
            entitySet.Where(Customer.Columns.AccountNumber != DBNull.Value);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void HavingOneNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[AccountNumber] "
                                    + "HAVING _t0.[AccountNumber] IS NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.GroupBy(Customer.Columns.AccountNumber);
            entitySet.Having(Customer.Columns.AccountNumber == null);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void HavingOneNotNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[AccountNumber] "
                                    + "HAVING _t0.[AccountNumber] IS NOT NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.GroupBy(Customer.Columns.AccountNumber);
            entitySet.Having(Customer.Columns.AccountNumber != null);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void HavingOneNotNullOneNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[AccountNumber] "
                                    + "HAVING (_t0.[ModifiedDate] IS NULL "
                                    + "AND _t0.[AccountNumber] IS NOT NULL)";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.GroupBy(Customer.Columns.AccountNumber);
            entitySet.Having((Customer.Columns.ModifiedDate == null) & (Customer.Columns.AccountNumber != null));

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void HavingOneDBNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[AccountNumber] "
                                    + "HAVING _t0.[AccountNumber] IS NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.GroupBy(Customer.Columns.AccountNumber);
            entitySet.Having(Customer.Columns.AccountNumber == DBNull.Value);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void HavingOneNotDBNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[AccountNumber] "
                                    + "HAVING _t0.[AccountNumber] IS NOT NULL";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.GroupBy(Customer.Columns.AccountNumber);
            entitySet.Having(Customer.Columns.AccountNumber != DBNull.Value);

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void HavingOneNotDBNullOneDBNull()
        {
            const string expected = "SELECT * FROM [Sales].[Customer] _t0 "
                                    + "GROUP BY _t0.[AccountNumber] "
                                    + "HAVING (_t0.[ModifiedDate] IS NULL "
                                    + "AND _t0.[AccountNumber] IS NOT NULL)";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.GroupBy(Customer.Columns.AccountNumber);
            entitySet.Having((Customer.Columns.ModifiedDate == DBNull.Value) & (Customer.Columns.AccountNumber != DBNull.Value));

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }
    }
}