﻿#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateLessThanOrEqualTests
    {
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        public void LessThanOrEqualInt(object value, bool expected)
        {
            ColumnPredicate predicate1 = Customer.Columns.CustomerId <= 2;
            ColumnPredicate predicate2 = Customer.Columns.CustomerId.IsLessOrEqualTo(2);

            Assert.AreEqual(expected, predicate1.Evaluate(value));
            Assert.AreEqual(expected, predicate2.Evaluate(value));
        }
    }
}