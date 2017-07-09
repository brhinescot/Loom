#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Schema;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping
{
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public void SingleAggregateStaticMethod()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.Count();

            Assert.IsNotNull(column);
            Assert.AreEqual("COUNT(CustomerID)", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void DoubleAggregateStaticMethod()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.Count().Max();

            Assert.IsNotNull(column);
            Assert.AreEqual("MAX(COUNT(CustomerID))", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void SingleAggregateExtension()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.Count();

            Assert.IsNotNull(column);
            Assert.AreEqual("COUNT(CustomerID)", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void DoubleAggregateExtension()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.Count().Max();

            Assert.IsNotNull(column);
            Assert.AreEqual("MAX(COUNT(CustomerID))", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void IsNullStaticMethod()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.IsNull(0);

            Assert.IsNotNull(column);
            Assert.AreEqual("ISNULL(CustomerID, 0)", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void IsNullExtension()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.IsNull(0);

            Assert.IsNotNull(column);
            Assert.AreEqual("ISNULL(CustomerID, 0)", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void IsNullNestedStaticMethod()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.Max().IsNull(0);

            Assert.IsNotNull(column);
            Assert.AreEqual("ISNULL(MAX(CustomerID), 0)", string.Format(column.ColumnFormat, column.Name));
        }

        [Test]
        public void IsNullNestedExtension()
        {
            IQueryableColumn column = Customer.Columns.CustomerId.Max().IsNull(0);

            Assert.IsNotNull(column);
            Assert.AreEqual("ISNULL(MAX(CustomerID), 0)", string.Format(column.ColumnFormat, column.Name));
        }
    }
}