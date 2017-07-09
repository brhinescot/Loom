#region Using Directives

using System.Collections.Generic;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateEqualToAnyTests
    {
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void EqualToAnyOneInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        public void EqualToAnyTwoInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, 4);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        public void EqualToAnyThreeInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, 4, 6);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, true)]
        [TestCase(9, false)]
        public void EqualToAnyParamsInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, 4, 6, 8);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, true)]
        [TestCase(9, false)]
        public void EqualToAnyCollectionInt(object value, bool expected)
        {
            List<int> numbers = new List<int> {2, 4, 6, 8};
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(numbers);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void EqualToAnyOneWithIgnoresInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        public void EqualToAnyTwoWithIgnoresInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, 4, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        public void EqualToAnyThreeWithIgnoresInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(2, 4, 6, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, true)]
        [TestCase(9, false)]
        public void EqualToAnyCollectionWithIgnoresInt(object value, bool expected)
        {
            List<int> numbers = new List<int> {2, 4, 6, 8};
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToAny(numbers, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }
    }
}