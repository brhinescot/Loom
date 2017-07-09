#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateContainsTests
    {
        [TestCase("67298659285", false)]
        [TestCase("JH65JfT8D885", false)]
        [TestCase("JHHHJKHTDI875", true)]
        [TestCase("TDYTEY97585", true)]
        [TestCase("JDJH985TD", true)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void ContainsNonEmptyString(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.AccountNumber.Contains("TD");
            bool result = predicate.Evaluate(value);

            Assert.AreEqual(expected, result);
        }

        [TestCase("67298659285", true)]
        [TestCase(null, false)]
        [TestCase("", true)]
        public void ContainsEmptyString(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.AccountNumber.Contains("");
            bool result = predicate.Evaluate(value);

            Assert.AreEqual(expected, result);
        }

        [TestCase("67298659285", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void ContainsNull(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.AccountNumber.Contains(null);
            bool result = predicate.Evaluate(value);

            Assert.AreEqual(expected, result);
        }
    }
}