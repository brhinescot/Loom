#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateTests
    {
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void BoolGreaterThanComparison(bool value, bool expectedResult)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId > false;
            bool result = predicate.Evaluate(value);

            Assert.AreEqual(expectedResult, result);
        }
    }
}