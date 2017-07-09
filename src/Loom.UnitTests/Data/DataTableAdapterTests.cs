#region Using Directives

using System.Data;
using NUnit.Framework;

#endregion

namespace Loom.Data
{
    [TestFixture]
    public class DataTableAdapterTests
    {
        [Test]
        public void CreateBasic()
        {
            TestCustomer customer = new TestCustomer();
            DataTableAdapter<TestCustomer> adapter = new DataTableAdapter<TestCustomer>();
            DataTable table = adapter.Create(customer);

            Assert.AreEqual(5, table.Columns.Count);
            Assert.AreEqual("CustomerId", table.Columns[0].ColumnName);
            Assert.AreEqual("TerritoryId", table.Columns[1].ColumnName);
            Assert.AreEqual("AccountNumber", table.Columns[2].ColumnName);
            Assert.AreEqual("CustomerType", table.Columns[3].ColumnName);
            Assert.AreEqual("Rowguid", table.Columns[4].ColumnName);
        }

        [Test]
        public void CreateWithExtraColumn()
        {
            TestCustomer customer = new TestCustomer();
            customer.CustomerId = 100;

            DataTableAdapter<TestCustomer> adapter = new DataTableAdapter<TestCustomer>();
            adapter.AddColumn("CustomerId2", typeof(int), item => customer.CustomerId);
            DataTable table = adapter.Create(customer);

            Assert.AreEqual(6, table.Columns.Count);
            Assert.AreEqual("CustomerId", table.Columns[0].ColumnName);
            Assert.AreEqual("TerritoryId", table.Columns[1].ColumnName);
            Assert.AreEqual("AccountNumber", table.Columns[2].ColumnName);
            Assert.AreEqual("CustomerType", table.Columns[3].ColumnName);
            Assert.AreEqual("Rowguid", table.Columns[4].ColumnName);
            Assert.AreEqual("CustomerId2", table.Columns[5].ColumnName);

            Assert.AreEqual(100, table.Rows[0][0]);
            Assert.AreEqual(100, table.Rows[0]["CustomerId"]);
            Assert.AreEqual(table.Rows[0][0], table.Rows[0][5]);
            Assert.AreEqual(table.Rows[0]["CustomerId"], table.Rows[0]["CustomerId2"]);
        }
    }
}