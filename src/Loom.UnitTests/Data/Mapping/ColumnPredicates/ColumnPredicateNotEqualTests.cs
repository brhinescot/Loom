#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateNotEqualTests
    {
        [TestCase(1, true)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        public void NotEqualsInt(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId != 2;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsNotEqualTo(2);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }
    }
}