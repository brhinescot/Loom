#region Using Directives

using System.Collections.Generic;
using AdventureWorks.Person;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateStartsWithTests
    {
        [TestCase("B786342785", true)]
        [TestCase("B", true)]
        [TestCase('B', true)]
        [TestCase("BBB95364", true)]
        [TestCase("7B86342785", false)]
        [TestCase("D", false)]
        [TestCase('D', false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase(1, false)]
        [TestCase(1, false)]
        public void StartsWithString(object value, bool success)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.StartsWith("B");

            Assert.AreEqual(success, predicate.Evaluate(value));
        }

        [TestCase("B786342785", true)]
        [TestCase("B", true)]
        [TestCase('B', true)]
        [TestCase("BBB95364", true)]
        [TestCase("7B86342785", false)]
        [TestCase("D", false)]
        [TestCase('D', false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase(1, false)]
        [TestCase(1, false)]
        public void StartsWithStringWithIgnores(object value, bool success)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.StartsWith("B", Ignore.Null);

            Assert.AreEqual(success, predicate.Evaluate(value));
        }

        [TestCase("Aaron", false)]
        [TestCase("Brian", true)]
        [TestCase("Clint", false)]
        [TestCase("James", false)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyOneString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("B");

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", false)]
        [TestCase("James", false)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyTwoString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("A", "B");

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", true)]
        [TestCase("James", false)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyThreeString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("A", "B", "C");

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", true)]
        [TestCase("James", true)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyParamsString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("A", "B", "C", "J");

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", true)]
        [TestCase("James", true)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyCollectionString(object name, bool success)
        {
            List<string> values = new List<string> {"A", "B", "C", "J"};
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny(values);

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", false)]
        [TestCase("Brian", true)]
        [TestCase("Clint", false)]
        [TestCase("James", false)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyOneWithIgnoresString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("B", Ignore.Zero);

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", false)]
        [TestCase("James", false)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyTwoWithIgnoresString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("A", "B", Ignore.Zero);

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", true)]
        [TestCase("James", false)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        public void StartsWithAnyThreeWithIgnoresString(object name, bool success)
        {
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny("A", "B", "C", Ignore.Zero);

            Assert.AreEqual(success, predicate.Evaluate(name));
        }

        [TestCase("Aaron", true)]
        [TestCase("Brian", true)]
        [TestCase("Clint", true)]
        [TestCase("James", true)]
        [TestCase("Tom", false)]
        [TestCase("Peter", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(1, false)]
        [TestCase(false, false)]
        public void StartsWithAnyCollectionWithIgnoresString(object name, bool success)
        {
            List<string> values = new List<string> {"A", "B", "C", "J"};
            ColumnPredicate predicate = Person.Columns.FirstName.StartsWithAny(values, Ignore.Zero);

            Assert.AreEqual(success, predicate.Evaluate(name));
        }
    }
}