#region Using Directives

using System;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateGreaterTests
    {
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        [TestCase("1", false)]
        [TestCase("2", false)]
        [TestCase("3", true)]
        [TestCase(1f, false)]
        [TestCase(2f, false)]
        [TestCase(3f, true)]
        [TestCase(1d, false)]
        [TestCase(2d, false)]
        [TestCase(3d, true)]
        [TestCase(null, false)]
        [TestCase("", false, ExpectedException = typeof(FormatException))]
        public void GreaterThanInt(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId > 2;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsGreaterThan(2);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase("A", false)]
        [TestCase("B", false)]
        [TestCase("C", true)]
        [TestCase('A', false)]
        [TestCase('B', false)]
        [TestCase('C', true)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void GreaterThanAlphaString(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId > "B";
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsGreaterThan("B");

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase("1", false)]
        [TestCase("2", false)]
        [TestCase("3", true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        [TestCase(1f, false)]
        [TestCase(2f, false)]
        [TestCase(3f, true)]
        [TestCase(1d, false)]
        [TestCase(2d, false)]
        [TestCase(3d, true)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void GreaterThanNumericString(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId > "2";
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsGreaterThan("2");

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(1d, false)]
        [TestCase(2d, true)]
        [TestCase(3d, true)]
        [TestCase(1f, false)]
        [TestCase(2f, true)]
        [TestCase(3f, true)]
        [TestCase("1", false)]
        [TestCase("2", true)]
        [TestCase("3", true)]
        [TestCase(null, false)]
        [TestCase("", false, ExpectedException = typeof(FormatException))]
        public void GreaterThanOrEqualInt(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId >= 2;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsGreaterOrEqualTo(2);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }
    }
}