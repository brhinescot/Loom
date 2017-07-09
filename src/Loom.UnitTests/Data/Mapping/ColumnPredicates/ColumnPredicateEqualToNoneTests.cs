#region Using Directives

using System.Collections.Generic;
using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateEqualToNoneTests
    {
        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        public void EqualToNoneOneInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        public void EqualToNoneTwoInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1, 3);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        public void EqualToNoneThreeInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1, 3, 5);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, true)]
        public void EqualToNoneParamsInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1, 3, 5, 7);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, true)]
        public void EqualToNoneCollectionInt(object value, bool expected)
        {
            List<int> numbers = new List<int> {1, 3, 5, 7};
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(numbers);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        public void EqualToNoneOneWithIgnoresInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        public void EqualToNoneTwoWithIgnoresInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1, 3, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        public void EqualToNoneThreeWithIgnoresInt(object value, bool expected)
        {
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(1, 3, 5, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(7, false)]
        [TestCase(8, true)]
        public void EqualToNoneCollectionWithIgnoresInt(object value, bool expected)
        {
            List<int> numbers = new List<int> {1, 3, 5, 7};
            ColumnPredicate predicate = Customer.Columns.CustomerId.IsEqualToNone(numbers, Ignore.Zero);

            Assert.AreEqual(expected, predicate.Evaluate(value));
        }
    }
}