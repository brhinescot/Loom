#region Using Directives

using System;
using AdventureWorks.Sales;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.ColumnPredicates
{
    [TestFixture]
    public class ColumnPredicateIgnoreTests
    {
        [Test]
        public void EqualToWithIgnore()
        {
            DateTime minTime = DateTime.MinValue;
            DateTime someTime = DateTime.Now;

            // Ignored conditions should return null.
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo("", Ignore.Empty));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(minTime, Ignore.MinDate));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(null, Ignore.Null));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(null, Ignore.NullOrEmpty));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo("", Ignore.NullOrEmpty));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(null, Ignore.NullOrMinDate));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(minTime, Ignore.NullOrMinDate));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(null, Ignore.NullOrZero));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(0, Ignore.NullOrZero));
            Assert.IsNull(Customer.Columns.CustomerId.IsEqualTo(0, Ignore.Zero));

            // Not Nulls
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo("Test", Ignore.Empty));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(someTime, Ignore.MinDate));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo("Test", Ignore.Null));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo("Test", Ignore.NullOrEmpty));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo("Test", Ignore.NullOrMinDate));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(someTime, Ignore.NullOrMinDate));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo("Test", Ignore.NullOrZero));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(2, Ignore.NullOrZero));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(3, Ignore.Zero));

            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(null, Ignore.None));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(0, Ignore.None));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo("", Ignore.None));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsEqualTo(minTime, Ignore.None));
        }

        [Test]
        public void NotEqualToWithIgnore()
        {
            DateTime minTime = DateTime.MinValue;
            DateTime someTime = DateTime.Now;

            // Nulls
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo("", Ignore.Empty));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(minTime, Ignore.MinDate));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(null, Ignore.Null));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(null, Ignore.NullOrEmpty));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo("", Ignore.NullOrEmpty));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(null, Ignore.NullOrMinDate));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(minTime, Ignore.NullOrMinDate));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(null, Ignore.NullOrZero));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(0, Ignore.NullOrZero));
            Assert.IsNull(Customer.Columns.CustomerId.IsNotEqualTo(0, Ignore.Zero));

            // Not Nulls
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo("Test", Ignore.Empty));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(someTime, Ignore.MinDate));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo("Test", Ignore.Null));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo("Test", Ignore.NullOrEmpty));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo("Test", Ignore.NullOrMinDate));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(someTime, Ignore.NullOrMinDate));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo("Test", Ignore.NullOrZero));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(2, Ignore.NullOrZero));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(3, Ignore.Zero));

            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(null, Ignore.None));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(0, Ignore.None));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo("", Ignore.None));
            Assert.IsNotNull(Customer.Columns.CustomerId.IsNotEqualTo(minTime, Ignore.None));
        }
    }
}