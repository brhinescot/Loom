#region Using Directives

using AdventureWorks.Sales;
using Loom.Data.Mapping.Query;
using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Providers
{
    [TestFixture]
    public class SqlCommandWriterTests
    {
        [Test]
        public void SelectAllColumnsOneTable()
        {
            const string expected = "SELECT * FROM [Sales].[Customer]";

            SqlCommandWriter writer = new SqlCommandWriter();

            writer.WriteSelect();
            writer.WriteFrom(DataRecord<Customer>.Table);

            Util.AssertStringsSame(expected, writer.ToString());
        }

        [Test]
        public void SelectAllColumnsTwoTables()
        {
            const string expected = "SELECT * FROM [Sales].[Customer], [Sales].[Currency]";

            SqlCommandWriter writer = new SqlCommandWriter();

            writer.WriteSelect();
            writer.WriteFrom(Customer.Table, Currency.Table);

            Util.AssertStringsSame(expected, writer.ToString());
        }

        [Test]
        public void SelectSingleColumn()
        {
            const string expected = "SELECT _t0.[CustomerID] FROM [Sales].[Customer] _t0";

            SqlCommandWriter writer = new SqlCommandWriter();

            writer.WriteSelect(Customer.Columns.CustomerId);
            writer.WriteFrom(DataRecord<Customer>.Table);

            Util.AssertStringsSame(expected, writer.ToString());
        }

        [Test]
        public void SelectTwoColumns()
        {
            const string expected = "SELECT _t0.[CustomerID], _t0.[AccountNumber] FROM [Sales].[Customer] _t0";

            SqlCommandWriter writer = new SqlCommandWriter();

            writer.WriteSelect(Customer.Columns.CustomerId, Customer.Columns.AccountNumber);
            writer.WriteFrom(DataRecord<Customer>.Table);

            Util.AssertStringsSame(expected, writer.ToString());
        }

        [Test]
        [Ignore("Need to complete SqlCommandWriter")]
        public void SelectTwoColumnsWithWhere()
        {
            const string expected = "SELECT _t0.[CustomerID], _t0.[AccountNumber] FROM [Sales].[Customer] _t0" +
                                    " WHERE _t0.[CustomerID] > 100";

            SqlCommandWriter writer = new SqlCommandWriter();

            writer.WriteSelect(Customer.Columns.CustomerId, Customer.Columns.AccountNumber);
            writer.WriteFrom(DataRecord<Customer>.Table);
            writer.WriteWhere();
            writer.WritePredicate(Customer.Columns.CustomerId, ">", new ParameterNames("_p0", null));

            Util.AssertStringsSame(expected, writer.ToString());
        }
    }
}