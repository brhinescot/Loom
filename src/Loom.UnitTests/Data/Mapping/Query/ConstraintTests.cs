#region Using Directives

using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    public class ConstraintTests : ActiveDataSharedSessionTestBase
    {
        [Test]
        public void SelectRandom()
        {
            const string expected = "SELECT TOP 3 * FROM [Sales].[Customer] " +
                                    "ORDER BY NEWID()";

            EntitySet<Customer> entitySet = Session.Customers;
            entitySet.Constrain(Constraint.Random(3));

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectAllColumnsTopCount()
        {
            const string expected = "SELECT TOP 10 * FROM [Sales].[Customer]";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Constrain(Constraint.TopCount(10));

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }

        [Test]
        public void SelectAllColumnsTopPercent()
        {
            const string expected = "SELECT TOP 10 PERCENT * FROM [Sales].[Customer]";

            EntitySet<Customer> entitySet = Session.EntitySet<Customer>();
            entitySet.Constrain(Constraint.TopPercent(10));

            AssertCommandTextSame(expected, entitySet);
            AssertParamCountSame(entitySet, 0);
        }
    }
}