#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateLessThanTests
    {
        [TestCase(1, true)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        public void LessThanInt(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId < 2;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsLessThan(2);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase("A", true)]
        [TestCase("F", true)]
        [TestCase("G", false)]
        [TestCase("J", false)]
        public void LessThanString(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId < "G";
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsLessThan("G");

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }
    }
}